using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using UnstableDeck.Screens;

namespace UnstableDeck
{
    public class GameRoot : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;

        private readonly int gameWidth = 1000;
        private readonly int gameHeight = 500;

        public GameRoot()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = gameWidth,
                PreferredBackBufferHeight = gameHeight,
                SynchronizeWithVerticalRetrace = false
                
            };

            Content.RootDirectory = "Content";
            IsFixedTimeStep = false;
            IsMouseVisible = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1f / 60f);

            _screenManager = Components.Add<ScreenManager>();
        }

        protected override void Initialize()
        {

            Window.Title="UnstableDeck (LudumDare49)";

             _graphics.PreferredBackBufferHeight = gameHeight;
             _graphics.PreferredBackBufferWidth = gameWidth;
             _graphics.ApplyChanges();

            _screenManager.LoadScreen(new TitleScreen(this), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));

        }

        protected override void LoadContent()
        {
            base.LoadContent();

            
        }

       
    }
}
