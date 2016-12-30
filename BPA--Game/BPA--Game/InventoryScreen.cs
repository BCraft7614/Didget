using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BPA__Game.Content
{
    public class InventoryScreen : Screen
    {
        mButton btnBack;

        int screenWidth;
        int screenHeight;

        public InventoryScreen()
        {
            screenWidth = 400;
            screenHeight = 300;
        }

        public override void LoadContent(ContentManager ContentMgr, GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            btnBack = new mButton(ContentMgr.Load<Texture2D>("BtnBack"), graphics.GraphicsDevice);
            btnBack.ButtonClicked += HandleButtonClicked;

        }
        public override void UnloadContent()
        {
            btnBack.ButtonClicked -= HandleButtonClicked;
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            btnBack.Update();

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            btnBack.Draw(spriteBatch);

        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            sender = btnBack;
            if (sender == btnBack)
            {
                nextScreen = "GameScreen";//ScreenNaem.GameScreen;
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
