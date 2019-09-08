using System;
using System.Collections.Generic;
using System.Text;
using WTS_Game.Core.Models;

namespace WTS_Game.Core.Maps
{
    public class MapSecond : Map
    {
        public MapSecond() : base(10, 10)
        {
            Name = "Second map";
        }

        public override void Initialize()
        {
            m_player = new Player(0, 0, Width, Height);
            m_goal = new Goal(9, 9);
            m_gameObjects.Add(m_goal);
            m_gameObjects.Add(m_player);
            m_gameObjects.Add(new Wall(0, 5));
            m_gameObjects.Add(new Wall(1, 5));
            m_gameObjects.Add(new Wall(2, 5));
            m_gameObjects.Add(new Wall(3, 5));
            m_gameObjects.Add(new Wall(5, 5));
            m_gameObjects.Add(new Wall(6, 5));
            m_gameObjects.Add(new Wall(8, 5));
            m_gameObjects.Add(new Wall(9, 5));
            m_gameObjects.Add(new Trap(3, 3));
            m_gameObjects.Add(new Trap(4, 3));
            m_gameObjects.Add(new Trap(5, 3));
            m_gameObjects.Add(new Trap(6, 7));
            m_gameObjects.Add(new Trap(7, 7));
            m_gameObjects.Add(new Trap(8, 7));
            Monster monster = new Monster(4, 4);
            monster.SetRectangleRoute(7, 6);
            m_gameObjects.Add(monster);
            MapPoints = 22;
        }
    }
}
