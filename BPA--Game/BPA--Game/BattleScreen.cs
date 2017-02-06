﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.IO;


namespace BPA__Game
{
    class BattleScreen : Screen
    {
        int screenWidth;
        int screenHeight;
        Texture2D background;
        mButton fightButton;
        mButton FleeButton;
        mButton specialButton;
        mButton itemButton;
        mButton selected;
        Random rand = new Random();
        EnemyAI enemyAI = new EnemyAI(20,20);
        Texture2D battleEnemy;
        Texture2D battleEnemy2;
        Texture2D fistAnimation;
        Texture2D heartAnime;
        Texture2D animationTexture;
        Vector2 animationPosition;

        public int enemyHealth;
        public int playerHealth;
        public int playerStength;
        public int playerDefense;
        public int playerCoins;

        private bool playersTurn;
        private bool enemyTurn;
        private bool playersAnimation;
        private bool enemyAnimation;
        private int animationCount;
        private bool fightValid;
        private int enemyStrength;
        private int enemyDefense;
      
        
        private enum actionType{HEAL,ATTACK,RUN,SPECIAL};
        private actionType action;
      
        SpriteFont enemyHealthFont;
        SpriteFont HealthFont;
        public BattleScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
            enemyHealth = 100;
            playersTurn = false;
            fightValid = false;
            enemyTurn = false;
            enemyAnimation = false;
            playersAnimation = false;
            animationCount = 0;
            enemyStrength = rand.Next(1, 10);
            enemyDefense = rand.Next(1, 10);
        }
          
        
        public void ReadSave()
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader("SaveData"))
            {
                file.ReadLine(); //player positionX
                file.ReadLine(); //player positionY
                playerHealth = Convert.ToInt32(file.ReadLine());
                playerStength = Convert.ToInt32(file.ReadLine());
                playerDefense = Convert.ToInt32(file.ReadLine());
                playerCoins = Convert.ToInt32(file.ReadLine()); 
                
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
                writeFile.WriteLine(playerStength);
                writeFile.WriteLine(playerDefense);
                writeFile.WriteLine(playerCoins);
                for (int i = 0; i < 4; i++)
                {
                    readFile.ReadLine();
                }
                string line;
                while ((line = readFile.ReadLine()) != null)
                {
                    writeFile.WriteLine(line);
                }

            }

            //Checks to see if Sava Data File Exists and the delets and Move the Save Data
            writeFile.Close();
            if (File.Exists("SaveData"))
            {
                File.Delete("SaveData");
            }
            File.Move("tempFile", "SaveData");

        }

        public override void LoadContent(ContentManager ContentMgr, GraphicsDeviceManager graphics)
        {
            //All of our animation and buttins are stored into LoadContent
            fightButton = new mButton(ContentMgr.Load<Texture2D>("BtnFight"), graphics.GraphicsDevice, (o, e) => selected = fightButton);

            itemButton = new mButton(ContentMgr.Load<Texture2D>("BtnItem"), graphics.GraphicsDevice, (o, e) => selected = itemButton);

            specialButton = new mButton(ContentMgr.Load<Texture2D>("BtnSpecial"), graphics.GraphicsDevice, (o, e) => selected = specialButton);

            FleeButton = new mButton(ContentMgr.Load<Texture2D>("btnLoad"), graphics.GraphicsDevice, (o, e) => selected = FleeButton);

            battleEnemy = ContentMgr.Load<Texture2D>("BattlePen");
            battleEnemy2 = ContentMgr.Load<Texture2D>("BattleCen");
            fistAnimation = ContentMgr.Load<Texture2D>("FistAnimation");
            heartAnime = ContentMgr.Load<Texture2D>("Heart");
            enemyHealthFont = ContentMgr.Load<SpriteFont>("HealthFont");
            HealthFont = ContentMgr.Load<SpriteFont>("HealthFont");
            fightButton.ButtonClicked += HandleButtonClicked;
            itemButton.ButtonClicked += HandleButtonClicked;
            specialButton.ButtonClicked += HandleButtonClicked;
            fightButton.setPosition(new Vector2(80,550));
            itemButton.setPosition(new Vector2(80,650));
            specialButton.setPosition(new Vector2(450, 630));
            background = ContentMgr.Load<Texture2D>("BattleScreen");
            ReadSave();
            
            base.LoadContent(ContentMgr, graphics);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 700), Color.White);
            spriteBatch.DrawString(HealthFont, "Health:" + playerHealth.ToString(), new Vector2(689, 330), Color.Green);
            spriteBatch.DrawString(enemyHealthFont, "Health:" + enemyHealth.ToString(), new Vector2(650, 50), Color.Red);
            spriteBatch.Draw(battleEnemy2, new Vector2(660,70), Color.White);
            spriteBatch.Draw(battleEnemy, new Vector2(660, 70), Color.White);
            DrawAnimation(spriteBatch);
            fightButton.Draw(spriteBatch);
            itemButton.Draw(spriteBatch);
            specialButton.Draw(spriteBatch);
        }

        public override void Update(GameTime theTime)
        {
            fightButton.Update();
            itemButton.Update();
            specialButton.Update();
            FleeButton.Update();

            FightUpdate();
            EnemyFightUpdate();
            base.Update(theTime);
        }
     
        //Should Call Invertory class if Item button is pressed
        private void ItemUpdate(GameTime theTime)
        {
            
        }


        //Should call FightActionClass
        private void FightUpdate()
        {
            if (playersTurn & fightValid)
            {
                if (action == actionType.ATTACK) { PlayerGivesDamage(); }
                else if(action == actionType.HEAL) { }
                else if(action == actionType.SPECIAL) { }
                
                if (enemyHealth <= 0)
                {
                    playerCoins = playerCoins + 5;
                    ChangeScreen("GameScreen");
                    
                    enemyHealth = 100;
                }
                playersTurn = false;
                playersAnimation = true;
            }
        }
        private void EnemyFightUpdate()
        {
            if(enemyTurn && fightValid)
            {
                int TypeOfAction = rand.Next(2);
                if (TypeOfAction == 0)
                {
                    action = actionType.HEAL;
                    animationTexture = heartAnime;
                    animationPosition = new Vector2(660, 70);
                    enemyHealth += rand.Next(1, 5);

                }
                if (TypeOfAction == 1)
                {
                    action = actionType.ATTACK;
                    animationTexture = fistAnimation;
                    animationPosition = new Vector2(110, 300);
                    int dmg = enemyStrength - playerDefense;
                    if (dmg < 0)
                    {
                        dmg = 0;
                    }
                    dmg += rand.Next(0, 5);
                    playerHealth = playerHealth - dmg;
                }

                enemyTurn = false;
                enemyAnimation = true;
            }
            
        }
        private void DrawAnimation(SpriteBatch spriteBatch)
        {
            if (fightValid)
            {
                
                if (playersAnimation)
                {
                    spriteBatch.Draw(animationTexture, animationPosition, Color.White);
                    animationCount++;
                    if(animationCount > 30)
                    {
                        animationCount = 0;
                        enemyTurn = true;
                        playersAnimation = false;
                    }
                }
                if (enemyAnimation)
                {
                    spriteBatch.Draw(animationTexture, animationPosition, Color.White);
                    animationCount++;
                    if (animationCount > 30)
                    {
                        animationCount = 0;
                        enemyAnimation = false;
                        fightValid = false;
                    }
                }
            }
        }
        //Should loada SpecialAblitlyClass
        private void SpecialUpdate(GameTime theTime)
        {

        }


        //Should load RunClass but it doesnt work
        private void FleeUpdate(GameTime theTime)
        {

        }

        public void PlayerGivesDamage()
        {
            int dmg = playerStength - enemyDefense;
            if (dmg < 0)
            {
                dmg = 0;
            }
            dmg += rand.Next(0, 5);
            enemyHealth = enemyHealth - dmg;
        }
        
      
        public override void UnloadContent()
        {
            fightButton.ButtonClicked -= HandleButtonClicked;
            itemButton.ButtonClicked -= HandleButtonClicked;
            specialButton.ButtonClicked -= HandleButtonClicked;
            base.UnloadContent();
        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            sender = fightButton;
            if (!fightValid)
            {
                fightValid = true;
                playersTurn = false;
                enemyTurn = false;
                playersAnimation = true;
                enemyAnimation = false;
                if (sender == fightButton)
                {
                    playersTurn = true;
                    action = actionType.ATTACK;
                    animationTexture = fistAnimation;
                    animationPosition = new Vector2(660, 70);
                }
                else if (sender == itemButton)
                {
                    
                }
                else if (sender == specialButton)
                {
                    
                }
            }
           
        }
        public void ChangeScreen(string NextScreen)
        {
            WriteSave();
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
