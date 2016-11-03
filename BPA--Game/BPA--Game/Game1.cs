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
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Texture2D rightanim;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        enum Screen
        {
            TitleScreen,
            GameScreen,
            OptionsScreen,
            PauseScreen,
        }
        Screen CurrentScreen = Screen.TitleScreen;
        mButton btnPlay;

        int screenWidth = 800; int screenHeight = 700;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            btnPlay = new mButton(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            rightanim = Content.Load<Texture2D>("RightAnime");
            btnPlay.setPosition(new Vector2(350, 300));
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState mouse = Mouse.GetState();
            switch (CurrentScreen)
            {
                case Screen.TitleScreen:   
                    btnPlay.Update(mouse);
                    break;
                case Screen.GameScreen:

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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (CurrentScreen)
            {
                case Screen.TitleScreen:
                    btnPlay.Draw(spriteBatch);
                    break;
                case Screen.GameScreen:
                    spriteBatch.Draw(rightanim, new Vector2(100,100), Color.White);
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
    }
}
