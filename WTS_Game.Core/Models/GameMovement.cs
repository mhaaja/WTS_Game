using System;
using System.Collections.Generic;
using System.Text;

namespace WTS_Game.Core.Models
{
    public abstract class GameMovement
    {
        public int PositionX { get; private set; }

        public int PositionY { get; private set; }

        public abstract Map.MoveDirection Direction { get; }

        public GameMovement(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }

        public bool IsSamePosition(GameMovement gameMovement)
        {
            if(gameMovement.PositionX == PositionX && gameMovement.PositionY == PositionY)
            {
                return true;
            }
            return false;
        }
    }

    public class MoveUp : GameMovement
    {
        public override Map.MoveDirection Direction => Map.MoveDirection.Up;

        public MoveUp(int fromX, int fromY) : base(fromX, fromY-1)
        {

        }
    }

    public class MoveDown : GameMovement
    {
        public override Map.MoveDirection Direction => Map.MoveDirection.Down;

        public MoveDown(int fromX, int fromY) : base(fromX, fromY + 1)
        {

        }
    }

    public class MoveLeft : GameMovement
    {
        public override Map.MoveDirection Direction => Map.MoveDirection.Left;

        public MoveLeft(int fromX, int fromY) : base(fromX - 1, fromY)
        {

        }
    }

    public class MoveRight : GameMovement
    {
        public override Map.MoveDirection Direction => Map.MoveDirection.Right;

        public MoveRight(int fromX, int fromY) : base(fromX + 1, fromY)
        {

        }
    }
}
