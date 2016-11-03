using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPA__Game
{
    public class LoadScreen:Screen
    {
        mButton btnLoad1;
        mButton btnLoad2;
        mButton btnBack;
        Texture2D background;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenName nextScreen;
        int screenWidth;
        int screenHeight;

       
        public LoadScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
        }

        public ScreenName GetNextScreen()
        {
            return nextScreen;
        }



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            btnLoad2 = new mButton(Content.Load<Texture2D>("BtnLoad2"), graphics.GraphicsDevice);
            btnLoad1 = new mButton(Content.Load<Texture2D>("BtnLoad"), graphics.GraphicsDevice);
            btnBack = new mButton(Content.Load<Texture2D>("BtnBack"), graphics.GraphicsDevice);
            btnLoad2.ButtonClicked += HandleButtonClicked;
            btnLoad1.ButtonClicked += HandleButtonClicked;
            btnBack.ButtonClicked += HandleButtonClicked;
            btnLoad2.setPosition(new Vector2(350, 300));
            btnLoad1.setPosition(new Vector2(350, 300 + btnLoad1.size.Y * 2));
            btnBack.setPosition(new Vector2(350, 300 + btnBack.size.Y * 4));
            this.IsMouseVisible = true;
        }
        protected override void UnloadContent()
        {
            btnLoad2.ButtonClicked -= HandleButtonClicked;
            btnLoad1.ButtonClicked -= HandleButtonClicked;

            base.UnloadContent();
        }
        public  override void Update(GameTime gameTime)
        {

            MouseState mouse = Mouse.GetState();
            btnLoad2.Update(mouse);
            btnLoad1.Update(mouse);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            btnLoad2.Draw(spriteBatch);
            btnLoad1.Draw(spriteBatch);

        }


        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            if (sender == btnLoad1)
            {
                nextScreen = ScreenName.GameScreen;
            }
            else if (sender == btnLoad2)
            {
                nextScreen = ScreenName.GameScreen;
            }
            else if (sender == btnBack)
            {
                nextScreen = ScreenName.OptionsScreen;
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
