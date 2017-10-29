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
    public class Pacman : AnimatedSprite
    {
        

        public bool Intersecting;
        Enums.Direction badDirection;
        Enums.Direction nextDirection;
        int previousDirection;
        int offset;
        public Texture2D fov;
        public Vector2 Speed;
        public bool fog;

        //public int LookRadius;

        public Pacman(Texture2D image, Texture2D fovTexture, Vector2 position, Color color, float scale, List<Rectangle> frames, TimeSpan frameTimer, SpriteEffects spriteEffects, float rotation, int offset)
            :base(image, position, color, scale, frames, frameTimer, spriteEffects, rotation)
        {
            fov = fovTexture;
            this.offset = offset;
            direction = Enums.Direction.Stop;
            nextDirection = Enums.Direction.Stop;
            //LookRadius = 2;
            Speed = Vector2.Zero;
        }

        public void Update(GameTime gameTime, KeyboardState ks, int blockPosX, int blockPosY, Viewport viewport)
        {
            Update(gameTime); 


            

            
            
                Position += Speed;
            

            if (ks.IsKeyDown(Keys.Space))
            {
                nextDirection = Enums.Direction.Stop;
                Position = new Vector2(Position.X - 5, Position.Y - 5);
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                nextDirection = Enums.Direction.Down;
                Intersecting = false;
            }
            else if (ks.IsKeyDown(Keys.Up))
             {
                nextDirection = Enums.Direction.Up;
                Intersecting = false;

            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                nextDirection = Enums.Direction.Right;
                Intersecting = false;

            }
            else if (ks.IsKeyDown(Keys.Left))
            {
                nextDirection = Enums.Direction.Left;
                Intersecting = false;

            }
            if(Position.X % 50 == offset && Position.Y % 50 == offset)
            {
                direction = nextDirection;
            }

            if (Position.X >= blockPosX)
            {
                Speed = Vector2.Zero;
            }
            for (int i = 0; i < viewport.Width; i += 10)
            {

            }
            if (direction == Enums.Direction.Down)
            {
                rotation = (float)Math.PI * 2 * .25f;
                Speed = new Vector2(0, 2f);
            }                
            else if (direction == Enums.Direction.Up)
            {
                rotation = (float)Math.PI * 2 * .75f;
                Speed = new Vector2(0, -2f);
            }
            else if (direction == Enums.Direction.Right)
            {
                //spriteEffects = SpriteEffects.FlipVertically;
                rotation = 0f;
                Speed = new Vector2(2f, 0);
            }
            else if (direction == Enums.Direction.Left)
            {
                //spriteEffects = SpriteEffects.FlipHorizontally;
                rotation = (float)Math.PI;
                Speed = new Vector2(-2f, 0);
            }
            else if(direction == Enums.Direction.Stop)
            {
                Speed = new Vector2(0, 0);
            }
            if (Intersecting)
            {
                if (Position.X % 50 != 0)
                {
                    double answer = Position.X / 50;
                    answer = Math.Floor(answer);
                    answer *= 50;
                    Position = new Vector2((float)answer, Position.Y);

                }
                if (Position.Y % 50 != 0)
                {
                    double answer = Position.Y / 50;
                    answer = Math.Floor(answer);
                    answer *= 50;
                    Position = new Vector2(Position.X, (float)answer);
                }
            }

        }



        public void HitWall()
        {
            Intersecting = true;
            Position = new Vector2((int)Position.X / 50 * 50 + offset, (int)Position.Y / 50 * 50 + offset);
            direction = Enums.Direction.Stop;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if(fog == true)
            {
                spriteBatch.Draw(fov, Position - new Vector2(fov.Width, fov.Height) / 2, Color.White);
            }
            else if(fog == false)
            {

            }
            
        }


    }
}
