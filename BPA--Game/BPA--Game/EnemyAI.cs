using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace BPA__Game
{
    class EnemyAI : Entity
    {
        Texture2D rightWalk, leftWalk, upWalk, downWalk, currentWalk;
        Rectangle destRect;
        Rectangle sourceRect;
        Player player;
        float elapsed;
        float delay = 200f;
        int frames = 0;
        int randTime = 0;
        int randDirection = 0;

        public void Enemy(Player player, int posX, int posY, int enemySeed)
        {

            position.X = posX;
            position.Y = posY;
        }
    }
}
