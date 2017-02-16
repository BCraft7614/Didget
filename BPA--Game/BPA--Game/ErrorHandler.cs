using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Net.Mail;

namespace BPA__Game.Content
{
    
    public class ErrorHandler : Microsoft.Xna.Framework.Game
    {
        public string ErrorText = "No Error Found";
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont ErrorFont;
        private string errorMsg;
        mButton sendEmail;
        mButton dontSend;

        public ErrorHandler()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ErrorFont = Content.Load<SpriteFont>("ErrorFont");
            sendEmail = new mButton(Content.Load<Texture2D>("SendEmail"), graphics.GraphicsDevice);
            dontSend = new mButton(Content.Load<Texture2D>("Nobtn"), graphics.GraphicsDevice);
            sendEmail.ButtonClicked += HandleButtonClicked;
            dontSend.ButtonClicked += HandleButtonClicked;
            sendEmail.setPosition(new Vector2(350, 300 + sendEmail.size.Y * 2));
            dontSend.setPosition(new Vector2(350, 300 + dontSend.size.Y * 4));

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            sendEmail.ButtonClicked -= HandleButtonClicked;
            dontSend.ButtonClicked -= HandleButtonClicked;
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            sendEmail.Update();
            dontSend.Update();
        }
      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.DrawString(ErrorFont, "An Error occured in your game, the error is as follows", Vector2.Zero, Color.White);
            spriteBatch.DrawString(ErrorFont, errorMsg, new Vector2(0, 40), Color.White);
            sendEmail.Draw(spriteBatch);
            dontSend.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void HandleButtonClicked(object sender, EventArgs eventArgs)
        {
            //If user chooses send, this sends the email
            if (sender == sendEmail)
            {
                SendEmail();
                System.Environment.Exit(1);
            }
            //This is if the user chooses to not send the email. Doesn't send email
            else if (sender == dontSend)
            {
                System.Environment.Exit(1);
            }
        }
        private void SendEmail()
        {
            try
            {
                
                MailMessage mail = new MailMessage("didgetsreal@gmail.com","DidgetsRealBPA@gmail.com");
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com",587);

                mail.From = new MailAddress("didgetsreal@gmail.com");
                mail.To.Add("DidgetsRealBPA@gmail.com");
                mail.Subject = "Errors";
                mail.Body = errorMsg;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                SmtpServer.Port =587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("didgetsReal@gmail.com", "Didgets1234");
                SmtpServer.EnableSsl = true;
                
                SmtpServer.Send(mail);
            }
            catch (Exception)
            {
                
            }
        }
        public void SetErrorMsg(string message, string stackTrace)
        {
            //This gets the error code and types it out in the email
            errorMsg = message + "\n" + stackTrace;
        }

    }
}

