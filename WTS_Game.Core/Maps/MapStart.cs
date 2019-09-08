using System;
using System.Collections.Generic;
using System.Text;
using WTS_Game.Core.Models;

namespace WTS_Game.Core.Maps
{
    public class MapStart : Map
    {
        public MapStart() : base(10, 10)
        {
            Name = "Start map";
        }

        public override void Initialize()
        {
            m_player = new Player(0, 0, Width, Height);
            m_goal = new Goal(9, 9);
            m_gameObjects.Add(m_goal);
            m_gameObjects.Add(m_player);
            m_gameObjects.Add(new Wall(5, 1));
            m_gameObjects.Add(new Wall(5, 2));
            m_gameObjects.Add(new Wall(5, 3));
            m_gameObjects.Add(new Wall(5, 4));
            m_gameObjects.Add(new Wall(5, 5));
            m_gameObjects.Add(new Wall(5, 6));
            m_gameObjects.Add(new Wall(5, 7));
            m_gameObjects.Add(new Wall(5, 8));
            m_gameObjects.Add(new Trap(3, 0));
            m_gameObjects.Add(new Trap(7, 9));
            Monster monster = new Monster(0, 9);
            monster.SetRectangleRoute(2, 7);
            m_gameObjects.Add(monster);
            monster = new Monster(9, 0);
            monster.SetRectangleRoute(7, 2);
            m_gameObjects.Add(monster);
            MapPoints = 22;
        }
    }
}
