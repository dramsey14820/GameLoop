using System;
using System.Windows.Forms;

namespace GameLoop
{
    internal static class Program
    {
        #region Private Members

        /// <summary>
        /// The private instance of the fast loop object.
        /// </summary>
        private static FastLoop _fastLoop = new FastLoop(GameLoop);

        #endregion Private Members

        #region Main Method

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        #endregion Main Method

        #region Static Methods

        /// <summary>
        /// The game the loop.
        /// </summary>
        /// <param name="elapsedTime">The elapsed time.</param>
        private static void GameLoop(double elapsedTime)
        {
            // TODO:  GameCode goes here.
            // TODO:  Get Input
            // TODO:  Process
            // TODO:  Render

            System.Console.WriteLine(string.Format("loop at {0}", DateTime.Now.ToLongTimeString()));
        }

        #endregion Static Methods
    }
}