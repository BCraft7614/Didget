using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPA__Game
{
    public class TitleScreen : Screen
    {
        mButton btnPlay;
        mButton btnLoad;
        mButton btnOp;
        mButton btnExit;
        Texture2D rightanim;
        Texture2D background;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int screenWidth;
        int screenHeight;

      
        public TitleScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
           // LoadContent();
           // Initialize();
        }
 

        public override void LoadContent(ContentManager ContentMgr, GraphicsDeviceManager graphics)
        {
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            btnPlay = new mButton(ContentMgr.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnOp = new mButton(ContentMgr.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnLoad = new mButton(ContentMgr.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnExit = new mButton(ContentMgr.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnPlay.ButtonClicked += HandleButtonClicked;
            btnOp.ButtonClicked += HandleButtonClicked;
            btnLoad.ButtonClicked += HandleButtonClicked;
            btnPlay.setPosition(new Vector2(350, 300));
            btnOp.setPosition(new Vector2(350, 300 + btnOp.size.Y * 2));
            btnLoad.setPosition(new Vector2(350, 300 + btnLoad.size.Y * 4));
            btnExit.setPosition(new Vector2(350, 300 + btnLoad.size.Y * 8));
            
          //  this.IsMouseVisible = true;
        }
        public override void UnloadContent()
        {
            btnPlay.ButtonClicked -= HandleButtonClicked;
            btnOp.ButtonClicked -= HandleButtonClicked;
            btnLoad.ButtonClicked -= HandleButtonClicked;
           // base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {

            MouseState mouse = Mouse.GetState();
            btnPlay.Update(mouse);
            btnOp.Update(mouse);
            btnLoad.Update(mouse);
            btnExit.Update(mouse);

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
            if (sender == btnPlay)
            {
                nextScreen = "GameScreen"; //ScreenName.GameScreen;
            }
            else if (sender == btnOp)
            {
                nextScreen = "OptionsScreen";//ScreenName.OptionsScreen;
            }
            else if(sender == btnLoad)
            {
                nextScreen = "LoadScreen"; //ScreenName.LoadScreen;
            }
            else if(sender == btnExit)
            {
                System.Environment.Exit(1);
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

