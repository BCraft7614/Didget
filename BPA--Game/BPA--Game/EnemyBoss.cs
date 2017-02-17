using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPA__Game
{
    class EnemyBoss : EnemyAI
    {
        public EnemyBoss(int posX, int posY)
        {
            rand = new Random(posX);
            position.X = posX;
            position.Y = posY;
            enemyHealth = 150;
            enemyStr = 20;
            enemyDef = 15;
        }
    }
}
