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
    public class AnimatedSprite : Sprite
    {
        protected List<Rectangle> frames;
        protected Rectangle currentFrame;
        protected int activeFrameIndex;

        public SpriteEffects spriteEffects;
        public float rotation;
        public Enums.Direction direction; //make this an enum

        protected TimeSpan frameTimer;
        protected TimeSpan elapsedFrameTime;

        public override Rectangle Hitbox
        {
            get { return new Rectangle((int)(Position.X - Center.X), (int)(Position.Y - Center.Y), (int)(frames[activeFrameIndex].Width * Scale), (int)(frames[activeFrameIndex].Height * Scale)); }
        }

        public Vector2 Center
        {
            get { return new Vector2(frames[activeFrameIndex].Width / 2 * Scale, frames[activeFrameIndex].Height / 2 * Scale); }
        }

        public AnimatedSprite(Texture2D image, Vector2 position, Color color, float scale, List<Rectangle> frames, TimeSpan frameTimer, SpriteEffects spriteEffects, float rotation)
        : base(image, position, color, scale)
        {
            this.spriteEffects = spriteEffects;
            this.rotation = rotation;
            this.frames = frames;
            this.frameTimer = frameTimer;
            elapsedFrameTime = TimeSpan.Zero;
            activeFrameIndex = 0;
        }

        public virtual void Update(GameTime gameTime)
        {
            elapsedFrameTime += gameTime.ElapsedGameTime;
            if (elapsedFrameTime >= frameTimer)
            {
                activeFrameIndex++;
                if (activeFrameIndex >= frames.Count)
                {
                    activeFrameIndex = 0;
                }

                currentFrame = frames[activeFrameIndex];
                elapsedFrameTime = TimeSpan.Zero;
            }

            //Position = new Vector2(Position.X + xspeed, Position.Y + yspeed);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle? DrawFrame = currentFrame;
            if (currentFrame.Width == 0)
            {
                DrawFrame = null;
            }
            spriteBatch.Draw(Image, Position, DrawFrame, Color, rotation, Center / new Vector2(Scale), new Vector2(Scale), spriteEffects, 0);
        }

    }

}

