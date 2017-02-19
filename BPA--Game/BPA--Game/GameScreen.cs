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
    public enum Direction
    {
        up,
        down,
        left,
        right,
    }
    public class GameScreen:Screen
    {
        // Work Here Ryan. Add Player and Battle Scene
        protected const int NUMCOL = 2;
        protected const int NUMROW = 2;
        

        protected Level[,] levelArray = new Level[NUMCOL, NUMROW];

        protected mButton btnPlay;
        protected mButton btnLoad;
        protected mButton btnOp;
        protected mButton btnExit;

        protected Texture2D background;
        protected Texture2D towerBuilding;
        protected Texture2D waterFountain;
        protected Texture2D shopStore;

        protected GraphicsDeviceManager graphics;
        
        SpriteFont Font;
        protected SpriteBatch spriteBatch;
        protected List<EnemyAI> enemies;
        protected Player player;
        protected Buildings mainTower;
        protected Buildings waterPool;
        protected SpriteFont tutorialHelp;

        //ScreenName nextScreen;
        protected int screenWidth;
        protected int screenHeight;

        protected Entity upTransitionRect;
        protected Entity leftTransitionRect;
        protected Entity rightTransitionRect;
        protected Entity downTransitionRect;

        protected int enemyCollisionIndex;

        protected List<Buildings> buildings = new List<Buildings>();
        protected List<Texture2D> buildingTextures = new List<Texture2D>();
        protected ContentManager content;
        protected bool inLevelDescription;
        protected bool playerDescription;
        protected int currentRow;
        protected int currentCol;
        public bool newGame;

        public struct Level
        {

            public string background;
            public List<string> buildingName;
            public List<int> buildingX, buildingY;
            
        }

        public GameScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
            newGame = true;
            //LoadContent();
            //Initialize();
            inLevelDescription = false;
            player = new Player();
            ReadFile();
            currentRow = 0;
            currentCol = 0;

        }


        /// <summary>
        /// Makes sure that the player stays in the spot where he left off when pausing screens or in actions
        /// </summary>
        public void WriteSave(int eHealth, int eStrength)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("SaveData"))
            {
                //writing the players attributes
                
                file.WriteLine(player.position.X);
                file.WriteLine(player.position.Y);
                file.WriteLine(player.GetHealth());//Writes the players Health to a file
                file.WriteLine(player.GetStrength());//Writes the players strength to a file
                file.WriteLine(player.GetDefense());// Writes the players defense to a file
                file.WriteLine(player.GetCoins());
                file.WriteLine(player.HealPotion());
                file.WriteLine(player.GetenemiesKilled());
                //writing enemy attributes and position 
                for (int i = 0; i < enemies.Count; i++)
                {

                    if (i != enemyCollisionIndex)
                    {
                        file.WriteLine(enemies[i].position.X);
                        file.WriteLine(enemies[i].position.Y);
                        file.WriteLine("Blue Left Movement");//FIXME
                    }
                }
                file.WriteLine(eStrength);
                file.WriteLine(eHealth);
                newGame = false;
            }
        }
        /// <summary>
        /// Loads the save file that contains the player postitons  he left off on when pausing screens or in battle
        /// </summary>
        public void ReadSaveFile()
        {
           
            using (System.IO.StreamReader file = new System.IO.StreamReader("SaveData"))
            {
                //reads all the players attributes in the SaveData file
                
                player.position.X = Convert.ToInt32(file.ReadLine());
                player.position.Y = Convert.ToInt32(file.ReadLine());
                player.playerHealth = Convert.ToInt32(file.ReadLine());
                player.playerstr = Convert.ToInt32(file.ReadLine());
                player.playerdef = Convert.ToInt32(file.ReadLine());
                player.coins = Convert.ToInt32(file.ReadLine());
                player.healthPotion = Convert.ToInt32(file.ReadLine());
                player.enemieskilled = Convert.ToInt32(file.ReadLine());
                string line;
                while (( line = file.ReadLine()) != null)
                {
                    int xPos = Convert.ToInt32(line);
                    int yPos = Convert.ToInt32(file.ReadLine());
                    if ((line = file.ReadLine()) == null) { break; }
                    string texture  = line;//FIXME
                    enemies.Add(new EnemyAI(xPos, yPos));
                }
            }
          
            newGame = true;
           
        }
         
        /// <summary>
        /// Reads the levels file to create the levels and building within the file
        /// </summary>
        public void ReadFile()
        {
            // string currentDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            using (System.IO.StreamReader file = new System.IO.StreamReader("Levels.txt"))
            {
                string line;
                Level myLevel = new Level();
                myLevel.buildingName = new List<string>();
                myLevel.buildingX = new List<int>();
                myLevel.buildingY = new List<int>();
                while ((line = file.ReadLine()) != null)
                {
                    if (line.StartsWith("Start"))
                    {
                        inLevelDescription = true;
                    }

                    if (inLevelDescription)
                    {
                        int Row = Convert.ToInt32(file.ReadLine());
                        int Col = Convert.ToInt32(file.ReadLine());
                        myLevel.background = file.ReadLine();
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.StartsWith("End"))
                            {
                                inLevelDescription = false;
                                break;
                            }
                            myLevel.buildingName.Add(line);
                            myLevel.buildingX.Add(Convert.ToInt32(file.ReadLine()));
                            myLevel.buildingY.Add(Convert.ToInt32(file.ReadLine()));

                        }
                        levelArray[Row, Col] = myLevel;
                        myLevel = new Level();
                        myLevel.buildingName = new List<string>();
                        myLevel.buildingX = new List<int>();
                        myLevel.buildingY = new List<int>();
                    }
                }
                //This reads the file f levels to load in the background and buildings that are put into a list
            }
              
        }
        public override void LoadContent(ContentManager ContentMgr,GraphicsDeviceManager graphics)
        {
            content = ContentMgr;
            //spriteBatch = new SpriteBatch(GraphicsDevice);
          
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            leftTransitionRect = new Entity(0, 0, 1, screenHeight);
            rightTransitionRect = new Entity(screenWidth - 1, 0, 1, screenHeight);
            upTransitionRect = new Entity(0,0,screenWidth, 1);
            downTransitionRect = new Entity(0, screenHeight - 1, screenWidth, 1);
            tutorialHelp = content.Load<SpriteFont>("TutorialHelp");
            Font = content.Load<SpriteFont>("HealthFont");
            player.LoadContent(content);

            LoadLevel(content);
           // health = new List<Health>();
 
            //this.IsMouseVisible = true;
        }

        /// <summary>
        /// Makes sure that when it loads a level that the enemies are not within the buildings and that the player does not hit the enemies on spawn
        /// </summary>
        /// <param name="content"></param>
        public void LoadLevel(ContentManager content)
        {
            Random rand = new Random();
           // List<EnemyAI> removeEnemy = new List<EnemyAI>();
            enemies = new List<EnemyAI>();
            background = content.Load<Texture2D>(levelArray[currentRow, currentCol].background);

            for (int i = 0; i < levelArray[currentRow, currentCol].buildingName.Count; i++)
            {
                buildingTextures.Add(content.Load<Texture2D>(levelArray[currentRow, currentCol].buildingName[i]));
                buildings.Add(new Buildings(buildingTextures[i], new Vector2(levelArray[currentRow, currentCol].buildingX[i], levelArray[currentRow, currentCol].buildingY[i])));
            }
            if (newGame) {

                for (int i = 0; i < 2; i++)
                {
                    bool goodStart = false;
                    int startX = 0;
                    int startY = 0;
                    
                    while (!goodStart)
                    {
                        startX = rand.Next(10, 600);
                        startY = rand.Next(10, 600);

                        float playerRight = player.position.X + player.Width + 100;
                        float playerLeft = player.position.X;
                        float playerTop = player.position.Y;
                        float playerBottom = player.position.Y + player.Height + 100;
                        float enemyRight = startX + 120;
                        float enemyLeft = startX;
                        float enemyTop = startY;
                        float enemyBottom = startY + 120;
                        bool buildingCheck = true;
                        EnemyAI collEnemy = new EnemyAI(startX, startY);
                        foreach (Buildings checkBuilding in buildings)
                        {
                            if (checkBuilding.Collision(collEnemy))
                            {
                                buildingCheck = false;
                            }
                          
                        }
                        if ((enemyRight < playerLeft ||
                            enemyLeft > playerRight ||
                            enemyTop > playerBottom ||
                            enemyBottom < playerTop)&& buildingCheck
                            )

                        {

                            goodStart = true;
                            
                        }
                        
                    }

                    int enemySeed = rand.Next(0, 5000);                    
                    enemies.Add(new EnemyAI(startX, startY));
                    

                }
            }
            else {
                ReadSaveFile();
               
            }
            
            if (player.enemieskilled%5 == 4)
            {
                enemies.Clear();
                enemies.Add(new EnemyBoss(0, 0));
             }
            foreach (EnemyAI enemy in enemies)
            {
                enemy.LoadContent(content);
            }
            enemyCollisionIndex = enemies.Count();
           
        }
        public override void UnloadContent()
        {
          
            foreach(EnemyAI enemy in enemies)
            {
                enemy.UnloadContent();
            }
            foreach (Buildings building in buildings)
            {
                building.UnloadContent();              
            }
            buildings.Clear();
            buildingTextures.Clear();
            enemies.Clear();
            
        }
        public void ScreenTransfer(int currentCol, int currentRow)
        {
            UnloadContent();
            LoadLevel(content);            

        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ChangeScreen("PauseScreen",0,0);

            }
            if (Keyboard.GetState().IsKeyDown(Keys.CapsLock))
            {
                ChangeScreen("InventoryScreen",0,0);
            }
            
            MouseState mouse = Mouse.GetState();
            player.Update(gameTime);
            //transitons to next screen if player collides left;
            if (player.Collision(leftTransitionRect))
            {
                currentCol = currentCol - 1;
                if (currentCol < 0)
                {
                    currentCol = NUMCOL -1;
                }
                player.position.X = 740;
                ScreenTransfer(currentRow, currentCol);
            }
            //transitons to next screen if player collides right;
            if (player.Collision(rightTransitionRect))
            {
                currentCol = currentCol + 1;
                if (currentCol >= NUMCOL)
                {
                    currentCol = 0;
                }
                player.position.X = 1;
                ScreenTransfer(currentRow, currentCol);
            }
            //transitons to next screen if player collides up;
            if (player.Collision(upTransitionRect))
            {
                currentRow = currentRow - 1;
                if (currentRow < 0)
                {
                    currentRow = NUMROW - 1;
                }
                player.position.Y = 630;
                ScreenTransfer(currentRow, currentCol);
            }
            //transitons to next screen if player collides down;
            if (player.Collision(downTransitionRect))
            {
                currentRow = currentRow + 1;
                if (currentRow >= NUMROW)
                {
                    currentRow = 0;
                }
                player.position.Y = 1;
                ScreenTransfer(currentRow, currentCol);
            }

            //players collision for a building
            for (int i = 0; i < buildings.Count; i++)
            {
                if (player.Collision(buildings[i]))
                {
                    player.position = player.oldPosition;
                    if (currentRow == 0 && currentCol == 1)
                    {
                        ChangeScreen("ShopScreen", 0,0);
                        
                    }
                }
            }
            
            for(int i = 0; i < enemies.Count; i++)
            {
              
                enemies[i].Update(gameTime,player);
                if (enemies[i].Collision(player))
                {
                    enemyCollisionIndex = i; 

                    ChangeScreen("BattleScreen", enemies[i].GetEnemyStrength(),enemies[i].GetEnemyHealth());
                   
                }
                for (int x = 0; x < buildings.Count; x++)
                {
                    if (enemies[i].Collision(buildings[x]))
                    {
                        
                        enemies[i].position = enemies[i].oldPosition;
                    }
                } 
            }
            
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 700), Color.White);
            for(int i = 0; i < buildings.Count; i++)
            {
                 buildings[i].Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
  
            foreach(EnemyAI enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            spriteBatch.DrawString(Font, "Health: " + player.playerHealth, new Vector2(700, 0), Color.DarkBlue);
            spriteBatch.DrawString(Font, "Coins: " + player.coins, new Vector2(600, 0), Color.Yellow);
            spriteBatch.DrawString(Font, "Enimies Killied: " + player.enemieskilled, new Vector2(400, 0), Color.OrangeRed);
           
            //GraphicsDevice.Clear(Color.CornflowerBlue);
        }
        public void ChangeScreen(string NextScreen,int strength, int health)
        {
            WriteSave(health,strength);
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
