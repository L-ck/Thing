using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame3
{
    public class Sprite
    {
        public Texture2D Image { get; private set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public float Scale { get; set; }

        public virtual Rectangle Hitbox
        {
            get { return new Rectangle((int)Position.X , (int)Position.Y , (int)(Image.Width * Scale), (int)(Image.Height * Scale)); }
        }

        public Sprite(Texture2D image, Vector2 position, Color color, float scale)
        {
            Image = image;
            Position = position;
            Color = color;
            Scale = scale;
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(image, position, color);
            spriteBatch.Draw(Image, Position, null, Color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }


    }
}
