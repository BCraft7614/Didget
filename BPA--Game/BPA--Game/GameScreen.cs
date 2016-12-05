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
        Player player;
        //ScreenName nextScreen;
        int screenWidth;
        int screenHeight;

  
        public GameScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
            //LoadContent();
            //Initialize();

            player = new Player();
        }

        public override void LoadContent(ContentManager ContentMgr,GraphicsDeviceManager graphics)
        {
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            background = ContentMgr.Load<Texture2D>("TownGameScreen");
            player.LoadContent(ContentMgr);
           
           
            //this.IsMouseVisible = true;
        }
        public override void UnloadContent()
        {
          
            
        }
        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                nextScreen = "PauseScreen";
            }
            MouseState mouse = Mouse.GetState();
            player.Update(gameTime);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 700), Color.White);
            player.Draw(spriteBatch);
            //GraphicsDevice.Clear(Color.CornflowerBlue);
        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            /*
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
          */
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
