using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Tweening;

namespace UnstableDeck.Screens
{
    public class TitleScreen : GameScreen
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _bgPaper, _bgSymbol, _bgLandscape, _bgLogo, _btnStart, _cardBlank;

        public TitleScreen(Game game)
            : base(game)
        {
            game.IsMouseVisible = true;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            _bgPaper = Content.Load<Texture2D>("bg-paper");
            _bgLandscape = Content.Load<Texture2D>("bg-nukeLandscape");
            _bgSymbol = Content.Load<Texture2D>("bg-nukeSymbol");
            _bgLogo = Content.Load<Texture2D>("bg-logo");
            _btnStart = Content.Load<Texture2D>("btn-title-start");
            _cardBlank= Content.Load<Texture2D>("card-blank");

        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = MouseExtended.GetState();
            var keyboardState = KeyboardExtended.GetState();

            if (keyboardState.WasKeyJustDown(Keys.Escape))
                Game.Exit();

            if (mouseState.LeftButton == ButtonState.Pressed || keyboardState.WasAnyKeyJustDown()){
                //ScreenManager.LoadScreen(new PongGameScreen(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Magenta);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_bgPaper, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.Draw(_bgSymbol, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.Draw(_bgLandscape, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            _spriteBatch.Draw(_bgLogo, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            _spriteBatch.Draw(_btnStart, new Vector2(GraphicsDevice.Viewport.Width/2, 325), null, Color.White, 0f, new Vector2(_btnStart.Width/2, _btnStart.Height/2), new Vector2(1, 1), SpriteEffects.None, 0f);

             _spriteBatch.Draw(_cardBlank, new Vector2(GraphicsDevice.Viewport.Width*0.75f, 300), null, Color.White, -0.3f, new Vector2(_cardBlank.Width/2, _cardBlank.Height/2), new Vector2(1, 1), SpriteEffects.None, 0f);
              _spriteBatch.Draw(_cardBlank, new Vector2(GraphicsDevice.Viewport.Width*0.75f+50, 275), null, Color.White, -0.0f, new Vector2(_cardBlank.Width/2, _cardBlank.Height/2), new Vector2(1, 1), SpriteEffects.None, 0f);

              _spriteBatch.Draw(_cardBlank, new Vector2(GraphicsDevice.Viewport.Width*0.75f+100, 250), null, Color.White, 0.3f, new Vector2(_cardBlank.Width/2, _cardBlank.Height/2), new Vector2(1, 1), SpriteEffects.None, 0f);


            _spriteBatch.End();
        }

    }
}