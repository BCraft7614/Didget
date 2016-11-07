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
    public class OptionsScreen : Screen
    {
        mButton btnSave;
        mButton btnLoad;
        mButton btnBack;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenName nextScreen;
        int screenWidth;
        int screenHeight;

        

        public OptionsScreen()
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
            btnLoad.Update(mouse);
            btnSave.Update(mouse);
            btnBack.Update(mouse);

        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            btnSave = new mButton(Content.Load<Texture2D>("btnSave"), graphics.GraphicsDevice);
            btnLoad = new mButton(Content.Load<Texture2D>("BtnLoad"), graphics.GraphicsDevice);
            btnBack = new mButton(Content.Load<Texture2D>("BtnLoad"), graphics.GraphicsDevice);
            btnSave.ButtonClicked += HandleButtonClicked;
            btnLoad.ButtonClicked += HandleButtonClicked;
            btnBack.ButtonClicked += HandleButtonClicked;
            btnSave.setPosition(new Vector2(350, 300));
            btnLoad.setPosition(new Vector2(350, 300 + btnLoad.size.Y * 2));
            this.IsMouseVisible = true;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            btnSave.Draw(spriteBatch);
            btnLoad.Draw(spriteBatch);
            btnBack.Draw(spriteBatch);
        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            if (sender == btnSave)
            {
                nextScreen = ScreenName.GameScreen;
            }
            else if (sender == btnLoad)
            {
                nextScreen = ScreenName.OptionsScreen;
            }
            else if(sender == btnLoad)
            {
                nextScreen = ScreenName.LoadScreen;
            }
            else if (sender == btnBack)
            {
                nextScreen = ScreenName.PauseScreen;
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
        
        
    }
}
