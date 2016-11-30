using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPA__Game
{
    public class mButton
    {
        public Texture2D texture;
        public Vector2 position;
        public Rectangle rectangle;

        Color color = new Color(255, 255, 255, 255);

        public Vector2 size;

        public mButton(Texture2D newTexture, GraphicsDevice graphics, EventHandler onClick = null)
        {
            texture = newTexture;
            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);
            ButtonClicked = onClick;
        }


        bool down;
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            if (rectangle.Contains(mouse.Position))
            {
                if(color.A == 225)
                {
                    down = false;
                }
                if(color.A == 0)
                {
                    down = true;
                }
                if (down) color.A += 3; else color.A -= 3;

                if (mouse.LeftButton == ButtonState.Pressed) {
                    OnButtonClicked();
                }          
           }
            else if (color.A < 255)
            {
                color.A += 3;
                
            }

        }
        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        public void Draw(SpriteBatch sprtieBatch)
        {
            sprtieBatch.Draw(texture, rectangle, color);
        }

        public event EventHandler ButtonClicked;
        public void OnButtonClicked()
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }


    }
}
