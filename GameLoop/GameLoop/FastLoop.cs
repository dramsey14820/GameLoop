using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GameLoop
{
    /// <summary>
    /// This is the layout of the Struct message to be sent to the Peek Message method in c.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Message
    {
        public IntPtr hWnd;
        public Int32 msg;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public System.Drawing.Point p;
    }

    /// <summary>
    /// This provides the game loop.
    /// </summary>
    public class FastLoop
    {
        #region Public Delegates

        /// <summary>
        /// The loop call back delegate.
        /// </summary>
        /// <param name="elapsedTime">The elapsed time.</param>
        public delegate void LoopCallBack(double elapsedTime);

        #endregion Public Delegates

        #region Public Extern Members

        // We are importing the peek message function from C here.
        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool PeekMessage(
            out Message msg,
            IntPtr hWnd,
            uint messageFilterMin,
            uint messageFilterMax,
            uint flags
        );

        #endregion Public Extern Members

        #region Private members

        /// <summary>
        /// The instance of the time.
        /// </summary>
        private PreciseTimer _timer = new PreciseTimer();

        /// <summary>
        /// The instance of the callback.
        /// </summary>
        private LoopCallBack _callback;

        #endregion Private members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FastLoop" /> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public FastLoop(LoopCallBack callback)
        {
            _callback = callback;
            Application.Idle += new EventHandler(OnApplicationEnterIdle);
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Called when the application becomes idle.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnApplicationEnterIdle(object sender, EventArgs e)
        {
            while (IsAppStillIdle())
            {
                _callback(_timer.GetElapsedTime());
            }
        }

        /// <summary>
        /// Determines if the application is still idle.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is app still idle]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsAppStillIdle()
        {
            Message message;

            // Note:  We are calling the external c method here.
            return !PeekMessage(out message, IntPtr.Zero, 0, 0, 0);
        }

        #endregion Private Methods
    }
}