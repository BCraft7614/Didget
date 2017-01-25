using BPA__Game.Content;
using System;
using System.Net.Mail;

namespace BPA__Game
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
        
           //try
            {
                using (var game = new ScreenManager())
                    game.Run();
            }
            /*
            catch (Exception e)
            {
                using (ErrorHandler errorHandler = new ErrorHandler())
                {
                    errorHandler.SetErrorMsg(e.Message, e.StackTrace);
                    errorHandler.Run();
                   
                }
            }
            */

        }
    }
}
#endif

