using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace BPA__Game
{
    class EnemyAI : Entity
    {
        public Direction direction;
        Texture2D rightWalk, leftWalk, upWalk, downWalk, currentWalk, texture;
        Rectangle destRect;
        Rectangle sourceRect;
        Random rand;
        Player player;
        float elapsed;
        float delay = 200f;
        int frames = 0;
        int randTime = 0;
        int randDirection = 0;

        public void Enemy(Player player, int posX, int posY, int enemySeed)
        {
            rand = new Random(enemySeed);
            position.X = posX;
            position.Y = posY;
        }
        public override void LoadContent(ContentManager Content)
        {

            base.LoadContent(Content);
            rightWalk = Content.Load<Texture2D>("R Right Movement");
            leftWalk = Content.Load<Texture2D>("R Left Movement");
            upWalk = Content.Load<Texture2D>("R Back Movement");
            downWalk = Content.Load<Texture2D>("R Front Movement");
            currentWalk = downWalk;
        }
        public void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames > 2)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle(32 * frames, 0, texture.Width / 3, texture.Height);
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (randTime <= 0)
            {
                randTime = rand.Next(10, 60);
                randDirection = rand.Next(0, 100);
                //randDirection *= (int) position.X + (int) position.Y;
                randDirection = randDirection % 2;
            }
            randTime--;
            if (Math.Round(player.position.X) > Math.Round(position.X) && Math.Round(player.position.Y) > Math.Round(position.Y))
            {
                if (randDirection == 1)
                {
                    position.X += 1.5f;
                    direction = Direction.right;
                    currentWalk = rightWalk;
                }
                else
                {
                    position.Y += 1.5f;
                    direction = Direction.down;
                    currentWalk = downWalk;
                }
            }
            else if (Math.Round(player.position.X) < Math.Round(position.X) && Math.Round(player.position.Y) > Math.Round(position.Y))
            {
                if (randDirection == 1)
                {
                    position.X -= 1.5f;
                    direction = Direction.left;
                    currentWalk = leftWalk;
                }
                else
                {
                    position.Y += 1.5f;
                    direction = Direction.down;
                    currentWalk = downWalk;
                }
            }
            else if (Math.Round(player.position.X) > Math.Round(position.X) && Math.Round(player.position.Y) < Math.Round(position.Y))
            {
                if (randDirection == 1)
                {
                    position.X += 1.5f;
                    direction = Direction.right;
                    currentWalk = rightWalk;
                }
                else
                {
                    position.Y -= 1.5f;
                    direction = Direction.up;
                    currentWalk = upWalk;
                }
            }
            else if (Math.Round(player.position.X) < Math.Round(position.X) && Math.Round(player.position.Y) < Math.Round(position.Y))
            {
                if (randDirection == 1)
                {
                    position.X -= 1.5f;
                    direction = Direction.left;
                    currentWalk = leftWalk;
                }
                else
                {
                    position.Y -= 1.5f;
                    direction = Direction.up;
                    currentWalk = upWalk;
                }
            }
            else if (Math.Round(player.position.X) > Math.Round(position.X))
            {
                position.X += 1.5f;
                direction = Direction.right;
                currentWalk = rightWalk;
            }
            else if (Math.Round(player.position.Y) > Math.Round(position.Y))
            {
                position.Y += 1.5f;
                direction = Direction.down;
                currentWalk = downWalk;
            }
            else if (Math.Round(player.position.X) < Math.Round(position.X))
            {
                position.X -= 1.5f;
                direction = Direction.left;
                currentWalk = leftWalk;
            }
            else if (Math.Round(player.position.Y) < Math.Round(position.Y))
            {
                position.Y -= 1.5f;
                direction = Direction.up;
                currentWalk = upWalk;
            }

            Animate(gameTime);
            destRect = new Rectangle((int)position.X, (int)position.Y, 31, 32);
            base.Update(gameTime);

        }

           
        }
    }
