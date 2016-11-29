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

        Vector2 position;
        public void LoadContent(ContentManager content)
        {
            rightAnime = content.Load<Texture2D>("Blue Right Movement");
            leftAnime = content.Load<Texture2D>("Blue Left Movement");
            upAnime = content.Load<Texture2D>("Blue Back Movement");
            downAnime = content.Load<Texture2D>("Blue Frount Movement");
            texture = downAnime;

            position = Vector2.Zero;
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W))
            {
                texture = upAnime;
                position += new Vector2(0, -2);
            }
            else if (state.IsKeyDown(Keys.D))
            {
                texture = rightAnime;
                position += new Vector2(1, 0);
            }
            else if (state.IsKeyDown(Keys.A))
            {
                texture = leftAnime;
                position += new Vector2(-1, 0);
            }
            else if (state.IsKeyDown(Keys.S))
            {
                texture = downAnime;
                position += new Vector2(0, 1);
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }

}
