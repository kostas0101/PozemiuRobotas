using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PozemiuRobotas
{
    public static class Player
    {
        public class Robot
        {
            public int botx;
            public int boty;
            public int moveCounter;
            public int roomNr;
            public bool gotKey;

            public Robot(int botx, int boty)
            {
                this.botx = botx;
                this.boty = boty;
                this.moveCounter = 0;
                this.roomNr = 0;
                this.gotKey = false;
            }
        }
    }
}
