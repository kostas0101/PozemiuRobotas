namespace PozemiuRobotas2
{
    public static class Player
    {
        public class Robot
        {
            private int _RobotX;
            private int _RobotY;
            private int _MoveCounter;
            private int _RoomKeyNr;
            private bool _GateKey;
            private int _BatteryLevel;

            public void MoveUp() => _RobotY--;
            public void MoveDown() => _RobotY++;
            public void MoveLeft() => _RobotX--;
            public void MoveRight() => _RobotX++;
            public void IncressMoveCounter() => _MoveCounter++;
            public void ResetMoveCounter() => _MoveCounter = 0;
            public void IncressRoomKeyNumber() => _RoomKeyNr++;
            public void GotGateKey() => _GateKey = true;
            public void DecresBatteryLevel() => _BatteryLevel--;

            public int GetX() => _RobotX;
            public int GetY() => _RobotY;
            public int GetMoveCounter() => _MoveCounter;
            public int GetRoomKeyNumber() => _RoomKeyNr;
            public bool HasGateKey() => _GateKey;
            public int GetBatteryLevel() => _BatteryLevel;


            public Robot(int robotX, int robotY, int battery)
            {

                _RobotX = robotX;
                _RobotY = robotY;
                _MoveCounter = 0;
                _RoomKeyNr = 2;
                _GateKey = false;
                _BatteryLevel = battery;
            }

            public void AddBattery()
            {
                _BatteryLevel += batteryAdd;
                if (_BatteryLevel > batteryMax)
                    _BatteryLevel = batteryMax;
            }

            public void DrawBattery()
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nBattery level: " + _BatteryLevel + "\n|");
                for (int i = 0; i < _BatteryLevel / 10; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("█");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("|");
                }
            }
        }

        private const int batteryMax = 200;
        private const int batteryAdd = 100;
    }
}
