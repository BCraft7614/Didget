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
    public class ShopScreen: Screen

    {
        Player player = new Player();
        int screenWidth;
        int screenHeight;
        Texture2D backGround;
        mButton potionBottle;
        mButton btnBack;
        public ShopScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
        }
        public override void LoadContent(ContentManager ContentMgr, GraphicsDeviceManager graphics)
        {
            backGround = ContentMgr.Load<Texture2D>("ShopGround");
            potionBottle = new mButton(ContentMgr.Load<Texture2D>("Bottle"), graphics.GraphicsDevice);
            btnBack = new mButton(ContentMgr.Load<Texture2D>("BtnBack"), graphics.GraphicsDevice);
            potionBottle.setPosition(new Vector2(350, 300));
            btnBack.setPosition(new Vector2(350, 300 + btnBack.size.Y * 8));
            potionBottle.ButtonClicked += HandleButtonClicked;
            btnBack.ButtonClicked += HandleButtonClicked;
        }
        public override void UnloadContent()
        {
            potionBottle.ButtonClicked -= HandleButtonClicked;
            btnBack.ButtonClicked -= HandleButtonClicked;
        }
        public override void Update(GameTime theTime)
        {
            potionBottle.Update();
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(backGround, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            potionBottle.Draw(spriteBatch);
        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            if(sender == potionBottle)
            {
                player.coins -= 10;
                player.healthPotion++;
            }
            if(sender == btnBack)
            {
                ChangeScreen("TitleScreen");
            }
            OnButtonClicked();
        }

        public void ChangeScreen(string NextScreen)
        {
           
            nextScreen = NextScreen;
            OnButtonClicked();
        }
        public EventHandler ButtonClicked;
        public void OnButtonClicked()
        {
            if (ButtonClicked != null)
            {
                ButtonClicked(this, EventArgs.Empty);

            }
        }

    }
}
