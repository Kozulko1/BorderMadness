using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameScripts
{
    public class User
    {
        public string username;
        public int score;

        public User()
        {
        }

        public User(string username, int score)
        {
            this.username = username;
            this.score = score;
        }
    }
}
