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
        const int NUMCOL = 2;
        
        Level[,] levelArray = new Level[1, 2];
        mButton btnPlay;
        mButton btnLoad;
        mButton btnOp;
        mButton btnExit;
        Texture2D background;
        Texture2D towerBuilding;
        Texture2D waterFountain;
        Texture2D shopStore;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<EnemyAI> enemies;
        //private List<Health> health;
        Player player;
        Buildings mainTower;
        Buildings waterPool;
        //ScreenName nextScreen;
        int screenWidth;
        int screenHeight;
        Entity leftTransitionRect;
        Entity rightTransitionRect;
        List<Buildings> buildings = new List<Buildings>();
        List<Texture2D> buildingTextures = new List<Texture2D>();
        bool inLevelDescription;

        int currentRow;
        int currentCol;
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
            //LoadContent();
            //Initialize();
            inLevelDescription = false;
            player = new Player();
            ReadFile();
            currentRow = 0;
            currentCol = 0;

        }

        public void ReadFile()
        {
            string currentDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            System.IO.StreamReader file = new System.IO.StreamReader(currentDirectory + "/Levels.txt");
            string line;
            Level myLevel;
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
                }
            }
        }
        public override void LoadContent(ContentManager ContentMgr,GraphicsDeviceManager graphics)
        {
        
            //spriteBatch = new SpriteBatch(GraphicsDevice);
          
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            leftTransitionRect = new Entity(5, 0, 1, screenHeight);
            rightTransitionRect = new Entity(screenWidth - 5, 0, 1, screenHeight);
            

            player.LoadContent(ContentMgr);
            Random rand = new Random();

           // health = new List<Health>();
         

            enemies = new List<EnemyAI>();
            for(int i = 0; i < 2; i++)
            {
                bool goodStart = false;
                int startX =0;
                int startY =0;
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

                    if((enemyRight < playerLeft ||
                        enemyLeft > playerRight ||
                        enemyTop > playerBottom ||
                        enemyBottom < playerTop))
                    {
                        goodStart = true;
                    }
                }

                int enemySeed = rand.Next(0, 5000);
                enemies.Add(new EnemyAI(player, startX, startY, enemySeed));
                enemies[i].LoadContent(ContentMgr);
               
            }
            background = ContentMgr.Load<Texture2D>(levelArray[currentRow, currentCol].background);

            for (int i = 0; i < levelArray[currentRow, currentCol].buildingName.Count; i++)
            {
                buildingTextures.Add(ContentMgr.Load<Texture2D>(levelArray[currentRow, currentCol].buildingName[currentRow * NUMCOL + currentCol]));
                buildings.Add(new Buildings(buildingTextures[0], new Vector2(levelArray[currentRow, currentCol].buildingX[currentRow * NUMCOL + currentCol], levelArray[currentRow, currentCol].buildingY[currentRow * NUMCOL + currentCol])));
            }
            //this.IsMouseVisible = true;
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
            //LoadContent();

        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ChangeScreen("PauseScreen");

            }
            if (Keyboard.GetState().IsKeyDown(Keys.CapsLock))
            {
                ChangeScreen("InventoryScreen");
            }
            MouseState mouse = Mouse.GetState();
            player.Update(gameTime);
            if (player.Collision(leftTransitionRect))
            {
                currentCol = currentCol - 1;
                if (currentCol < 0)
                {
                    currentCol = NUMCOL;
                }
                ScreenTransfer(currentRow, currentCol);
                
            }
            if (player.Collision(rightTransitionRect))
            {
                ChangeScreen("PauseScreen");
            }

            for (int i = 0; i < buildings.Count; i++)
            {
                if (player.Collision(buildings[i]))
                {
                    player.position = player.oldPosition;

                }
            }

            foreach (EnemyAI enemy in enemies)
            {
                enemy.Update(gameTime,player);
                if (enemy.Collision(player))
                {
                    ChangeScreen("BattleScreen");
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
           
            //GraphicsDevice.Clear(Color.CornflowerBlue);
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
