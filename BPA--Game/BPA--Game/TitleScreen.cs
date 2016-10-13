using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BPA__Game
{
    public class TitleScreen : Game
    {
        mButton btnPlay;
        mButton btnOp;
        Texture2D background;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int screenWidth;
        int screenHeight;

        public TitleScreen()
        {
            screenWidth = 700;
            screenHeight = 800;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            MouseState mouse = Mouse.GetState();
            if (btnPlay.isClicked == true)
            {
                //CurrentScreen = Screen.PlayScreen;

            }
            else if (btnOp.isClicked == true)
            {
                //CurrentScreen = screenHeight.OptionScreen;
            }
            btnPlay.Update(mouse);
            btnOp.Update(mouse);

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            btnPlay = new mButton(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnOp = new mButton(Content.Load<Texture2D>("OpButton"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(350, 300));
            btnOp.setPosition(new Vector2(350, 300 + btnOp.size.Y * 2));
            this.IsMouseVisible = true;
        }
        protected override void Draw(GameTime gameTime)
        {
            btnPlay.Draw(spriteBatch);
        }

    }

}

