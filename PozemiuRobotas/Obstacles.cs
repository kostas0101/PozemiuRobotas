using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PozemiuRobotas
{
    public static class Obstacles
    {
        public class Saw
        {
            public int x;
            public int y;
            public bool goingUp;

            public Saw(int x, int y, bool goingUp)
            {
                this.x = x;
                this.y = y;
                this.goingUp = goingUp;
            }

            public void Move(int[,] map)
            {
                if (goingUp && map[y + 1, x] != 1)
                    goingUp = false;
                else if (!goingUp && map[y - 1, x] != 1)
                    goingUp = true;

                if (goingUp)
                    y++;
                else
                    y--;
            }
        }

        public class Spyke
        {
            public int x;
            public int y;
            public bool up;

            public Spyke(int x, int y, bool up)
            {
                this.x = x;
                this.y = y;
                this.up = up;
            }

            public void Move()
            {
                up = !up;
            }

        }

        public class Enamy
        {
            public int x;
            public int y;
            public bool active;
            public int releasCounter;

            public Enamy(int x, int y, bool active)
            {
                this.x = x;
                this.y = y;
                this.active = active;
                this.releasCounter = 0;
            }

            public void Chase(int botx, int boty)
            {
                if (botx < x)
                    x--;
                if (botx > x)
                    x++;
                if (boty < y)
                    y--;
                if (boty > y)
                    y++;
            }
        }

    }
}
