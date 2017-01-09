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
        Texture2D background;
        mButton fightButton;
        mButton FleeButton;
        mButton specialButton;
        mButton itemButton;
        mButton selected;
        int enemyHealth = 100;
        int playerHealth = 100;
        SpriteFont enemyHealthFont;
        SpriteFont HealthFont;
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
            enemyHealthFont = ContentMgr.Load<SpriteFont>("HealthFont");
            HealthFont = ContentMgr.Load<SpriteFont>("HealthFont");
            fightButton.ButtonClicked += HandleButtonClicked;
            itemButton.ButtonClicked += HandleButtonClicked;
            specialButton.ButtonClicked += HandleButtonClicked;

            background = ContentMgr.Load<Texture2D>("BattleScreen");
            base.LoadContent(ContentMgr, graphics);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 700), Color.White);
            spriteBatch.DrawString(HealthFont, "Health:" + playerHealth.ToString(), new Vector2(240, 530), Color.Green);
            spriteBatch.DrawString(enemyHealthFont, "Health:" + enemyHealth.ToString(), new Vector2(0, 500), Color.Red);
            fightButton.Draw(spriteBatch);
            itemButton.Draw(spriteBatch);
            specialButton.Draw(spriteBatch);
        }

        public override void Update(GameTime theTime)
        {
            fightButton.Update();
            itemButton.Update();
            specialButton.Update();
            FleeButton.Update();


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
        public override void UnloadContent()
        {
            fightButton.ButtonClicked -= HandleButtonClicked;
            itemButton.ButtonClicked -= HandleButtonClicked;
            specialButton.ButtonClicked -= HandleButtonClicked;
            base.UnloadContent();
        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            sender = fightButton;
            if (sender == fightButton)
            {
                enemyHealth--; //ScreenName.GameScreen;
            }
            else if (sender == itemButton)
            {
                nextScreen = "LoadScreen"; //ScreenName.LoadScreen;
                OnButtonClicked();
            }
            else if (sender == specialButton)
            {
                nextScreen = "TitleScreen"; //ScreenName.TitleScreen
                OnButtonClicked();
            }

            
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
