using System;
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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace BPA__Game
{
    public class BattleScreen : Screen
    {
        protected int screenWidth;
        protected int screenHeight;
        protected Texture2D background;
        protected mButton fightButton;
        protected mButton FleeButton;
        protected mButton specialButton;
        protected mButton itemButton;
        protected mButton selected;
        protected Random rand = new Random();
        protected EnemyAI enemyAI = new EnemyAI(20,20);
        protected Texture2D battleEnemy;
        protected Texture2D battleEnemy2;
        protected Texture2D fistAnimation;
        protected Texture2D heartAnime;
        protected Texture2D fireball;
        protected Texture2D animationTexture;
        protected Texture2D PotionWarn;
        protected Texture2D brassKnuckle;
        protected Vector2 animationPosition;
        protected Vector2 enemyPos = new Vector2(660, 70);
        protected Vector2 playerPos = new Vector2(110, 300);
        public int enemyHealth;
        public int playerHealth;
        public int playerStength;
        public int playerDefense;
        public int playerCoins;
        public int healPotion;
        public int enemiesKilled;
        public int fistUpgrade;
        private bool playersTurn;
        private bool enemyTurn;
        private bool playersAnimation;
        private bool enemyAnimation;
        private int animationCount;
        private bool fightValid;
        private int enemyStrength;
        private int enemyDefense;
        private int attackpts;
        
        SoundEffect punchSound;
        SoundEffect healingSound;
        private enum actionType{HEAL,ATTACK,RUN,SPECIAL};
        private actionType action;

        protected SpriteFont enemyHealthFont;
        protected SpriteFont HealthFont;
        protected SpriteFont tutorialHelp;
        private int deathtime = 0;
        private bool death = false;

        private int amountOfCoins;

       public BattleScreen()
        {
            PlayersInitialize();
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
                healPotion = Convert.ToInt32(file.ReadLine());
                enemiesKilled = Convert.ToInt32(file.ReadLine());
                fistUpgrade = Convert.ToInt32(file.ReadLine());
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    enemyStrength = Convert.ToInt32(line);
                    enemyDefense = enemyStrength;
                    enemyHealth = Convert.ToInt32(file.ReadLine());
                    if ((line = file.ReadLine()) == null) { break; }
                    string tempString = line;//FIXME
                }

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
                writeFile.WriteLine(healPotion);
                writeFile.WriteLine(enemiesKilled);
                writeFile.WriteLine(fistUpgrade);
                for (int i = 0; i < 7; i++)
                {
                    readFile.ReadLine();
                }
                string line;
                while ((line = readFile.ReadLine()) != null)
                {
                    writeFile.WriteLine(line);
                }

            }

            //Checks to see if Sava Data File Exists and the deletes and Move the Save Data
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
            fireball = ContentMgr.Load<Texture2D>("Fireball");
            brassKnuckle = ContentMgr.Load<Texture2D>("BrassKnuckles");
           
            enemyHealthFont = ContentMgr.Load<SpriteFont>("HealthFont");
            HealthFont = ContentMgr.Load<SpriteFont>("HealthFont");
            fightButton.ButtonClicked += HandleButtonClicked;
            itemButton.ButtonClicked += HandleButtonClicked;
            specialButton.ButtonClicked += HandleButtonClicked;
            fightButton.setPosition(new Vector2(80,550));
            itemButton.setPosition(new Vector2(80,650));
            specialButton.setPosition(new Vector2(450, 630));
            background = ContentMgr.Load<Texture2D>("BattleScreen");
            tutorialHelp = ContentMgr.Load<SpriteFont>("TutorialHelp");
            song = ContentMgr.Load<Song>("BattleMusic");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            punchSound = ContentMgr.Load<SoundEffect>("Punching");
            healingSound = ContentMgr.Load <SoundEffect>("Healing");
            ReadSave();
            
            base.LoadContent(ContentMgr, graphics);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 700), Color.White);
            spriteBatch.DrawString(HealthFont, "Health:" + playerHealth.ToString(), new Vector2(590, 370), Color.Green);
            spriteBatch.DrawString(enemyHealthFont, "Health:" + enemyHealth.ToString(), new Vector2(650, 50), Color.Red);
            spriteBatch.Draw(battleEnemy2, new Vector2(660,70), Color.White);
            spriteBatch.Draw(battleEnemy, new Vector2(660, 70), Color.White);
            spriteBatch.DrawString(HealthFont, "Special Points: " + attackpts, new Vector2(590,390), Color.Black);
            spriteBatch.DrawString(HealthFont, "Healing Potions: x" + healPotion, new Vector2(590, 520), Color.PaleVioletRed);
            DrawAnimation(spriteBatch);
            fightButton.Draw(spriteBatch);
            itemButton.Draw(spriteBatch);
            specialButton.Draw(spriteBatch);
            if (death)
            {
                spriteBatch.DrawString(HealthFont, "YOU DIED", new Vector2(400, 350), Color.Red);
                
            }
            


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
        public void FightUpdate()
        {   
            if (playerHealth <= 0)
            {
                death = true;
                deathtime++;
                if (deathtime >= 100)
                {
                    ExitGame = true;
                }
                
            }
            else if (playersTurn & fightValid)
            {
                if (action == actionType.ATTACK) { PlayerGivesDamage(); }
                else if(action == actionType.HEAL) { PlayerHeals(); }
                else if(action == actionType.SPECIAL) { PlayerSpecial(); }
                
                if (enemyHealth <= 0)
                {
                    playerCoins = playerCoins + amountOfCoins;
                    enemiesKilled++;
                    ChangeScreen("GameScreen");
                    PlayersInitialize();
                    enemyHealth = 100;
                }

                playersTurn = false;
                playersAnimation = true;
            }
        }
        public void EnemyFightUpdate()
        {
            if(enemyTurn && fightValid)
            {
                int TypeOfAction = rand.Next(3);
                if (TypeOfAction == 0)
                {
                    action = actionType.HEAL;
                    animationTexture = heartAnime;
                    animationPosition = enemyPos;
                    enemyHealth += rand.Next(1, 5);

                }
                if (TypeOfAction >= 1)
                {
                    action = actionType.ATTACK;
                    animationTexture = fistAnimation;
                    animationPosition = playerPos;
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
        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            if (fightValid)
            {
                
                if (playersAnimation)
                {// displays the player fight animation
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
        protected void SpecialUpdate(GameTime theTime)
        {

        }


        //Should load RunClass but it doesnt work
        protected void FleeUpdate(GameTime theTime)
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
            if (fistUpgrade == 1)
            {
                dmg += 5;
            }
            enemyHealth = enemyHealth - dmg;
        }

        public void PlayerSpecial()
        {
            
            if(attackpts >= 20)
            {
                int dmg= 40;
                enemyHealth = enemyHealth - dmg;
                attackpts -= 20;
            }
            

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
            //sender = fightButton;
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
                    animationPosition = enemyPos;
                    attackpts = attackpts + 2;
                    punchSound.Play();

                    if (fistUpgrade == 1)
                    {
                        animationTexture = brassKnuckle;
                       
                    }
                      
                }
                else if (sender == itemButton)
                {
                    playersTurn = true;
                    action = actionType.HEAL;
                    animationTexture = heartAnime;
                    animationPosition = playerPos;
      
                }
                else if (sender == specialButton)
                {
                    if(attackpts >= 20)
                    {
                        playersTurn = true;
                        action = actionType.SPECIAL;
                        animationTexture = fireball;
                        animationPosition = enemyPos;

                    }

                }
            }
           
        }
        public void PlayersInitialize()
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
            amountOfCoins = rand.Next(10, 15);
        }
        public void PlayerHeals()
        {

            if (healPotion > 0)
            {
                if (playerHealth <= 20)
                {
                    healingSound.Play();
                    playerHealth = 100;
                }
                else
                {
                    healingSound.Play();
                    playerHealth += 20;
                }
                healPotion--;
            }
            else
            {
                fightValid = false;
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
