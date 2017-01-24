﻿using System;
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
        const int NUMROW = 1;

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
        Player player;
        Buildings mainTower;
        Buildings waterPool;

        //ScreenName nextScreen;
        int screenWidth;
        int screenHeight;
            
        Entity upTransitionRect;
        Entity leftTransitionRect;
        Entity rightTransitionRect;
        Entity downTransitionRect;

        List<Buildings> buildings = new List<Buildings>();
        List<Texture2D> buildingTextures = new List<Texture2D>();
        ContentManager content;
        bool inLevelDescription;
        bool playerDescription;
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
           // WriteSave();
            currentRow = 0;
            currentCol = 0;

        }
        
        public void WriteSave()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("SaveData.txt"))
            {
                //writing the players attributes
                file.WriteLine(player.GetHealth());//Writes the players Health to a file
                file.WriteLine(player.position.X);
                file.WriteLine(player.position.Y);
                file.WriteLine(player.GetStrength());//Writes the players strength to a file
                file.WriteLine(player.GetDefense());// Writes the players defense to a file
                file.WriteLine(player.GetCoins());

                //writing enemy attributes
                foreach (EnemyAI enemy in enemies)
                {
                    file.WriteLine(enemy.position.X);
                    file.WriteLine(enemy.position.Y);
                    file.WriteLine("Blue Left Movement");//FIXME
                }
                //enemies.Clear();
            }
        }
        
        public void ReadSaveFile()
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader("SaveData.txt"))
            {
                player.playerHealth = Convert.ToInt32(file.ReadLine());
                player.position.X = Convert.ToInt32(file.ReadLine());
                player.position.Y = Convert.ToInt32(file.ReadLine());
                player.str = Convert.ToInt32(file.ReadLine());
                player.def = Convert.ToInt32(file.ReadLine());
                player.coins = Convert.ToInt32(file.ReadLine());
                string line;
                while (( line = file.ReadLine()) != null)
                {
                    int xPos = Convert.ToInt32(line);
                    int yPos = Convert.ToInt32(file.ReadLine());
                    string texture  = file.ReadLine();//FIXME
                    enemies.Add(new EnemyAI(xPos, yPos));
                }
            }
        }
         
        public void ReadFile()
        {
            // string currentDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            using (System.IO.StreamReader file = new System.IO.StreamReader("Levels.txt"))
            {
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
            player.LoadContent(content);

            LoadLevel(content);
           // health = new List<Health>();
 
            //this.IsMouseVisible = true;
        }
        public void LoadLevel(ContentManager content)
        {
            Random rand = new Random();
            enemies = new List<EnemyAI>();
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

                    if ((enemyRight < playerLeft ||
                        enemyLeft > playerRight ||
                        enemyTop > playerBottom ||
                        enemyBottom < playerTop))
                    {
                        goodStart = true;
                    }
                }

                int enemySeed = rand.Next(0, 5000);
                enemies.Add(new EnemyAI(startX, startY));
                enemies[i].LoadContent(content);

            }
            background = content.Load<Texture2D>(levelArray[currentRow, currentCol].background);

            for (int i = 0; i < levelArray[currentRow, currentCol].buildingName.Count; i++)
            {
                buildingTextures.Add(content.Load<Texture2D>(levelArray[currentRow, currentCol].buildingName[currentRow * NUMCOL + currentCol]));
                buildings.Add(new Buildings(buildingTextures[0], new Vector2(levelArray[currentRow, currentCol].buildingX[currentRow * NUMCOL + currentCol], levelArray[currentRow, currentCol].buildingY[currentRow * NUMCOL + currentCol])));
            }
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
                ChangeScreen("PauseScreen");

            }
            if (Keyboard.GetState().IsKeyDown(Keys.CapsLock))
            {
                ChangeScreen("InventoryScreen");
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

                }
            }

            foreach (EnemyAI enemy in enemies)
            {
                enemy.Update(gameTime,player);
                if (enemy.Collision(player))
                {
                    ChangeScreen("BattleScreen");
                }
                foreach(Buildings building in buildings)
                {
                    if (enemy.Collision(building))
                    {
                        enemy.position = enemy.oldPosition;
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
           
            //GraphicsDevice.Clear(Color.CornflowerBlue);
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
