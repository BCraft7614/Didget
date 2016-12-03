using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BPA__Game
{
    public class OptionsScreen : Screen
    {
       
        mButton btnSave;
        mButton btnLoad;
        mButton btnBack;
        GraphicsDeviceManager graphics;
        Texture2D background;
        //SpriteBatch spriteBatch;
        int screenWidth;
        int screenHeight;

        

        public OptionsScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
            //LoadContent();
            //Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            btnLoad.Update(mouse);
            btnSave.Update(mouse);
            btnBack.Update(mouse);

        }

        public override void LoadContent(ContentManager ContentMgr, GraphicsDeviceManager graphics)
        {
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            btnSave = new mButton(ContentMgr.Load<Texture2D>("BtnSave"), graphics.GraphicsDevice);
            btnLoad = new mButton(ContentMgr.Load<Texture2D>("BtnLoad"), graphics.GraphicsDevice);
            btnBack = new mButton(ContentMgr.Load<Texture2D>("BtnBack"), graphics.GraphicsDevice);
            btnSave.ButtonClicked += HandleButtonClicked;
            btnLoad.ButtonClicked += HandleButtonClicked;
            btnBack.ButtonClicked += HandleButtonClicked;
            btnSave.setPosition(new Vector2(350, 100));
            btnLoad.setPosition(new Vector2(350, 200 + btnLoad.size.Y * 2));
            btnBack.setPosition(new Vector2(350, 300 + btnBack.size.Y * 4));
            background = ContentMgr.Load<Texture2D>("CellRoom");


        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 700), Color.White);
            btnSave.Draw(spriteBatch);
            btnLoad.Draw(spriteBatch);
            btnBack.Draw(spriteBatch);
        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            if (sender == btnSave)
            {
                nextScreen = "GameScreen"; //ScreenName.GameScreen;
            }
            else if (sender == btnLoad)
            {
                nextScreen = "LoadScreen"; //ScreenName.LoadScreen;
            }
            else if (sender == btnBack)
            {
                nextScreen = "TitleScreen"; //ScreenName.TitleScreen
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
