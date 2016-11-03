﻿using Microsoft.Xna.Framework;
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
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color color = new Color(255, 255, 255, 255);

        public Vector2 size;

        public mButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;
            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);

        }


        bool down;
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
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
            if(ButtonClicked != null)
            {
                ButtonClicked(this, EventArgs.Empty);
            }
        }


    }
}
