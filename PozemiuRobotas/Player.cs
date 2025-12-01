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
            private int _RobotX { get; set; }
            private int _RobotY { get; set; }
            private int _MoveCounter { get; set; }
            private int _RoomKeyNr { get; set; }
            private bool _GateKey { get; set; }
            private int _BatteryLevel { get; set; }

            public Action MoveUp => () => _RobotY--;
            public Action MoveDown => () => _RobotY++;
            public Action MoveLeft => () => _RobotX--;
            public Action MoveRight => () => _RobotX++;
            public Action IncressMoveCounter => () => _MoveCounter++;
            public Action REsetMoveCounter => () => _MoveCounter = 0;
            public Action IncressRoomKeyNumber => () => _RoomKeyNr++;
            public Action GotGateKey => () => _GateKey = true;
            public Action DecresBatteryLevel => () => _BatteryLevel--;

            public int GetX() => _RobotX;
            public int GetY() => _RobotY;
            public int GetMoveCounter() => _MoveCounter;
            public int GetRoomKeyNumber() => _RoomKeyNr;
            public bool HasGateKey() => _GateKey;
            public int GetBatteryLevel() => _BatteryLevel;


            public Robot(int robotX, int robotY, int battery)
            {

                this._RobotX = robotX;
                this._RobotY = robotY;
                this._MoveCounter = 0;
                this._RoomKeyNr = 2;
                this._GateKey = false;
                this._BatteryLevel = battery;
            }

            public void addBattery()
            {
                _BatteryLevel += 100;
                if (_BatteryLevel > 200)
                    _BatteryLevel = 200;
            }

            public void drawBattery()
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n" + "Battery level: " + _BatteryLevel + "\n" + "|");
                for (int i = 0; i < _BatteryLevel / 10; i++)
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
