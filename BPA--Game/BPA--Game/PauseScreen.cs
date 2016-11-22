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
    public class PauseScreen:Screen
    {
        /*
        mButton btnResume;
        mButton btnOp;
        mButton btnExit;
        GraphicsDeviceManager graphics;
        ScreenName nextScreen;
        SpriteBatch spriteBatch;
        int screenWidth;
        int screenHeight;

 
        public PauseScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
            LoadContent();
            Initialize();
        }
        public ScreenName GetNextScreen()
        {
            return nextScreen;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            MouseState mouse = Mouse.GetState();
 
            btnResume.Update(mouse);
            btnOp.Update(mouse);
            btnExit.Update(mouse);

        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            btnResume = new mButton(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnOp = new mButton(Content.Load<Texture2D>("OpButton"), graphics.GraphicsDevice);
            btnExit = new mButton(Content.Load<Texture2D>("BtnExit"), graphics.GraphicsDevice);
            btnResume.ButtonClicked += HandleButtonClicked;
            btnOp.ButtonClicked += HandleButtonClicked;
            btnExit.ButtonClicked += HandleButtonClicked;
            btnResume.setPosition(new Vector2(350, 300));    
            btnOp.setPosition(new Vector2(350, 300 + btnOp.size.Y * 2));
            btnExit.setPosition(new Vector2(359, 300 + btnExit.size.Y * 2));
            this.IsMouseVisible = true;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            btnResume.Draw(spriteBatch);
            btnOp.Draw(spriteBatch);
            btnExit.Draw(spriteBatch);
        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            if (sender == btnResume)
            {
                nextScreen = ScreenName.GameScreen;
            }
            else if (sender == btnOp)
            {
                nextScreen = ScreenName.OptionsScreen;
            }
            else if (sender == btnExit)
            {
                Exit();
            }
            
            OnButtonClicked();
        }

        public event EventHandler ButtonClicked;
        public void OnButtonClicked()
        {
            if (ButtonClicked != null)
            {
                ButtonClicked(this, EventArgs.Empty);
            }
        }
        */
    }
}
