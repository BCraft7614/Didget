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
using System.IO;

namespace BPA__Game
{
    public class ShopScreen : Screen

    {
   
        private int screenWidth;
        private int screenHeight;
        private Texture2D backGround;
        private Texture2D potionBottle;
        private mButton btnBack;
        private mButton btnBuyP;
        private mButton btnBuyF;
        private SpriteFont shopText;
        public Texture2D brassKnuckle;
        public int playerCoins;
        public int playerHealth;
        public int playerDefense;
        public int playerStrength;
        public int healPotions;
        public int enemiesKilled;
        public bool fistUpgrade;
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
            btnBuyP = new mButton(ContentMgr.Load<Texture2D>("BtnBuy"), graphics.GraphicsDevice);
            btnBuyF = new mButton(ContentMgr.Load<Texture2D>("BtnBuy"), graphics.GraphicsDevice);
            brassKnuckle = ContentMgr.Load<Texture2D>("BrassKnuckles");
            //FistUpgrade = new 
            //potionBottle.setPosition(new Vector2(350, 300));
            btnBuyP.setPosition(new Vector2(350, 300 + btnBuyP.size.Y * 8));
            btnBuyF.setPosition(new Vector2(379, 300 + btnBuyF.size.Y * 8));
            ////potionBottle.ButtonClicked += HandleButtonClicked;
            shopText = ContentMgr.Load<SpriteFont>("TutorialHelp");
            btnBack.ButtonClicked += HandleButtonClicked;
            btnBuyP.ButtonClicked += HandleButtonClicked;
            btnBuyF.ButtonClicked += HandleButtonClicked;
            ReadSave();
        }

        public override void UnloadContent()
        {
            //otionBottle.ButtonClicked -= HandleButtonClicked;
            btnBack.ButtonClicked -= HandleButtonClicked;
            btnBuyP.ButtonClicked -= HandleButtonClicked;
            btnBuyF.ButtonClicked -= HandleButtonClicked;
        }

        public override void Update(GameTime theTime)
        {

            //potionBottle.Update();
            btnBack.Update();
            btnBuyP.Update();
            btnBuyF.Update();



        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(backGround, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            spriteBatch.Draw(potionBottle, new Vector2(350, 300), Color.White);
            spriteBatch.Draw(brassKnuckle, new Vector2(379, 300), Color.White);
            btnBack.Draw(spriteBatch);
            btnBuyP.Draw(spriteBatch);
            btnBuyF.Draw(spriteBatch);
            if (NoCoins)
            {
                spriteBatch.DrawString(shopText, "Not enough coins", new Vector2(100, 100), Color.DarkGoldenrod);

            }
            spriteBatch.DrawString(shopText, "Coins: " + playerCoins, new Vector2(700, 0), Color.Gold);

        }

        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {

            if (sender == btnBuyP)
            {
                if (playerCoins >= 10)
                {
                    playerCoins -= 10;
                    healPotions++;

                }
                else if (playerCoins < 10)
                {
                    NoCoins = true;

                }
            }
            if (sender == btnBack)
            {
                NoCoins = false;
                ChangeScreen("GameScreen");

            }
            if (sender == btnBuyF)
            {
                if (playerCoins >= 30)
                {
                    playerCoins -= 30;
                    fistUpgrade= true;
                }
                else if (playerCoins < 30)
                {
                    NoCoins = true;
                    fistUpgrade = false;

                }
            }

        }

        public void ChangeScreen(string NextScreen)
        {

            nextScreen = NextScreen;
            WriteSave();
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
      
        public void ReadSave()
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader("SaveData"))
            {
                file.ReadLine(); //player positionX
                file.ReadLine(); //player positionY
                playerHealth = Convert.ToInt32(file.ReadLine());
                playerStrength = Convert.ToInt32(file.ReadLine());
                playerDefense = Convert.ToInt32(file.ReadLine());
                playerCoins = Convert.ToInt32(file.ReadLine());
                healPotions = Convert.ToInt32(file.ReadLine());
                enemiesKilled = Convert.ToInt32(file.ReadLine());
                string line = file.ReadLine();
                fistUpgrade = Convert.ToBoolean(line);
            }
        }

        public void WriteSave()
        {
            //Write the save file Save File into another file witch is called TempFile
            var writeFile = new StreamWriter("tempFile");
            using (StreamReader readFile = new StreamReader("SaveData"))
            {
                writeFile.WriteLine(readFile.ReadLine());
                writeFile.WriteLine(readFile.ReadLine());
                writeFile.WriteLine(playerHealth);
                writeFile.WriteLine(playerStrength);
                writeFile.WriteLine(playerDefense);
                writeFile.WriteLine(playerCoins);
                writeFile.WriteLine(healPotions);
                writeFile.WriteLine(enemiesKilled);
                writeFile.Write(fistUpgrade);              
                for (int i = 0; i < 6; i++)
                {
                    readFile.ReadLine();
                }
                string line;
                while ((line = readFile.ReadLine()) != null)
                {
                    writeFile.WriteLine(line);
                }

            }
            writeFile.Close();
            if (File.Exists("SaveData"))
            {
                File.Delete("SaveData");
            }
            File.Move("tempFile", "SaveData");
        }
    }
}
