using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Containers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.TextureAtlases;

using UnstableDeck.GameObjects;

namespace UnstableDeck.Smoke
{


    public class SmokeParticles: GameObject
    {
   
        private ParticleEffect _particleEffect;
        private Texture2D _particleTexture;

        public SmokeParticles(GraphicsDevice _graphicsDevice)
        {
            _particleTexture = new Texture2D(_graphicsDevice, 1, 1);
            _particleTexture.SetData(new[] { Color.White*0.5f });

            TextureRegion2D textureRegion = new TextureRegion2D(_particleTexture);
            _particleEffect = new ParticleEffect(autoTrigger: false)
            {
                Position = new Vector2(260, 235),
                Emitters = new List<ParticleEmitter>
                {
                    new ParticleEmitter(textureRegion, 25000, TimeSpan.FromSeconds(2.5),
                        //Profile.Spray(new Vector2(-0.95f,-1f), 1f ))
                        Profile.Circle(20, Profile.CircleRadiation.Out))
                        //Profile.Point())
                    {
                        Parameters = new ParticleReleaseParameters
                        {
                            Speed = new Range<float>(1f, 40f),
                            Quantity = 10,
                            Opacity = new Range<float>(0.1f, 0.5f),
                            Rotation = new Range<float>(-1f, 1f),
                            Scale = new Range<float>(10.0f, 30.0f),
                            Mass = 1.25f,
                        },
                        Modifiers =
                        {
                            new AgeModifier
                            {
                                Interpolators = new List<Interpolator>()
                                {
                                    new OpacityInterpolator { StartValue = 0.5f, EndValue = 0.0f },
                                    new ScaleInterpolator { StartValue = new Vector2(1,1), EndValue = new Vector2(20,20) }
                                }
                            },
                            new RotationModifier {RotationRate = -3f},

                           // new RectangleContainerModifier {Width = 800, Height = 480},
                            new OpacityFastFadeModifier(),
                            new LinearGravityModifier {Direction = new Vector2(-2.5f,-0.65f), Strength = 35f},
                            new DragModifier { Density = 0.5f, DragCoefficient = 1f }
                        }
                    }
                }
            };

        }

        public void Update(GameTime gameTime)
        {
            _particleEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            

        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
        
            _spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            _spriteBatch.Draw(_particleEffect);
            _spriteBatch.End();

        }
    }
}