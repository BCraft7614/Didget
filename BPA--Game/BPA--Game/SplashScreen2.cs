using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BPA__Game
{
    public class SplashScreen2:Screen
    {
        private int screenHeight;
        private int screenWidth;
        private int amountCount;
        Texture2D SplashScreen;

        public SplashScreen2()
        {
            amountCount = 0;
            screenHeight = 700;
            screenWidth = 800;
        }
        public override void LoadContent(ContentManager ContentMgr, GraphicsDeviceManager graphics)
        {
            SplashScreen = ContentMgr.Load<Texture2D>("SplashText");
        }
        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(SplashScreen, new Rectangle( 0, 0, screenWidth, screenHeight), Color.White);
        }
        public override void Update(GameTime theTime)
        {
            amountCount++;
            if(amountCount >= 100)
            {
                amountCount = 0;
                ChangeScreen("TitleScreen");
            }
        }
        public void ChangeScreen(string NextScreen)
        {

            nextScreen = NextScreen;
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
