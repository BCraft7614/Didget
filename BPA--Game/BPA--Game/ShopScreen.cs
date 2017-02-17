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
        private int screenWidth;
        private int screenHeight;
        private Texture2D backGround;
        private Texture2D potionBottle;
        private mButton btnBack;
        private mButton btnBuy;
        private SpriteFont shopText;
        private bool NoCoins = false;

        public ShopScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
        }

        public override void LoadContent(ContentManager ContentMgr, GraphicsDeviceManager graphics)
        {
            backGround = ContentMgr.Load<Texture2D>("ShopGround");
            potionBottle = ContentMgr.Load<Texture2D>("Bottle");
            btnBack = new mButton(ContentMgr.Load<Texture2D>("BtnBack"), graphics.GraphicsDevice);
            btnBuy = new mButton(ContentMgr.Load<Texture2D>("BtnBuy"),graphics.GraphicsDevice);
            //potionBottle.setPosition(new Vector2(350, 300));
            btnBack.setPosition(new Vector2(350, 300 + btnBack.size.Y * 8));
            ////potionBottle.ButtonClicked += HandleButtonClicked;
            shopText = ContentMgr.Load<SpriteFont>("TutorialHelp");
            btnBack.ButtonClicked += HandleButtonClicked;
            btnBuy.ButtonClicked += HandleButtonClicked;
        }

        public override void UnloadContent()
        {
            //otionBottle.ButtonClicked -= HandleButtonClicked;
            btnBack.ButtonClicked -= HandleButtonClicked;
            btnBuy.ButtonClicked -= HandleButtonClicked;
        }

        public override void Update(GameTime theTime)
        {
            
            //potionBottle.Update();
            btnBack.Update();
            btnBuy.Update();

            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(backGround, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            spriteBatch.Draw(potionBottle, new Vector2(350, 300), Color.White);
            btnBack.Draw(spriteBatch);
            btnBuy.Draw(spriteBatch);
            if (NoCoins)
            {
                spriteBatch.DrawString(shopText, "Not enough coins", new Vector2(100, 100), Color.DarkGoldenrod);
                
            }
            
        }

        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            
            if(sender == btnBuy)
            {
                if(player.coins >= 10)
                { 
                    player.coins -= 10;
                    player.healthPotion++;

                }
                else if (player.coins < 10)
                {
                    NoCoins = true;
                    
                }
            }
            if(sender == btnBack)
            {
                NoCoins = false;
                ChangeScreen("GameScreen");
                
            }

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
