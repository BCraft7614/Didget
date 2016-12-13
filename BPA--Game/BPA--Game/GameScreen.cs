﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
        mButton btnPlay;
        mButton btnLoad;
        mButton btnOp;
        mButton btnExit;
        Texture2D background;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<EnemyAI> enemies;
        Player player;
        //ScreenName nextScreen;
        int screenWidth;
        int screenHeight;

  
        public GameScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
            //LoadContent();
            //Initialize();

            player = new Player();
        }

        public override void LoadContent(ContentManager ContentMgr,GraphicsDeviceManager graphics)
        {
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            background = ContentMgr.Load<Texture2D>("TownGameScreen");
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            player.LoadContent(ContentMgr);
            Random rand = new Random();

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
           
            //this.IsMouseVisible = true;
        }
        public override void UnloadContent()
        {
            player.UnloadContent();
            foreach(EnemyAI enemy in enemies)
            {
                enemy.UnloadContent();
            }
          
            
        }
        public override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                nextScreen = "PauseScreen";
            }
            MouseState mouse = Mouse.GetState();
            player.Update(gameTime);

            foreach (EnemyAI enemy in enemies)
            {
                enemy.Update(gameTime,player);
                if (enemy.Collision(player))
                {
                    ChangeScreen("TitleScreen");
                }

            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 700), Color.White);
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
