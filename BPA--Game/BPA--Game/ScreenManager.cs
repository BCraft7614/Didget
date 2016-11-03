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
    public class ScreenManager : Screen
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TitleScreen titleScreen;
        OptionsScreen optionScreen;
        PauseScreen pauseScreen;
        GameScreen gameScreen;
        LoadScreen loadScreen;

        ScreenName CurrentScreen;
        mButton btnPlay;

        int screenWidth = 800; int screenHeight = 700;

        public ScreenManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            CurrentScreen = ScreenName.TitleScreen;
            titleScreen = new TitleScreen();
            optionScreen = new OptionsScreen();
            pauseScreen = new PauseScreen();
            gameScreen = new GameScreen();
            loadScreen = new LoadScreen();

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
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            titleScreen.ButtonClicked += HandleButtonClicked;
            gameScreen.ButtonClicked += HandleButtonClicked;
            optionScreen.ButtonClicked += HandleButtonClicked;
            loadScreen.ButtonClicked += HandleButtonClicked;
            pauseScreen.ButtonClicked += HandleButtonClicked;


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
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            MouseState mouse = Mouse.GetState();
            switch (CurrentScreen)
            {
                case ScreenName.TitleScreen:
                    btnPlay.Update(mouse);
                    titleScreen.Update(gameTime);
                    break;
                case ScreenName.GameScreen:
                    gameScreen.Update(gameTime);
                    break;
                case ScreenName.OptionsScreen:
                    optionScreen.Update(gameTime);
                    break;

                case ScreenName.PauseScreen:
                    pauseScreen.Update(gameTime);
                    break;

                case ScreenName.LoadScreen:
                    loadScreen.Update(gameTime);
                    break;

            }


        }
        // TODO: Add your update logic here


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            switch (CurrentScreen)
            {
                case ScreenName.TitleScreen:
                    titleScreen.Draw(spriteBatch);
                    break;
                case ScreenName.GameScreen:
                    gameScreen.Draw(spriteBatch);
                    break;
                case ScreenName.OptionsScreen:
                    optionScreen.Draw(spriteBatch);
                    break;

                case ScreenName.PauseScreen:
                    pauseScreen.Draw(spriteBatch);
                    break;

                case ScreenName.LoadScreen:
                    loadScreen.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
        }


        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            if (sender == titleScreen)
            {
                CurrentScreen = titleScreen.GetNextScreen();

            }
            else if (sender == gameScreen)
            {
                CurrentScreen = gameScreen.GetNextScreen();
            }
            else if (sender == optionScreen)
            {
                CurrentScreen = optionScreen.GetNextScreen();
            }
            else if (sender == loadScreen)
            {
                CurrentScreen = loadScreen.GetNextScreen();
            }
            else if (sender == pauseScreen)
            {
                CurrentScreen = pauseScreen.GetNextScreen();
            }

        }

    }
}
