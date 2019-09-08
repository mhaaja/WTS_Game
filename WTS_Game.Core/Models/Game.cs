using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTS_Game.Core.Maps;

namespace WTS_Game.Core.Models
{
    public class Game
    {
        private Map m_map;

        public IEnumerable<GameObject> GameObjects
        {
            get
            {
                if(m_map == null)
                {
                    return new List<GameObject>();
                }
                return m_map.GameObjects;
            }
        }

        public int Points { get; private set; }

        public bool IsGameOn { get; private set; }

        public bool IsSimulationOn { get; private set; }

        public string GameMessage { get; private set; } = string.Empty;

        private int m_mapMovements;

        private List<GameMovement> m_gameMovements = new List<GameMovement>();
        public IEnumerable<GameMovement> GameMovements => m_gameMovements;

        private int m_mapNumber = 0;

        public Game()
        {

        }

        public Map LoadMap()
        {
            if (m_map == null)
            {
                m_map = new MapStart();
            }
            else
            {
                if(m_map.IsWinning)
                {
                    m_mapNumber++;
                }
                if (m_mapNumber == 1)
                {
                    m_map = new MapSecond();
                }
                else
                {
                    m_map = new MapStart();
                    m_mapNumber = 0;
                }
            }
            m_map.Initialize();
            m_mapMovements = 0;
            IsGameOn = true;
            IsSimulationOn = false;
            GameMessage = string.Empty;
            m_gameMovements.Clear();
            return m_map;
        }

        public GameMovement MoveUp()
        {
            (int x, int y) = FindCurrentLocation();
            GameMovement gameMovement = new MoveUp(x, y);
            return ProcessMovement(gameMovement);
        }

        public GameMovement MoveDown()
        {
            (int x, int y) = FindCurrentLocation();
            GameMovement gameMovement = new MoveDown(x, y);
            return ProcessMovement(gameMovement);
        }

        public GameMovement MoveLeft()
        {
            (int x, int y) = FindCurrentLocation();
            GameMovement gameMovement = new MoveLeft(x, y);
            return ProcessMovement(gameMovement);
        }

        public GameMovement MoveRight()
        {
            (int x, int y) = FindCurrentLocation();
            GameMovement gameMovement = new MoveRight(x, y);
            return ProcessMovement(gameMovement);
        }

        private GameMovement ProcessMovement(GameMovement gameMovement)
        {
            if (gameMovement.PositionX >= 0 && gameMovement.PositionX < m_map.Width && gameMovement.PositionY >= 0 && gameMovement.PositionY < m_map.Height)
            {
                if (!m_gameMovements.Any(m => m.IsSamePosition(gameMovement)))
                {
                    m_gameMovements.Add(gameMovement);
                    return gameMovement;
                }
            }
            return null;
        }

        private (int x, int y) FindCurrentLocation()
        {
            if (IsGameOn)
            {
                if (m_gameMovements.Any())
                {
                    var movement = m_gameMovements.Last();
                    return (movement.PositionX, movement.PositionY);
                }
                return (m_map.Player.PositionX, m_map.Player.PositionY);
            }
            return (0, 0);
        }

        public bool StartSimulation()
        {
            if(IsGameOn && m_gameMovements.Any())
            {
                IsGameOn = false;
                IsSimulationOn = true;
                return true;
            }
            return false;
        }

        public bool SimulateOneRound()
        {
            if (!IsSimulationOn) return false;
            m_map.ProcessOtherMovements();
            if(m_gameMovements.Any())
            {
                GameMovement gameMovement = m_gameMovements.First();
                m_gameMovements.Remove(gameMovement);
                ProcessPlayerMovement(gameMovement.Direction);
                return true;
            }
            if(!(m_map.IsWinning || m_map.IsLosing))
            {
                GameMessage = "You lost, because no more moves!";
            }
            IsSimulationOn = false;
            return false;
        }

        private void ProcessPlayerMovement(Map.MoveDirection moveDirection)
        {
            if(m_map != null && IsSimulationOn)
            {
                m_map.ProcessPlayerMove(moveDirection);
                m_mapMovements++;
                if(m_map.IsWinning)
                {
                    GameMessage = "You win!";
                    Points += Math.Max(1, m_map.MapPoints - m_mapMovements);
                    IsSimulationOn = false;
                }
                else if(m_map.IsLosing)
                {
                    GameMessage = "You lost!";
                    IsSimulationOn = false;
                }
            }
        }
    }
}
