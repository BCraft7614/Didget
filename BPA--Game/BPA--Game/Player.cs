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
        Texture2D rightAnime;
        Texture2D leftAnime;
        Texture2D upAnime;
        Texture2D downAnime;
        Rectangle soruceRect;
        float elapsed;
        float delay = 200f;
        int frames = 0;


        public override void LoadContent(ContentManager content)
        {
            rightAnime = content.Load<Texture2D>("DidgetRight");
            leftAnime = content.Load<Texture2D>("DidgetLeft");
            upAnime = content.Load<Texture2D>("Blue Back Movement");
            downAnime = content.Load<Texture2D>("Blue Front Movement");
            image = downAnime;

            position = Vector2.Zero;

        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                image = upAnime;
                position += new Vector2(0, -3);
                MovementAnimation(gameTime);
            }
            else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
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
                image = downAnime;
                position += new Vector2(0, 3);
                MovementAnimation(gameTime);
            }
            soruceRect = new Rectangle(32 * frames, 0, image.Width / 5, image.Height);
            base.Update(gameTime);
        }

        public void MovementAnimation(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsed >= delay)
            {
                if (frames >= 2)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
                
                //Something is wrong here
            }
        }
        //fix this part Ryan
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, soruceRect, Color.White);
        }
    }

}
