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

namespace BPA__Game
{
    class TutorialBattleScreen : Screen
    {
        int screenWidth;
        int screenHeight;
        Texture2D background;
        mButton fightButton;
        mButton FleeButton;
        mButton specialButton;
        mButton itemButton;
        mButton selected;
        EnemyAI enemy;
        int enemyHealth;
        int playerHealth;
        int playerStength;
        int playerDefence;
        private SpriteFont tutorialFont;

        SpriteFont enemyHealthFont;
        SpriteFont HealthFont;
        public TutorialBattleScreen()
        {
            screenWidth = 800;
            screenHeight = 700;
            //ReadSave();
        }


        public void ReadSave()
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader("Levels.txt"))
            {
                file.ReadLine(); //player positionX
                file.ReadLine(); //player positionY
                playerHealth = Convert.ToInt32(file.ReadLine());
                playerStength = Convert.ToInt32(file.ReadLine());
                playerStength = Convert.ToInt32(file.ReadLine());
            }


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
            fightButton.setPosition(new Vector2(650, 500));
            itemButton.setPosition(new Vector2());
            background = ContentMgr.Load<Texture2D>("BattleScreen");
            tutorialFont = ContentMgr.Load<SpriteFont>("TutorialHelp");
            base.LoadContent(ContentMgr, graphics);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 700), Color.White);
            spriteBatch.DrawString(tutorialFont, "To Battle use the fight button.", new Vector2(100, 100), Color.DarkGreen);
            spriteBatch.DrawString(HealthFont, "Health:" + playerHealth.ToString(), new Vector2(689, 300), Color.Green);
            spriteBatch.DrawString(enemyHealthFont, "Health:" + enemyHealth.ToString(), new Vector2(650, 50), Color.Red);
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
        private void FightUpdate()
        {
            enemyHealth -= 20;

            if (enemyHealth <= 0)
            {
                ChangeScreen("GameScreen");

            }

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
                //ScreenName.GameScreen;
                FightUpdate();

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
        public void ChangeScreen(string NextScreen)
        {
            nextScreen = NextScreen;
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
