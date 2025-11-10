using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PozemiuRobotas.Player;

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
            public int battery;


            public Robot(int botx, int boty, int battery)
            {
                this.botx = botx;
                this.boty = boty;
                this.moveCounter = 0;
                this.roomNr = 0;
                this.gotKey = false;
                this.battery = battery;
            }

            public void addBattery()
            {
                battery += 100;
                if (battery > 200)
                    battery = 200;
            }

            public void drawBattery()
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n" + "Battery level: " + battery + "\n" + "|");
                for (int i = 0; i < battery / 10; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("█");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("|");
                }
            }
        }
    }
}
