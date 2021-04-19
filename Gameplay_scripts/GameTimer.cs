using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameScripts
{
    class GameTimer
    {
        private static System.Timers.Timer timer = new System.Timers.Timer(10);
        private static System.Timers.Timer continueTimer = new System.Timers.Timer(1000);
        public static bool Pause = false;
        public static float elapsedTime = 0;
        public static int continueTime = 3;

        public static void Start()
        {
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
            Pause = false;
        }
        public static void Stop()
        {
            timer.Stop();
            timer.Elapsed -= Timer_Elapsed;
            Pause = true;
        }
        public static void Reset()
        {
            elapsedTime = 0;
        }

        public static void ContinueStart()
        {
            continueTimer.Start();
            continueTimer.Elapsed += ContinueTimer_Elapsed;
        }
        public static void ContinueStop()
        {
            continueTimer.Stop();
            continueTimer.Elapsed -= ContinueTimer_Elapsed;
            continueTime = 3;
        }

        private static void ContinueTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            continueTime--;
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            elapsedTime += 0.01F;
        }
    }
}