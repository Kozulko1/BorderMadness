using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameScripts
{
    public class LeaderboardEntry
    {
        public string uid;
        public int score = 0;

        public LeaderboardEntry()
        {

        }

        public LeaderboardEntry(string uid, int score)
        {
            this.uid = uid;
            this.score = score;
        }
    }
}
