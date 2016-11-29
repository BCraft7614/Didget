using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPA__Game
{
    public class GameScreen:Screen
    {
        // Work Here Ryan. Add Player and Battle Scene
        mButton btnPlay;
        mButton btnLoad;
        mButton btnOp;
        mButton btnExit;
        Texture2D background;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //ScreenName nextScreen;
        int screenWidth;
        int screenHeight;

  
        public GameScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
            //LoadContent();
            //Initialize();
        }

        public override void LoadContent(ContentManager ContentMgr,GraphicsDeviceManager graphics)
        {
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            btnPlay = new mButton(ContentMgr.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnOp = new mButton(ContentMgr.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnLoad = new mButton(ContentMgr.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            //btnExit = new mButton(ContentMgr.Load<Texture2D>("Buttomn"), graphics.GraphicsDevice);
            btnPlay.ButtonClicked += HandleButtonClicked;
            btnOp.ButtonClicked += HandleButtonClicked;
            btnLoad.ButtonClicked += HandleButtonClicked;
            btnPlay.setPosition(new Vector2(350, 100));
            btnOp.setPosition(new Vector2(350, 200 + btnOp.size.Y * 2));
            btnLoad.setPosition(new Vector2(350,400 + btnLoad.size.Y * 4));
            //this.IsMouseVisible = true;
        }
        public override void UnloadContent()
        {
            btnPlay.ButtonClicked -= HandleButtonClicked;
            btnOp.ButtonClicked -= HandleButtonClicked;
            btnLoad.ButtonClicked -= HandleButtonClicked;
            
        }
        public override void Update(GameTime gameTime)
        {
           
            MouseState mouse = Mouse.GetState();
            btnPlay.Update(mouse);
            btnOp.Update(mouse);
            btnLoad.Update(mouse);
           // btnExit.Update(mouse);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            btnPlay.Draw(spriteBatch);
            btnLoad.Draw(spriteBatch);
            btnOp.Draw(spriteBatch);
            btnLoad.Draw(spriteBatch);
        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            sender = btnPlay;
            if (sender == btnPlay)
            {

                nextScreen = "TitleScreen"; //ScreenName.GameScreen;
            }
            else if (sender == btnOp)
            {
                nextScreen = "OptionsScreen"; //ScreenName.OptionsScreen;
            }
            else if (sender == btnLoad)
            {
                nextScreen = "LoadScreen"; //ScreenName.LoadScreen;
            }
           // else if (sender == btnExit)
          //  {
           //     System.Environment.Exit(1);
          //  }
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
