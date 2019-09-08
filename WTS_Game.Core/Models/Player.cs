using System;
using System.Collections.Generic;
using System.Text;

namespace WTS_Game.Core.Models
{
    public class Player : MovableGameObject
    {
        private readonly int m_mapWidth;
        private readonly int m_mapHeight;

        public Player(int x, int y, int mapWidth, int mapHeight) : base(x, y)
        {
            m_mapWidth = mapWidth;
            m_mapHeight = mapHeight;
        }

        public override bool CanMoveTo(int x, int y)
        {
            if(x < 0 || x >= m_mapWidth || y < 0 || y >= m_mapHeight)
            {
                return false;
            }
            return true;
        }
    }
}
