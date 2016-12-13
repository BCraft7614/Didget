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
        mButton FleeButton;
        mButton specialButton;
        mButton itemButton;
        mButton selected;

        public BattleScreen()
            : base()
        {
        }

        public override void LoadContent(ContentManager ContentMgr, GraphicsDeviceManager graphics)
        {
            fightButton = new mButton(ContentMgr.Load<Texture2D>("btnLoad"), graphics.GraphicsDevice, (o, e) => selected = fightButton);

            itemButton = new mButton(ContentMgr.Load<Texture2D>("btnLoad"), graphics.GraphicsDevice, (o, e) => selected = itemButton);

            specialButton = new mButton(ContentMgr.Load<Texture2D>("btnLoad"), graphics.GraphicsDevice, (o, e) => selected = specialButton);

            FleeButton = new mButton(ContentMgr.Load<Texture2D>("btnLoad"), graphics.GraphicsDevice, (o, e) => selected = FleeButton);


            base.LoadContent(ContentMgr, graphics);
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
            else if (selected == FleeButton)
                FleeUpdate(theTime);
            //Shows Screen
            else
            {
                fightButton.Update();




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
        private void FleeUpdate(GameTime theTime)
        {

        }
    }
}
