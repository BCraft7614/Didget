using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace BPA__Game
{
    public class Screen
    {
        protected SoundEffect btnSound;
        protected Song song;
        protected String nextScreen;
        protected bool ExitGame =false;
        public  string GetNextScreen()
        {
           
            return nextScreen;
        }
        public virtual void LoadContent(ContentManager ContentMgr, GraphicsDeviceManager graphics)
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
        public bool GetExitGame()
        {
            return ExitGame;
        }
    }
}
