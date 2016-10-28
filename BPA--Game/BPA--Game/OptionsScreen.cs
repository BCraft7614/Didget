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
    public class OptionsScreen : Game
    {
        mButton btnSave;
        mButton btnLoad;
        mButton btnBack;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Screen nextScreen;
        int screenWidth;
        int screenHeight;

        public enum Screen
        {
            TitleScreen,
            GameScreen,
            OptionsScreen,
            PauseScreen,
            LoadScreen,
        }

        public OptionsScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
        }

        public Screen GetNextString()
        {
            return nextScreen;
        }

        protected override void Update(GameTime gameTime)
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

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            btnSave = new mButton(Content.Load<Texture2D>("btnSave"), graphics.GraphicsDevice, "Save");
            btnLoad = new mButton(Content.Load<Texture2D>("BtnLoad"), graphics.GraphicsDevice, "Load");
            btnBack = new mButton(Content.Load<Texture2D>("BtnLoad"), graphics.GraphicsDevice, "Back");
            btnSave.ButtonClicked += HandleButtonClicked;
            btnLoad.ButtonClicked += HandleButtonClicked;
            btnBack.ButtonClicked += HandleButtonClicked;
            btnSave.setPosition(new Vector2(350, 300));
            btnLoad.setPosition(new Vector2(350, 300 + btnLoad.size.Y * 2));
            this.IsMouseVisible = true;
        }
        protected override void Draw(GameTime gameTime)
        {
            btnSave.Draw(spriteBatch);
            btnLoad.Draw(spriteBatch);
            btnBack.Draw(spriteBatch);
        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            if (sender == btnSave)
            {
                nextScreen = Screen.GameScreen;
            }
            else if (sender == btnLoad)
            {
                nextScreen = Screen.OptionsScreen;
            }
            else if(sender == btnLoad)
            {
                nextScreen = Screen.LoadScreen;
            }
            else if (sender == btnBack)
            {
                nextScreen = Screen.PauseScreen;
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
