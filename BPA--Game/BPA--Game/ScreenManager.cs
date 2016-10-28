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
    public class ScreenManager : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TitleScreen titleScreen;
        OptionsScreen OptionScreen;
        PauseScreen pauseScreen;
        GameScreen gameScreen;
        enum Screen
        {
            TitleScreen,
            GameScreen,
            OptionsScreen,
            PauseScreen,
            LoadScreen,
        }
        Screen CurrentScreen;
        mButton btnPlay;

        int screenWidth = 800; int screenHeight = 700;

        public ScreenManager()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            CurrentScreen = Screen.TitleScreen;
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

            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            MouseState mouse = Mouse.GetState();
            switch (CurrentScreen)
            {
                case Screen.TitleScreen:
                    break;
                case Screen.GameScreen:

                    break;
                case Screen.OptionsScreen:

                    break;

                case Screen.PauseScreen:

                    break;

                case Screen.LoadScreen:

                    break;

            }

            base.Update(gameTime);
        }
        // TODO: Add your update logic here


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            switch (CurrentScreen)
            {
                case Screen.TitleScreen:
                    break;
                case Screen.GameScreen:
                    
                    break;
                case Screen.OptionsScreen:

                    break;

                case Screen.PauseScreen:

                    break;

                case Screen.LoadScreen:

                    break;
            }
            spriteBatch.End();
        }


        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            if (sender == btnPlay)
            {
                CurrentScreen = Screen.TitleScreen;
            }
            else if (sender == btnPlay)
            {
                CurrentScreen = Screen.GameScreen;
            }
        }
        public EventHandler ButtonClicked;
        public void OnButtonClicked()
        {
            if(ButtonClicked != null)
            {
                ButtonClicked(this, EventArgs.Empty);
            }
        }

    }
}
