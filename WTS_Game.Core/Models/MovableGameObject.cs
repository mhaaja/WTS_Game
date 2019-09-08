using System;
using System.Collections.Generic;
using System.Text;

namespace WTS_Game.Core.Models
{
    public abstract class MovableGameObject : GameObject
    {
        public int OldPositionX { get; private set; }
        public int OldPositionY { get; private set; }

        public override bool IsMovable => true;

        public MovableGameObject(int x, int y)  : base(x, y)
        {
            OldPositionX = x;
            OldPositionY = y;
        }

        public override bool HasSamePosition(GameObject gameObject)
        {
            if (gameObject.PositionX == PositionX && gameObject.PositionY == PositionY)
            {
                return true;
            }
            if(gameObject is MovableGameObject movableGameObject)
            {
                if(movableGameObject.OldPositionX == PositionX && movableGameObject.OldPositionY == PositionY &&
                    movableGameObject.PositionX == OldPositionX && movableGameObject.PositionY == OldPositionY)
                {
                    return true;
                }
            }
            return false;
        }

        public override void MoveTo(int x, int y)
        {
            OldPositionX = PositionX;
            OldPositionY = PositionY;
            base.MoveTo(x, y);
        }

    }
}
