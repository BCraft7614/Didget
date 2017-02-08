using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;
using BPA__Game.Content;

namespace BPA__Game
{


    public class TutorialScreen : GameScreen
    {

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
                     currentCol = NUMCOL - 1;
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

             for (int i = 0; i < enemies.Count; i++)
             {

                 enemies[i].Update(gameTime, player);
                 if (enemies[i].Collision(player))
                 {
                     enemyCollisionIndex = i;
                     // enemies.Remove(enemies[i]);
                     ChangeScreen("TutorialBattleScreen");

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
            for (int i = 0; i < buildings.Count; i++)
            {
                buildings[i].Draw(spriteBatch);
            }
            player.Draw(spriteBatch);

            foreach (EnemyAI enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            spriteBatch.DrawString(tutorialHelp, "To move use the Arrow keys or WASD keys", new Vector2(100, 100), Color.DarkGoldenrod);
        }
    }

}
