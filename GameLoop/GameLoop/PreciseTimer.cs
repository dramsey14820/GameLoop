using System.Runtime.InteropServices;

namespace GameLoop
{
    public class PreciseTimer
    {
        #region Private External Members

        /// <summary>
        /// Queries the performance frequency.
        /// </summary>
        /// <param name="PerformanceFrequency">The performance frequency.</param>
        /// <returns></returns>
        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        private static extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);

        /// <summary>
        /// Queries the performance counter.
        /// </summary>
        /// <param name="PerformanceCount">The performance count.</param>
        /// <returns></returns>
        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        private static extern bool QueryPerformanceCounter(ref long PerformanceCount);

        #endregion Private External Members

        #region Private Members

        /// <summary>
        /// The ticks per second
        /// </summary>
        private long _ticksPerSecond = 0;

        /// <summary>
        /// The previous elapsed time
        /// </summary>
        private long _previousElapsedTime = 0;

        #endregion Private Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PreciseTimer" /> class.
        /// </summary>
        public PreciseTimer()
        {
            QueryPerformanceFrequency(ref _ticksPerSecond);
            GetElapsedTime();// Get rid of first rubbish result
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Gets the elapsed time.
        /// </summary>
        /// <returns></returns>
        public double GetElapsedTime()
        {
            long time = 0;
            QueryPerformanceCounter(ref time);
            double elapsedTime = (double)(time - _previousElapsedTime) / (double)_ticksPerSecond;
            _previousElapsedTime = time;
            return elapsedTime;
        }

        #endregion Public Methods
    }
}