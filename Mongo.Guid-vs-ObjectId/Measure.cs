using System;
using System.Collections.Generic;

namespace Mongo.Guid_vs_ObjectId
{
    public static class Measure
    {
        public static long LastMeasurmentTime { get; set; }

        private static readonly System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        
        /// <summary>
        /// Will be executed at the end of each performance measurement, by default writes elapsed ms to Console
        /// </summary>
        public static Action<string, long> WhenDone = (prefix, elapsedTime) => Console.WriteLine(elapsedTime + "ms");

        public static void Performance(Action codeToMeasure, string prefix = "")
        {
            stopwatch.Reset();
            stopwatch.Start();

            codeToMeasure.Invoke();

            stopwatch.Stop();

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            WhenDone.Invoke(prefix, elapsedMilliseconds);
            LastMeasurmentTime = elapsedMilliseconds;
        }


        public static void Performance(string prefix, Action codeToMeasure)
        {
           Performance(codeToMeasure, prefix);
        }
    }
}
