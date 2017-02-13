using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using BPA__Game.Content;

namespace BPA__Game
{
    public class ScreenManager : Game
    {

        public static GraphicsDeviceManager GraphicsDevceMgr;
        public static SpriteBatch Sprites;
        public static SpriteBatch spriteBatch;
        public static Dictionary<string, Texture2D> Textures2D;
        public static Dictionary<string, Screen> Screens;
        TitleScreen titleScreen;
        BattleScreen battleScreen;
        OptionsScreen optionScreen;
        PauseScreen pauseScreen;
        GameScreen gameScreen;
        LoadScreen loadScreen;
        InventoryScreen inventoryScreen;
        Screen CurrentScreen;
        TutorialScreen tutorialScreen;
        TutorialBattleScreen tutorialBattleScreen;
        SplashScreen splashScreen;
        SplashScreen splashScreen2;
        ShopScreen shopScreen;

        public ScreenManager()
        {

            GraphicsDevceMgr = new GraphicsDeviceManager(this);
            GraphicsDevceMgr.PreferredBackBufferWidth = 800;
            GraphicsDevceMgr.PreferredBackBufferHeight = 700;
            Content.RootDirectory = "Content";
            
            Screens = new Dictionary<string, Screen>();
            titleScreen = new TitleScreen();
            gameScreen = new GameScreen();
            optionScreen = new OptionsScreen();
            pauseScreen = new PauseScreen();
            loadScreen = new LoadScreen();
            inventoryScreen = new InventoryScreen();
            battleScreen = new BattleScreen();
            tutorialScreen = new TutorialScreen();
            tutorialBattleScreen = new TutorialBattleScreen();
            splashScreen = new SplashScreen("SplashScreen","SplashScreen2");
            splashScreen2 = new SplashScreen("SplashText","TitleScreen");
            shopScreen = new ShopScreen();
            // LoadContent();
            //Initialize();
            CurrentScreen = splashScreen;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Textures2D = new Dictionary<string, Texture2D>();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            titleScreen.ButtonClicked += HandleButtonClicked;
            gameScreen.ButtonClicked += HandleButtonClicked;
            optionScreen.ButtonClicked += HandleButtonClicked;
            loadScreen.ButtonClicked += HandleButtonClicked;
            pauseScreen.ButtonClicked += HandleButtonClicked;
            inventoryScreen.ButtonClicked += HandleButtonClicked;
            battleScreen.ButtonClicked += HandleButtonClicked;
            tutorialScreen.ButtonClicked += HandleButtonClicked;
            tutorialBattleScreen.ButtonClicked += HandleButtonClicked;
            splashScreen.ButtonClicked += HandleButtonClicked;
            splashScreen2.ButtonClicked += HandleButtonClicked;
            shopScreen.ButtonClicked += HandleButtonClicked;

            Content = base.Content;
            Screens.Add("TitleScreen", titleScreen);
            Screens.Add("GameScreen", gameScreen);
            Screens.Add("PauseScreen", pauseScreen);
            Screens.Add("OptionsScreen", optionScreen);
            Screens.Add("LoadScreen", loadScreen);
            Screens.Add("InventoryScreen", inventoryScreen);
            Screens.Add("BattleScreen", battleScreen);
            Screens.Add("TutorialScreen", tutorialScreen);
            Screens.Add("TutorialBattleScreen", tutorialBattleScreen);
            Screens.Add("SplashScreen", splashScreen);
            Screens.Add("SplashScreen2", splashScreen2);
            Screens.Add("ShopScreen", shopScreen);

            CurrentScreen.LoadContent(Content, GraphicsDevceMgr);
          /*  gameScreen.LoadContent(Content, GraphicsDevceMgr);
            pauseScreen.LoadContent(Content, GraphicsDevceMgr);
            optionScreen.LoadContent(Content, GraphicsDevceMgr);
            loadScreen.LoadContent(Content, GraphicsDevceMgr);*/

            this.IsMouseVisible = true;
            base.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            titleScreen.ButtonClicked -= HandleButtonClicked;
            gameScreen.ButtonClicked -= HandleButtonClicked;
            optionScreen.ButtonClicked -= HandleButtonClicked;
            loadScreen.ButtonClicked -= HandleButtonClicked;
            pauseScreen.ButtonClicked -= HandleButtonClicked;
            inventoryScreen.ButtonClicked -= HandleButtonClicked;
            battleScreen.ButtonClicked -= HandleButtonClicked;
            tutorialScreen.ButtonClicked -= HandleButtonClicked;
            tutorialBattleScreen.ButtonClicked -= HandleButtonClicked;
            splashScreen.ButtonClicked -= HandleButtonClicked;
            splashScreen2.ButtonClicked -= HandleButtonClicked;
            shopScreen.ButtonClicked -= HandleButtonClicked;
            base.UnloadContent();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //MouseState mouse = Mouse.GetState();
            CurrentScreen.Update(gameTime);

        }
        // TODO: Add your update logic here


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            CurrentScreen.Draw(spriteBatch);
            spriteBatch.End();
        }


        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            if (sender == loadScreen)
            {
                gameScreen.newGame = !loadScreen.GetLoadGame();
            }
            else if (sender == titleScreen)
            {
                gameScreen.newGame = true;
            }
            Screen previousScreen = CurrentScreen;
            Screens.TryGetValue(CurrentScreen.GetNextScreen(), out CurrentScreen);
           // previousScreen.UnloadContent();
            CurrentScreen.LoadContent(Content,GraphicsDevceMgr);

        }
    }
}

        

