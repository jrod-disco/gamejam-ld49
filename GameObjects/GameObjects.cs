using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace UnstableDeck.GameObjects
{

     public enum CardTypes 
        {
            Good,
            Bad,
            Neutral
        };

    public enum ObjectTypes 
        {
            Unknown,
            Background,
            Card,
            Reactor,
        };


    public class GameObject
    {
   
        public ObjectTypes ObjectType = ObjectTypes.Unknown ;
        public Vector2 Position { get; set; } = new Vector2(0,0);
        public Vector2 Origin = new Vector2(0,0);
        public Vector2 Scale = Vector2.One;
        public float Rotation = 0;
        public float Opacity = 1;
        public Texture2D DefaultTexture;
        public Texture2D Texture;
        public Sprite Sprite;
        public RectangleF SpriteBoundingRectangle => Sprite.GetBoundingRectangle(Position, 0, Vector2.One);
        public Rectangle BoundingRectangle => new Rectangle((int)(Position.X-Origin.X), (int)(Position.Y-Origin.Y), (int)Texture.Width, (int)Texture.Height);
        public Vector2 CenterOrigin => new Vector2(Texture.Width/2,Texture.Height/2);
    }
}