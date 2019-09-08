using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTS_Game.Core.Models
{
    public abstract class Map
    {
        public enum MoveDirection { Up, Down, Left, Right }

        public int Width { get; }
        public int Height { get; }

        protected Player m_player;
        public Player Player => m_player;

        protected Goal m_goal;

        protected readonly List<GameObject> m_gameObjects = new List<GameObject>();
        public IEnumerable<GameObject> GameObjects => m_gameObjects;

        public string Name { get; protected set; } = string.Empty;

        public int MapPoints { get; protected set; }

        public bool IsWinning => m_player.HasSamePosition(m_goal);

        public bool IsLosing
        {
            get
            {
                var losingObjects = m_gameObjects.Where(o => o.IsLosing);
                return losingObjects.Any(o => o.HasSamePosition(m_player));
            }
        }

        protected Map(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public abstract void Initialize();

        public void ProcessOtherMovements()
        {
            var movableObjects = m_gameObjects.Where(g => g.IsMovable && !(g is Player));
            foreach(GameObject gameObject in movableObjects)
            {
                gameObject.ProcessTurn();
            }
        }

        public void ProcessPlayerMove(MoveDirection moveDirection)
        {
            int x = m_player.PositionX;
            int y = m_player.PositionY;
            switch(moveDirection)
            {
                case MoveDirection.Down:
                    y++;
                    break;
                case MoveDirection.Left:
                    x--;
                    break;
                case MoveDirection.Right:
                    x++;
                    break;
                case MoveDirection.Up:
                    y--;
                    break;
            }
            if(m_player.CanMoveTo(x, y))
            {
                var blockingObjects = m_gameObjects.Where(o => o.IsBlocking);
                if(!blockingObjects.Any(o => o.PositionX == x && o.PositionY == y))
                {
                    m_player.MoveTo(x, y);
                }
            }
        }
    }
}
