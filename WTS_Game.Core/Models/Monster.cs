using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTS_Game.Core.Models
{
    public class Monster : MovableGameObject
    {
        public override bool IsBlocking => false;
        public override bool IsLosing => true;

        private List<GameMovement> m_movements = new List<GameMovement>();
        public IReadOnlyList<GameMovement> Movements => m_movements;

        public Monster(int x, int y) : base(x, y)
        {

        }

        public void SetRectangleRoute(int x, int y)
        {
            if(y < PositionY)
            {
                if(x < PositionX)
                {
                    AddFirstHorizontal(x, y);
                }
                else if(x > PositionX)
                {
                    AddFirstVertical(x, y);
                }
                else
                {
                    AddVerticalMovements(PositionY, y, PositionX);
                    AddVerticalMovements(y, PositionY, PositionX);
                }
            }
            else if(y > PositionY)
            {
                if(x < PositionX)
                {
                    AddFirstVertical(x, y);
                }
                else if(x > PositionX)
                {
                    AddFirstHorizontal(x, y);
                }
                else
                {
                    AddVerticalMovements(PositionY, y, PositionX);
                    AddVerticalMovements(y, PositionY, PositionX);
                }
            }
            else
            {
                if(x != PositionX)
                {
                    AddHorizontalMovements(PositionX, x, PositionY);
                    AddHorizontalMovements(x, PositionX, PositionY);
                }
            }
        }

        private void AddFirstHorizontal(int x, int y)
        {
            AddHorizontalMovements(PositionX, x, PositionY);
            AddVerticalMovements(PositionY, y, x);
            AddHorizontalMovements(x, PositionX, y);
            AddVerticalMovements(y, PositionY, PositionX);
        }

        private void AddFirstVertical(int x, int y)
        {
            AddVerticalMovements(PositionY, y, PositionX);
            AddHorizontalMovements(PositionX, x, y);
            AddVerticalMovements(y, PositionY, x);
            AddHorizontalMovements(x, PositionX, PositionY);
        }

        public override bool CanMoveTo(int x, int y)
        {
            return true;
        }

        public override void ProcessTurn()
        {
            if(m_movements.Any())
            {
                GameMovement gameMovement = m_movements.First();
                m_movements.Remove(gameMovement);
                m_movements.Add(gameMovement);
                MoveTo(gameMovement.PositionX, gameMovement.PositionY);
            }
        }

        private void AddHorizontalMovements(int startX, int endX, int y)
        {
            bool isLeft = startX > endX;
            int direction = isLeft ? -1 : 1;
            for (int i = startX; i != endX; i += direction)
            {
                if(isLeft)
                {
                    m_movements.Add(new MoveLeft(i, y));
                }
                else
                {
                    m_movements.Add(new MoveRight(i, y));
                }
            }
        }
        private void AddVerticalMovements(int startY, int endY, int x)
        {
            bool isUp = startY > endY;
            int direction = isUp ? -1 : 1;
            for (int i = startY; i != endY; i += direction)
            {
                if (isUp)
                {
                    m_movements.Add(new MoveUp(x, i));
                }
                else
                {
                    m_movements.Add(new MoveDown(x, i));
                }
            }
        }
    }
}
