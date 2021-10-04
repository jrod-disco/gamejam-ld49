using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


using MonoGame.Extended;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Tweening;
using MonoGame.Extended.Sprites;

using UnstableDeck.GameObjects;
using UnstableDeck.Smoke;

namespace UnstableDeck.Screens
{
    public class TitleScreen : GameScreen
    {
        private SpriteBatch _spriteBatch;
        private Texture2D _bgPaper, _bgSymbol, _bgLandscape, _bgLogo, _btnStart;
        private BackgroundElement bgPaper, bgSymbol, bgLandscape, bgLogo;
        private UIButton btnStart;
        private UIButtonToggle btnAudioToggle;

        private Song _music;
        

        private Bag<Card> _titleCards;
        private float cardOriginX  = 710;
        private int cardOriginY  = 275;
        private float cardOriginR  = MathHelper.ToRadians(-60);
        private int cardEndGap  = 75;

        private readonly Tweener _tweener = new Tweener();
        static Random _random = new Random ();
        private SmokeParticles _smoke;

        static T RandomEnumValue<T> ()
        {
            var v = Enum.GetValues (typeof (T));
            return (T) v.GetValue (_random.Next(v.Length));
        }

        public TitleScreen(Game game)
            : base(game)
        {
            game.IsMouseVisible = true;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _smoke = new SmokeParticles (GraphicsDevice);
            
            _bgPaper = Content.Load<Texture2D>("bg-paper");
            _bgLandscape = Content.Load<Texture2D>("bg-nukeLandscape");
            _bgSymbol = Content.Load<Texture2D>("bg-nukeSymbol");
            _bgLogo = Content.Load<Texture2D>("bg-logo");
            _btnStart = Content.Load<Texture2D>("btn-title-start");
 
            _music = Content.Load<Song>("bg_unstable_1_mixdown");
            MediaPlayer.Play(_music);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.25f;
            
 
        }


        public override void Initialize(){

            CreateBackgrounds();
            CreateTitleCards();
            CreateUI();
         
            TweenTitleCards();
            TweenBackgrounds();
            TweenUI();
        }

        public override void Update(GameTime gameTime)
        {

            _tweener.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            var mouseState = MouseExtended.GetState();
            var keyboardState = KeyboardExtended.GetState();

            if (keyboardState.WasKeyJustDown(Keys.Escape))
                Game.Exit();

            if (mouseState.LeftButton == ButtonState.Pressed || keyboardState.WasAnyKeyJustDown()){
                //ScreenManager.LoadScreen(new PongGameScreen(Game), new FadeTransition(GraphicsDevice, Color.Black, 0.5f));
            }

             if (mouseState.LeftButton == ButtonState.Pressed) OnLeftMouseDown(new Vector2(mouseState.X, mouseState.Y));
             if (mouseState.LeftButton == ButtonState.Released) OnLeftMouseUp(new Vector2(mouseState.X, mouseState.Y));

             _smoke.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend);
            DrawBackground();
            DrawTitleCards(gameTime, _spriteBatch);
            DrawUI();
            _spriteBatch.End();

            _smoke.Draw(gameTime, _spriteBatch);
            
        }


        private void OnLeftMouseDown(Vector2 _clickPosition)
        {
            // Start Button
            btnStart.Pressed(_clickPosition);
            // Audio Toggle
            btnAudioToggle.Pressed(_clickPosition);
        }

        private void OnLeftMouseUp(Vector2 _clickPosition)
        {
            // Start Button
            btnStart.Released(_clickPosition);
            // Audio Toggle
            btnAudioToggle.Released(_clickPosition);
        }

        private void CreateUI()
        {

            btnStart = new UIButton(){
                Position = new Vector2(GraphicsDevice.Viewport.Width/2, 350),
                DefaultTexture = Content.Load<Texture2D>("btn-title-start"),
                Opacity = 0,
                ClickHandler = ()=>{Console.WriteLine("Clicked btnStart");return true;},
            };
            btnStart.Texture = btnStart.DefaultTexture;
            btnStart.Origin = btnStart.CenterOrigin;

            btnAudioToggle = new UIButtonToggle(true){
                Position = new Vector2(GraphicsDevice.Viewport.Width-40, 40),
                DefaultTexture = Content.Load<Texture2D>("btn-sound-on"),
                ToggleTexture = Content.Load<Texture2D>("btn-sound-off"),
                Opacity = 0,
            };
            btnAudioToggle.Texture = btnAudioToggle.DefaultTexture;
            btnAudioToggle.Origin = btnAudioToggle.CenterOrigin;
            btnAudioToggle.ClickHandler = () => 
            {
                btnAudioToggle.Toggle();
                MediaPlayer.Volume = btnAudioToggle.isSelected ? 0.25f : 0f;
                Console.WriteLine($"Clicked btnAudioToggle {btnAudioToggle.isSelected}");
                return true;
            };
            
        }

        private void TweenUI()
        {
            _tweener.TweenTo(target: btnStart, expression: btns => btnStart.Opacity, toValue: 1, duration: 0.25f, delay: 2.75f)
            .Easing(EasingFunctions.SineOut);
            _tweener.TweenTo(target: btnStart, expression: btns => btnStart.Position, toValue: new Vector2(GraphicsDevice.Viewport.Width/2, 325), duration: 0.5f, delay: 2.75f)
            .Easing(EasingFunctions.SineOut);

            _tweener.TweenTo(target: btnAudioToggle, expression: btns => btnAudioToggle.Opacity, toValue: 1, duration: 0.25f, delay: 2.75f)
            .Easing(EasingFunctions.SineOut);

        }

