using System;
using System.Collections.Generic;
using System.Text;

namespace WTS_Game.Core.Models
{
    public abstract class GameObject
    {
        public int PositionX { get; private set; }

        public int PositionY { get; private set; }

        public string Name { get; set; } = string.Empty;

        public virtual bool IsBlocking => true;

        public virtual bool IsLosing => false;

        public virtual bool IsMovable => false;

        public GameObject(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }

        public abstract bool CanMoveTo(int x, int y);

        public virtual bool HasSamePosition(GameObject gameObject)
        {
            if(gameObject.PositionX == PositionX && gameObject.PositionY == PositionY)
            {
                return true;
            }
            return false;
        }

        public virtual void MoveTo(int x, int y)
        {
            if(CanMoveTo(x, y))
            {
                PositionX = x;
                PositionY = y;
            }
        }

        public virtual void ProcessTurn()
        {

        }
    }
}
