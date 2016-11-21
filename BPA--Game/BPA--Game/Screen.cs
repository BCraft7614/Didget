using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BPA__Game
{
    public class Screen
    {
        private string nextScreen;
        public string GetNextScreen()
        {
            return nextScreen;
        }
        public virtual void LoadContent()
        {

        }
        public virtual void UnloadContent()
        {

        }
        //Update any information specific to the screen
        public virtual void Update(GameTime theTime)
        {

        }

        //Draw any objects specific to the screen
        public virtual void Draw(SpriteBatch theBatch)
        {

        }
    }
}
