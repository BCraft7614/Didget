using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPA__Game
{
    public class SplashScreen:Screen
    {
        protected int amountCount;
     
        Texture2D animationText;
 
        protected int screenWidth;
        protected int screenHeight;
        private string curScreenTexture;

        public SplashScreen(string CurScreenTexture, string NextScreen)
        {
            amountCount = 0;
            screenWidth = 800;
            screenHeight = 700;
            curScreenTexture = CurScreenTexture;
            nextScreen = NextScreen;
        }

        public override void LoadContent(ContentManager ContentMgr, GraphicsDeviceManager graphics)
        {
            animationText = ContentMgr.Load<Texture2D>(curScreenTexture);
            
        }
        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(animationText, new Rectangle(0,0,800,700), Color.White);
            
        }
        public override void Update(GameTime theTime)
        {
            amountCount++;
            if(amountCount >= 100)
            {
                amountCount = 0;
                ChangeScreen();

            }
            base.Update(theTime);
        }
        public void ChangeScreen()
        {
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

