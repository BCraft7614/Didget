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
    public class PauseScreen:Screen
    {
        
        mButton btnResume;
        mButton btnOp;
        mButton btnExit;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int screenWidth;
        int screenHeight;
        Player player;

 
        public PauseScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
            //LoadContent();
            //Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            btnResume.Update();
            btnOp.Update();
            btnExit.Update();
            
        }

        public override void LoadContent(ContentManager ContentMgr, GraphicsDeviceManager graphics)
        {
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            btnResume = new mButton(ContentMgr.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnOp = new mButton(ContentMgr.Load<Texture2D>("OPbtn"), graphics.GraphicsDevice);
            btnExit = new mButton(ContentMgr.Load<Texture2D>("BtnExit"), graphics.GraphicsDevice);
            btnResume.ButtonClicked += HandleButtonClicked;
            btnOp.ButtonClicked += HandleButtonClicked;
            btnExit.ButtonClicked += HandleButtonClicked;
            btnResume.setPosition(new Vector2(350, 300));    
            btnOp.setPosition(new Vector2(350, 400 + btnOp.size.Y * 2));
            btnExit.setPosition(new Vector2(359, 500 + btnExit.size.Y * 2));
            
        }
        public override void UnloadContent()
        {
            btnResume.ButtonClicked -= HandleButtonClicked;
            btnOp.ButtonClicked -= HandleButtonClicked;
            btnExit.ButtonClicked -= HandleButtonClicked;
            base.UnloadContent();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            btnResume.Draw(spriteBatch);
            btnOp.Draw(spriteBatch);
            btnExit.Draw(spriteBatch);
        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            sender = btnResume;
            if (sender == btnResume)
            {
                nextScreen = "GameScreen";//ScreenNaem.GameScreen;
            }
            else if (sender == btnOp)
            {
                nextScreen = "OptionsScreen";//ScreenName.OptionsScreen;
            }
            else if (sender == btnExit)
            {
                nextScreen = "TitleScreen"; //ScreenName.Tittlescreen;
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
