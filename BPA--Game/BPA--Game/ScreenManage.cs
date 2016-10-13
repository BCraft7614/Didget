using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPA__Game
{
    public class ScreenManage
    {
        enum Screen
        {
            TitleScreen,
            PlayScreen,
        }
        Screen CurrentScreen = Screen.TitleScreen;
        mButton btnPlay;

    }
}
