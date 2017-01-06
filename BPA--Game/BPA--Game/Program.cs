using BPA__Game.Content;
using System;

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
           // {
                using (var game = new ScreenManager())
                    game.Run();
           /* }
            catch (Exception e)
            {
                using (ErrorHandler errorHandler = new ErrorHandler())
                {
                    errorHandler.ErrorText = e.Message;
                    errorHandler.Run();
                }
            }*/

        }
    }
}
#endif

