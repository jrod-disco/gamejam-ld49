using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using MonoGame.Extended;
using MonoGame.Extended.Sprites;

using UnstableDeck.GameObjects;

namespace UnstableDeck.GameObjects
{
    public class Card : GameObject
    {
        public CardTypes CardType;
        public Texture2D FaceTexture;
        
        public Card(ContentManager _content, CardTypes _cardType)
        {
            CardType = _cardType;

            switch (CardType)
            {
                
                case CardTypes.Good:
                    Console.WriteLine("good card");
                    FaceTexture = _content.Load<Texture2D>("card-good-test");
                break;

                case CardTypes.Bad:
                    Console.WriteLine("bad card");
                    FaceTexture = _content.Load<Texture2D>("card-bad-test");
                break;

                case CardTypes.Neutral:
                    Console.WriteLine("neutral card");
                    FaceTexture = _content.Load<Texture2D>("card-neutral-test");
                break;

                default:
                    Console.WriteLine($"card type not found: _{CardType}");
                break;
            }
        }

        public void Draw (GameTime gameTime, SpriteBatch _spriteBatch)
        {
            // Render the blank card with the face texture overlay in the requisite sprite batch
            _spriteBatch.Draw(Texture, Position, null, Color.White * Opacity, Rotation, CenterOrigin, Scale, SpriteEffects.None, 0f);   
            _spriteBatch.Draw(FaceTexture, Position, null, Color.White * Opacity, Rotation, CenterOrigin, Scale, SpriteEffects.None, 0f);   
        }

    }
}