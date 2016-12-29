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
        Texture2D rightWalk, leftWalk, upWalk, downWalk;
        Rectangle sourceRect;
        Random rand;
        float elapsed;
        float delay = 200f;
        int frames = 0;
        int randTime = 0;
        int randDirection = 0;
        float oldDistance;
        public EnemyAI(Player player, int posX, int posY, int enemySeed)
        {
            rand = new Random(enemySeed);
            position.X = posX;
            position.Y = posY;
            
            
        }
        public override void LoadContent(ContentManager content)
        {

            base.LoadContent(content);
            rightWalk = content.Load<Texture2D>("Blue Right Movement");
            leftWalk = content.Load<Texture2D>("Blue Left Movement");
            upWalk = content.Load<Texture2D>("Blue Back Movement");
            downWalk = content.Load<Texture2D>("Blue Front Movement");
            image = downWalk;
        }
        public void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames > 1)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }
            sourceRect = new Rectangle(32 * frames, 0, image.Width / 3, image.Height);
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public void Update(GameTime gameTime,Player player)
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
                    image = rightWalk;
                }
                else
                {
                    position.Y += 1.5f;
                    direction = Direction.down;
                    image = downWalk;
                }
            }
            else if (Math.Round(player.position.X) < Math.Round(position.X) && Math.Round(player.position.Y) > Math.Round(position.Y))
            {
                if (randDirection == 1)
                {
                    position.X -= 1.5f;
                    direction = Direction.left;
                    image = leftWalk;
                }
                else
                {
                    position.Y += 1.5f;
                    direction = Direction.down;
                    image = downWalk;
                }
            }
            else if (Math.Round(player.position.X) > Math.Round(position.X) && Math.Round(player.position.Y) < Math.Round(position.Y))
            {
                if (randDirection == 1)
                {
                    position.X += 1.5f;
                    direction = Direction.right;
                    image = rightWalk;
                }
                else
                {
                    position.Y -= 1.5f;
                    direction = Direction.up;
                    image = upWalk;
                }
            }
            else if (Math.Round(player.position.X) < Math.Round(position.X) && Math.Round(player.position.Y) < Math.Round(position.Y))
            {
                if (randDirection == 1)
                {
                    position.X -= 1.5f;
                    direction = Direction.left;
                    image = leftWalk;
                }
                else
                {
                    position.Y -= 1.5f;
                    direction = Direction.up;
                    image = upWalk;
                }
            }
            else if (Math.Round(player.position.X) > Math.Round(position.X))
            {
                position.X += 1.5f;
                direction = Direction.right;
                image = rightWalk;
            }
            else if (Math.Round(player.position.Y) > Math.Round(position.Y))
            {
                position.Y += 1.5f;
                direction = Direction.down;
                image = downWalk;
            }
            else if (Math.Round(player.position.X) < Math.Round(position.X))
            {
                position.X -= 1.5f;
                direction = Direction.left;
                image = leftWalk;
            }
            else if (Math.Round(player.position.Y) < Math.Round(position.Y))
            {
                position.Y -= 1.5f;
                direction = Direction.up;
                image = upWalk;
            }

            Animate(gameTime);
          
            base.Update(gameTime);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, sourceRect, Color.White);
            base.Draw(spriteBatch);
        }


    }

    }

