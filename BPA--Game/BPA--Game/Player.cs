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
        Texture2D texture;
        Texture2D rightAnime;
        Texture2D leftAnime;
        Texture2D upAnime;
        Texture2D downAnime;
        Rectangle soruceRect;
        float elapsed;
        float delay = 200f;
        int frames = 0;

        Vector2 position;

        public void LoadContent(ContentManager content)
        {
            rightAnime = content.Load<Texture2D>("Blue Right Movement");
            leftAnime = content.Load<Texture2D>("Blue Left Movement");
            upAnime = content.Load<Texture2D>("Blue Back Movement");
            downAnime = content.Load<Texture2D>("Blue Front Movement");
            texture = downAnime;

            position = Vector2.Zero;
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W))
            {
                texture = upAnime;
                position += new Vector2(0, -3);
                MovementAnimation(gameTime);
            }
            else if (state.IsKeyDown(Keys.D))
            {
                texture = rightAnime;
                position += new Vector2(3, 0);
                MovementAnimation(gameTime);
            }
            else if (state.IsKeyDown(Keys.A))
            {
                texture = leftAnime;
                position += new Vector2(-3, 0);
                MovementAnimation(gameTime);
            }
            else if (state.IsKeyDown(Keys.S))
            {
                texture = downAnime;
                position += new Vector2(0, 3);
                MovementAnimation(gameTime);
            }
            soruceRect = new Rectangle(32 * frames, 0, texture.Width / 3, texture.Height);
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
            spriteBatch.Draw(texture, position, soruceRect, Color.White);
        }
    }

}
