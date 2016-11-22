using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

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
        OptionsScreen optionScreen;
        PauseScreen pauseScreen;
        GameScreen gameScreen;
        LoadScreen loadScreen;


        Screen CurrentScreen;

        public ScreenManager()
        {

            GraphicsDevceMgr = new GraphicsDeviceManager(this);
            GraphicsDevceMgr.PreferredBackBufferWidth = 800;
            GraphicsDevceMgr.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            CurrentScreen = titleScreen;
            Screens = new Dictionary<string, Screen>();
            titleScreen = new TitleScreen();
            optionScreen = new OptionsScreen();
            pauseScreen = new PauseScreen();
            gameScreen = new GameScreen();
            loadScreen = new LoadScreen();
           // LoadContent();
            //Initialize();
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
            //optionScreen.ButtonClicked += HandleButtonClicked;
            //loadScreen.ButtonClicked += HandleButtonClicked;
            //pauseScreen.ButtonClicked += HandleButtonClicked;

            Content = base.Content;
            Screens.Add("TitleScreen", titleScreen);
            Screens.Add("GameScreen", gameScreen);
            //titleScreen.LoadContent();
            //gameScreen.LoadContent();

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
            //optionScreen.ButtonClicked -= HandleButtonClicked;
            //loadScreen.ButtonClicked -= HandleButtonClicked;
           // pauseScreen.ButtonClicked -= HandleButtonClicked;
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
            Screens.TryGetValue(CurrentScreen.GetNextScreen(), out CurrentScreen);
        }
    }
}

        

