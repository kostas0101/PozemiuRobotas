using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PozemiuRobotas.Player;

namespace PozemiuRobotas
{

    public static class Obstacles
    {
        public class Obstacle
        {
            private int _X { set; get; }
            private int _Y { set; get; }
            private bool _Status { set; get; }


            public Action MoveUp => () => _Y--;
            public Action MoveDown => () => _Y++;
            public Action MoveLeft => () => _X--;
            public Action MoveRight => () => _X++;


            public Action<bool> SetStatus => (value) => _Status = value;
            public Action<int> SetX => (value) => _X = value;
            public Action<int> SetY => (value) => _Y = value;
            public int GetX() => _X;
            public int GetY() => _Y;
            public bool GetStatus() => _Status;
        }

        public class Saw : Obstacle
        {
            private bool _MovingDown
            {
                get => GetStatus();
                set => SetStatus(value);
            }

            public Saw(int x, int y, bool movingDown)
            {
                SetX(x);
                SetY(y);
                _MovingDown = movingDown;
                //this.X = x;
                //this.Y = y;
                //this.GoingUp = movingDown;
            }

            public void Move(int[,] map)
            {
                if (_MovingDown && map[GetY() + 1, GetX()] != 1)
                    _MovingDown = false;
                else if (!_MovingDown && map[GetY() - 1, GetX()] != 1)
                    _MovingDown = true;

                if (_MovingDown)
                    MoveDown();
                else
                    MoveUp();
            }
        }

        public class Spyke : Obstacle
        {
            public bool _Extended
            {
                get => GetStatus();
                set => SetStatus(value);
            }

            public Spyke(int x, int y, bool extended)
            {
                SetX(x);
                SetY(y);
                //this.X = x;
                //this.Y = y;
                this._Extended = extended;
            }

            public void Move()
            {
                _Extended = !_Extended;
            }

        }

        public class Enamy : Obstacle
        {
            private bool _Active
            {
                get => GetStatus();
                set => SetStatus(value);
            }
            private int _ReleasCounter;

            public Action IncresReleasCounter => () => _ReleasCounter++;
            public int GetReleasCounter() => _ReleasCounter;

            public Enamy(int x, int y, bool active)
            {
                SetX(x);
                SetY(y);
                //this.X = x;
                //this.Y = y;
                this._Active = active;
                this._ReleasCounter = 0;
            }

            public void Chase(int RobotX, int RobotY)
            {
                if (RobotX < GetX())
                    MoveLeft();
                if (RobotX > GetX())
                    MoveRight();
                if (RobotY < GetY())
                    MoveUp();
                if (RobotY > GetY())
                    MoveDown();
            }
        }

    }
}