        private void DrawUI()
        {
            _spriteBatch.Draw(btnStart.Texture, btnStart.Position, null, Color.White * btnStart.Opacity, btnStart.Rotation, btnStart.CenterOrigin, btnStart.Scale, SpriteEffects.None, 0f);
            _spriteBatch.Draw(btnAudioToggle.Texture, btnAudioToggle.Position, null, Color.White * btnAudioToggle.Opacity, btnAudioToggle.Rotation, btnAudioToggle.CenterOrigin, btnAudioToggle.Scale, SpriteEffects.None, 0f);
        }

        private void CreateBackgrounds()
        {
            bgPaper = new BackgroundElement{
                Texture = Content.Load<Texture2D>("bg-paper"),    
                Opacity = 0,
            };
            bgSymbol = new BackgroundElement{
                Texture = Content.Load<Texture2D>("bg-nukeSymbol"),    
                Opacity = 0,
            };
            bgLandscape = new BackgroundElement{
                Texture = Content.Load<Texture2D>("bg-nukeLandscape"),    
                Opacity = 0,
            };
            bgLogo = new BackgroundElement{
                Texture = Content.Load<Texture2D>("bg-logo"),    
                Opacity = 0,
            };
        }
        private void TweenBackgrounds()
        {
            
            _tweener.TweenTo(target: bgPaper, expression: bgpa => bgPaper.Opacity, toValue: 1, duration: 0.25f, delay: 0.5f)
            .Easing(EasingFunctions.SineOut);
            _tweener.TweenTo(target: bgSymbol, expression: bgsy => bgSymbol.Opacity, toValue: 1, duration: 0.5f, delay: 0.75f)
            .Easing(EasingFunctions.SineOut);
            _tweener.TweenTo(target: bgLandscape, expression: bgla => bgLandscape.Opacity, toValue: 1, duration: 1.0f, delay: 1.0f)
            .Easing(EasingFunctions.SineOut);
            _tweener.TweenTo(target: bgLogo, expression: bglo => bgLogo.Opacity, toValue: 1, duration: 1.5f, delay: 1.0f)
            .Easing(EasingFunctions.SineOut);
        }  
        


        private void DrawBackground()
        {

            _spriteBatch.Draw(bgPaper.Texture, bgPaper.Position, null, Color.White * bgPaper.Opacity, bgPaper.Rotation, bgPaper.Origin, bgPaper.Scale, SpriteEffects.None, 0f);
            _spriteBatch.Draw(bgSymbol.Texture, bgSymbol.Position, null, Color.White * bgSymbol.Opacity, bgSymbol.Rotation, bgSymbol.Origin, bgSymbol.Scale, SpriteEffects.None, 0f);
            _spriteBatch.Draw(bgLandscape.Texture, bgLandscape.Position, null, Color.White * bgLandscape.Opacity, bgLandscape.Rotation, bgLandscape.Origin, bgLandscape.Scale, SpriteEffects.None, 0f);
            _spriteBatch.Draw(bgLogo.Texture, bgLogo.Position, null, Color.White * bgLogo.Opacity, bgLogo.Rotation, bgLogo.Origin, bgLogo.Scale, SpriteEffects.None, 0f); 

        }


        private void CreateTitleCards()
        {
            var cardTexture = Content.Load<Texture2D>("card-blank");
            _titleCards = new Bag<Card>(3);

            Vector2[] cardPositions = new Vector2[3]{
                new Vector2(cardOriginX,cardOriginY),
                new Vector2(cardOriginX+50,cardOriginY-25),
                new Vector2(cardOriginX+100,cardOriginY),
            };

            for (int i = 0; i < 3; i++)
            {
                var randomCardType = RandomEnumValue<CardTypes> ();
                var thisCard  = new Card(Content, randomCardType){
                    Position = cardPositions[i],
                    Texture = cardTexture,
                    Opacity = 0,
                    Rotation = cardOriginR,
                };
                _titleCards.Add(thisCard);
            }


        }

        private void TweenTitleCards()
        {
            var i=0;
            var baseDelay = 1.5f;
            var baseDuration = 0.5f;
            foreach (var card in _titleCards)
            {
                 _tweener.TweenTo(target: card, expression: c => card.Position, toValue: new Vector2(cardOriginX+(i*cardEndGap), card.Position.Y), duration: baseDuration, delay: baseDelay+(i*0.1f))
                .Easing(EasingFunctions.SineOut);

                _tweener.TweenTo(target: card, expression: c => card.Rotation, toValue: MathHelper.ToRadians(-30+(i*30f)), duration: baseDuration/2+(i*0.1f), delay: baseDelay+(i*0.1f))
                .Easing(EasingFunctions.SineOut);

                _tweener.TweenTo(target: card, expression: c => card.Opacity, toValue: 1, duration: baseDuration/3, delay: baseDelay+(i*0.1f))
                .Easing(EasingFunctions.SineOut);

                  i++;
            }  
        }

        private void DrawTitleCards(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            foreach (var card in _titleCards)
            {
                card.Draw(gameTime, _spriteBatch);
            }  
        }


    }
}

// _tweener.TweenTo(this, a => a.myPoint, new Vector2(endRect.Width, endRect.Height), duration: duration, delay: delay).Easing(EasingFunctions.SineOut)