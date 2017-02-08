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

namespace BPA__Game
{
    public class TutorialBattleScreen : BattleScreen
    {
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 700), Color.White);
            spriteBatch.DrawString(HealthFont, "Health:" + playerHealth.ToString(), new Vector2(689, 330), Color.Green);
            spriteBatch.DrawString(enemyHealthFont, "Health:" + enemyHealth.ToString(), new Vector2(650, 50), Color.Red);
            spriteBatch.Draw(battleEnemy2, new Vector2(660, 70), Color.White);
            spriteBatch.Draw(battleEnemy, new Vector2(660, 70), Color.White);
            DrawAnimation(spriteBatch);
            fightButton.Draw(spriteBatch);
            itemButton.Draw(spriteBatch);
            specialButton.Draw(spriteBatch);
            spriteBatch.DrawString(tutorialHelp, "To Battle, use the fight button\n Items hold potions if you have any\n Special is for you to find out ;D", new Vector2(100, 100), Color.DarkGoldenrod);


        }

    }
}
