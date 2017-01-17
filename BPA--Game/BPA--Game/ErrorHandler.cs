using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BPA__Game.Content
{
    
    public class ErrorHandler : Microsoft.Xna.Framework.Game
    {
        public string ErrorText = "No Error Found";
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont ErrorFont;

        public ErrorHandler()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

     
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ErrorFont = Content.Load<SpriteFont>("ErrorFont");
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            base.Update(gameTime);
        }
      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.DrawString(ErrorFont, "An Error occured in your game, the error is as follows", Vector2.Zero, Color.White);
            spriteBatch.DrawString(ErrorFont, ErrorText, new Vector2(0, 40), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

