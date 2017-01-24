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
     
        public EnemyAI(int posX, int posY)
        {
            rand = new Random();
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
            Height = image.Height;
            Width = image.Width / 3;
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
            sourceRect = new Rectangle(21 * frames, 0, image.Width / 3, image.Height);
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
            oldPosition = position;
            if (randTime <= 0)
            {

                randTime = rand.Next(60, 160);
                randDirection = rand.Next(0, 6);
                //randDirection *= (int) position.X + (int) position.Y;    

            }
            randTime--;

            
              if(player.position.X> position.X && randDirection >3)
              {
                  position.X += 1.0f;
                  image = rightWalk;

              }
              else if (player.position.X < position.X && randDirection >3)
              {
                  position.X -= 1.0f;
                  image = leftWalk;

              }
              else if(player.position.Y > position.Y && randDirection >3)
              {
                  position.Y += 1.0f;
                  image = downWalk;

              }
              else if(player.position.Y < position.Y && randDirection >3)
              {
                  position.Y -= 1.0f;
                  image = upWalk;

              }
              else{
                 if(randDirection == 0)
                {
                    position.X += 1.0f;
                    image = rightWalk;
                }
                 else if (randDirection == 1)
                {
                    position.X -= 1.0f;
                    image = leftWalk;
                }
                 else if (randDirection == 2)
                {
                    position.Y += 1.0f;
                    image = downWalk;
                }
                 else
                {
                    position.Y -= 1.0f;
                    image = upWalk;
                }

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

