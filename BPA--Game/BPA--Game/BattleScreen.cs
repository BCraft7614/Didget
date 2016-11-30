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
    class BattleScreen : Screen
    {
        mButton fightButton;
        mButton runButton;
        mButton specialButton;
        mButton itemButton;
        mButton selected;

        public BattleScreen()
            : base()
        {

        }

        public override void Update(GameTime theTime)
        {
            //Fight Button
            if (selected == fightButton)
                FightUpdate(theTime);
            //Item Button
            else if (selected == itemButton)
                ItemUpdate(theTime);
            //Special Button
            else if (selected == specialButton)
                SpecialUpdate(theTime);
            //Run Button
            else if (selected == runButton)
                RunUpdate(theTime);
            //Shows Screen
            else
            {
                MouseState mouseState = Mouse.GetState();

                if (mouseState.LeftButton == ButtonState.Pressed && fightButton.rectangle.Contains(mouseState.Position))
                    selected = fightButton;




            }


            base.Update(theTime);
        }
        //Should Call Invertory class if Item button is pressed
        private void ItemUpdate(GameTime theTime)
        {

        }


        //Should call FightActionClass
        private void FightUpdate(GameTime theTime)
        {

        }

        //Should loada SpecialAblitlyClass
        private void SpecialUpdate(GameTime theTime)
        {

        }


        //Should load RunClass but it doesnt work
        private void RunUpdate(GameTime theTime)
        {

        }


        //Event Handler

        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            /*
            sender = btnPlay;
            if (sender == btnPlay)
            {

                nextScreen = "TitleScreen"; //ScreenName.GameScreen;
            }
            else if (sender == btnOp)
            {
                nextScreen = "OptionsScreen"; //ScreenName.OptionsScreen;
            }
            else if (sender == btnLoad)
            {
                nextScreen = "LoadScreen"; //ScreenName.LoadScreen;
            }
           // else if (sender == btnExit)
          //  {
           //     System.Environment.Exit(1);
          //  }
          */
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
