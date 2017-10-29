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
using static WindowsGame3.Enums;

namespace WindowsGame3
{

    public class Minotaur : AnimatedSprite
    {
        public List<Rectangle> upframes = new List<Rectangle>();
        public List<Rectangle> downframes = new List<Rectangle>();
        public List<Rectangle> leftframes = new List<Rectangle>();
        public List<Rectangle> rightframes = new List<Rectangle>();
        

        public bool Intersecting;

        Enums.Direction nextDirection;
        int previousDirection;
        int offset;
        public Vector2 Speed;
        public int rene = Game1.random.Next(0, 4);
        public int rene2;

        //public int LookRadius;

        public Minotaur(Texture2D image, Vector2 position, Color color, float scale,
                TimeSpan frameTimer, SpriteEffects spriteEffects, float rotation, int offset)
                : base(image, position, color, scale, new List<Rectangle>(), frameTimer, spriteEffects, rotation)
        {
            this.offset = offset;
            direction = Enums.Direction.Stop;
            nextDirection = Enums.Direction.Stop;
            //LookRadius = 2;
            Speed = Vector2.Zero;

            downframes.Add(new Rectangle(13, 3, 24, 45));
            downframes.Add(new Rectangle(61, 4, 24, 45));
            downframes.Add(new Rectangle(107, 1, 25, 47));

            upframes.Add(new Rectangle(12, 143, 24, 45));
            upframes.Add(new Rectangle(60, 143, 24, 45));
            upframes.Add(new Rectangle(107, 143, 24, 45));

            leftframes.Add(new Rectangle(15, 48, 24, 45));
            leftframes.Add(new Rectangle(61, 48, 24, 45));
            leftframes.Add(new Rectangle(110, 49, 24, 45));

            rightframes.Add(new Rectangle(14, 93, 24, 45));
            rightframes.Add(new Rectangle(61, 95, 24, 45));
            rightframes.Add(new Rectangle(106, 94, 24, 45));

            foreach (Rectangle frame in upframes)
            {
                base.frames.Add(frame);
            }


            foreach (Rectangle frame in downframes)
            {
                base.frames.Add(frame);
            }


            foreach (Rectangle frame in leftframes)
            {
                base.frames.Add(frame);
            }


            foreach (Rectangle frame in rightframes)
            {
                base.frames.Add(frame);
            }
        }

        public void Update(GameTime gameTime, KeyboardState ks, Viewport viewport)
        {
            Update(gameTime);
            if (Intersecting)
            {
                direction = Enums.Direction.Down;
            }
            else
            {
                Position += Speed;
            }









            if (direction == Direction.Down)
            {

                Speed = new Vector2(0, 2f);
            }
            else if (direction == Direction.Up)
            {

                Speed = new Vector2(0, -2f);
            }
            else if (direction == Direction.Right)
            {
                //spriteEffects = SpriteEffects.FlipVertically;

                Speed = new Vector2(2f, 0);
            }
            else if (direction == Direction.Left)
            {
                //spriteEffects = SpriteEffects.FlipHorizontally;

                Speed = new Vector2(-2f, 0);
            }
            else if (direction == Direction.Stop)
            {
                Speed = new Vector2(0, 0);
            }


            //Position = new Vector2((int)Position.X / 50 * 50 + offset, (int)Position.Y / 50 * 50 + offset);
        }

        public void HitWall()
        {

            if (rene == 0)
            {
                direction = Enums.Direction.Down;
                Intersecting = false;
            }
            else if (rene == 1)
            {
                direction = Enums.Direction.Up;
                Intersecting = false;

            }
            else if (rene == 2)
            {
                direction = Enums.Direction.Right;
                Intersecting = false;

            }
            else if (rene == 3)
            {
                direction = Enums.Direction.Left;
                Intersecting = false;

            }

            if (direction == Enums.Direction.Right && rene == 2)
            {
                rene2 = Game1.random.Next(0, 3);
                if (rene2 == 0)
                {
                    direction = Enums.Direction.Down;
                    Intersecting = false;
                }
                else if (rene2 == 1)
                {
                    direction = Enums.Direction.Up;
                    Intersecting = false;

                }
                else if(rene2 == 2)
                {
                    direction = Enums.Direction.Left;
                    Intersecting = false; 
                }


            }
            else if(direction == Enums.Direction.Left && rene == 3)
            {
                rene2 = Game1.random.Next(0, 3);
                if (rene2 == 0)
                {
                    direction = Enums.Direction.Down;
                    Intersecting = false;
                }
                else if (rene2 == 1)
                {
                    direction = Enums.Direction.Up;
                    Intersecting = false;

                }
                else if (rene2 == 2)
                {
                    direction = Enums.Direction.Right;
                    Intersecting = false;
                }
            }
            else if(direction == Enums.Direction.Up && rene == 1)
            {
                rene2 = Game1.random.Next(0, 3);
                if (rene2 == 0)
                {
                    direction = Enums.Direction.Down;
                    Intersecting = false;
                }
                else if (rene2 == 1)
                {
                    direction = Enums.Direction.Right;
                    Intersecting = false;

                }
                else if (rene2 == 2)
                {
                    direction = Enums.Direction.Left;
                    Intersecting = false;
                }
            }
            else if(direction == Enums.Direction.Down && rene == 0)
            {
                rene2 = Game1.random.Next(0, 3);
                if (rene2 == 0)
                {
                    direction = Enums.Direction.Right;
                    Intersecting = false;
                }
                else if (rene2 == 1)
                {
                    direction = Enums.Direction.Up;
                    Intersecting = false;

                }
                else if (rene2 == 2)
                {
                    direction = Enums.Direction.Left;
                    Intersecting = false;
                }
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
                if(Position.Y % 50 != 0)
                {
                    double answer = Position.Y / 50;
                    answer = Math.Floor(answer);
                    answer *= 50;
                    Position = new Vector2(Position.X, (float)answer);
                }
            }


            
            
           

            Intersecting = false;
            Position = new Vector2((int)Position.X / 50 * 50 + offset, (int)Position.Y / 50 * 50 + offset);

        }


        public void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }
    }

}


