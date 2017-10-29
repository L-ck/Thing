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
    class Tile
    {
        Sprite sprite;
        int x;
        int y;

        public Tile(int x, int y, Sprite image)
        {
            this.x = x;
            this.y = y;
            
            if(image != null)
            {
                sprite = new Sprite(image.Image, new Vector2(x, y), image.Color, image.Scale);                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
             if(sprite != null)
            {
                sprite.Draw(spriteBatch);
            }
        }
        public Rectangle Hitbox()
        {
            if(sprite == null)
            {
                return Rectangle.Empty;
            }
            return sprite.Hitbox;
            
        }
    }
}
