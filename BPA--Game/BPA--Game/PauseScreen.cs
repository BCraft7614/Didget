using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BPA__Game
{
    public class PauseScreen : Game
    {
        mButton btnResume;
        mButton btnRestart;
        mButton btnOp;
        Texture2D background;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int screenWidth;
        int screenHeight;

        public PauseScreen()
        {
            screenWidth = 800;
            screenHeight = 600;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            MouseState mouse = Mouse.GetState();
            if (btnResume.isClicked == true)
            {
                //CurrentScreen = Screen.PlayScreen;

            }
            else if (btnOp.isClicked == true)
            {
                //CurrentScreen = screenHeight.OptionScreen;
            }
            btnResume.Update(mouse);
            btnOp.Update(mouse);

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            btnResume = new mButton(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnOp = new mButton(Content.Load<Texture2D>("OpButton"), graphics.GraphicsDevice);
            btnRestart = new mButton(Content.Load<Texture2D>("RestatButton"), graphics.GraphicsDevice);
            btnResume.setPosition(new Vector2(350, 300));
            btnOp.setPosition(new Vector2(350, 300 + btnOp.size.Y * 2));
            this.IsMouseVisible = true;
        }
        protected override void Draw(GameTime gameTime)
        {
            btnResume.Draw(spriteBatch);
            btnOp.Draw(spriteBatch);
        }

    }

}


    
