using System;

namespace Utilites
{

    /// <summary>
    /// SystemDateTime is Mock for DatTime where time and speed can be explicit set.
    /// </summary>
    public static class SystemDateTime
    {
        #region Settings

        private static double Amplification { get; set; }


        private static DateTime RealStart { get; set; }


        private static DateTime Initial { get; set; }

        #endregion


        public static DateTime Now => UtcNow.ToLocalTime();


        /// <summary>
        /// Return the current date acording to Local Time.
        /// </summary>
        public static DateTime Today => UtcNow.ToLocalTime().Date;


        /// <summary>
        /// Return Today+1.
        /// </summary>
        public static DateTime Tomorrow => UtcNow.ToLocalTime().Date.AddDays(1);


        /// <summary>
        /// Return Today-1
        /// </summary>
        public static DateTime Yesterday => UtcNow.ToLocalTime().Date.AddDays(-1);



        public static DateTime UtcNow
        {
            get
            {
                var realDuration = DateTime.UtcNow.Subtract(RealStart);

                var realDurationTicks = realDuration.Ticks;

                var simulatedDurationTicks = (long)(realDurationTicks * Amplification);

                var result = Initial.AddTicks(simulatedDurationTicks);

                return result;
            }
        }


        /// <summary>
        /// Sets DateTime (Default = Now) and optionally set the Amplification (Default = realtime).
        /// Amplification:
        /// - 1.0 means real time speed (Default)
        /// - 2.0 means the time runs at double speed.
        /// - 0.5 means the time runs at half speed.
        /// - 0.0 means the time stands still.
        /// - -1.0 means time runs backwards at real time speed.
        /// </summary>
        /// <param name="initialDateTime"></param>
        /// <param name="amplification"></param>
        public static void SetTime(DateTime? initialDateTime, double amplification = 1d)
        {
            RealStart = DateTime.UtcNow;
            Initial = initialDateTime ?? RealStart;
            Amplification = amplification;
        }


        /// <summary>
        /// Calculates the duration in ms between from and until divided by the SystemDateTimeAmplification.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="until"></param>
        /// <returns></returns>
        public static double DurationMs(this DateTime from, DateTime until)
        {
            return until.Subtract(from).TotalMilliseconds/Amplification;
        }

        /// <summary>
        /// Resets the SystemTimeStub to use the (MS) System.DateTime.
        /// </summary>
        public static void Reset()
        {
            SetTime(null);
        }


        static SystemDateTime()
        {
            Reset();
        }

    }
}
