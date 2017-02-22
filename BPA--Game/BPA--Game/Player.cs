using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace BPA__Game
{
    public class Player : Entity
    {
        public Direction direction;
        Texture2D rightAnime;
        Texture2D leftAnime;
        Texture2D upAnime;
        Texture2D downAnime;
        Rectangle soruceRect;
        Texture2D idleAnime;

        Random rand = new Random();
        public int coins;
        public int playerHealth;
        public int playerstr;
        public int playerdef;
        public int enemieskilled;
        public int fistUpgrade;

        public int healthPotion;
        float elapsed;
        float delay = 200f;
        int frames = 0;
        
        public Player()
        {
            playerHealth = 100;
            coins = 10;
            playerstr = 30;
            playerdef = 5;
            healthPotion = 5;
            enemieskilled = 0;
            fistUpgrade = 0;
        }
        /// <summary>
        /// Returns the amount of potions the player has
        /// </summary>
        /// <returns></returns>
        public int HealPotion()
        {
            return healthPotion;
        }

        /// <summary>
        /// Returns the amount of coins the player has
        /// </summary>
        /// <returns></returns>
        public int GetCoins()
        {
             return coins;
        }

        /// <summary>
        /// Returns player health
        /// </summary>
        /// <returns></returns>
        public int GetHealth()
        {
            return playerHealth;
        }

        /// <summary>
        /// Returns player strength
        /// </summary>
        /// <returns></returns>
        public int GetStrength()
        {
            return playerstr;
        }

        /// <summary>
        /// Returns player defense
        /// </summary>
        /// <returns></returns>
        public int GetDefense()
        {
             return playerdef;
        }

        public int GetFistUpgrade()
        {
            return fistUpgrade;
        }

        /// <summary>
        /// Returns the amount of enemies killed so far
        /// </summary>
        /// <returns></returns>
        public int GetenemiesKilled()
        {
            return enemieskilled;
        }

        public override void LoadContent(ContentManager content)
        {
            rightAnime = content.Load<Texture2D>("DidgetRight");
            leftAnime = content.Load<Texture2D>("DidgetLeft");
            upAnime = content.Load<Texture2D>("DigetUp");
            downAnime = content.Load<Texture2D>("DigetDown");
            idleAnime = content.Load<Texture2D>("IdleLeft");
            image = idleAnime;
            Height = image.Height;
            Width = image.Width / 3;
            position.X = 20;
            position.Y = 20;

        }
        public override void Update(GameTime gameTime)
        {
            oldPosition = position;
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                direction = Direction.up;
                image = upAnime;
                position += new Vector2(0, -3);
                MovementAnimation(gameTime);
            }
            else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                direction = Direction.right;
                image = rightAnime;
                position += new Vector2(3, 0);
                MovementAnimation(gameTime);
                

            }
            else if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                image = leftAnime;          
                position += new Vector2(-3, 0);
                MovementAnimation(gameTime);
                
            }
            else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                direction = Direction.down;
                image = downAnime;
                position += new Vector2(0, 3);
                MovementAnimation(gameTime);
            }

            else
            {
                image = downAnime;
            }
           
            soruceRect = new Rectangle(32 * frames, 0, image.Width / 3, image.Height);
            base.Update(gameTime);
        }

        public void MovementAnimation(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames >= 1)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
                
              
            }
        }
        
        //fix this part Ryan
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, soruceRect, Color.White);
            
        }

    }

}
