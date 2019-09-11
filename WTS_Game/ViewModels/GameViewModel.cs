using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using WTS_Game.Core.Models;
using WTS_Game.Helpers;

namespace WTS_Game.ViewModels
{
    public class GameViewModel : Observable
    {
        private Game m_game;

        private string m_mapName = string.Empty;
        public string MapName
        {
            get => m_mapName;
            private set
            {
                Set(ref m_mapName, value);
            }
        }

        private int m_points;
        public int Points
        {
            get => m_points;
            private set
            {
                Set(ref m_points, value);
            }
        }

        private string m_message;
        public string Message
        {
            get => m_message;
            set
            {
                Set(ref m_message, value);
            }
        }

        private bool m_isGameOn = true;
        public bool IsGameOn
        {
            get => m_isGameOn;
            private set
            {
                if(Set(ref m_isGameOn, value))
                {
                    OnPropertyChanged(nameof(BeforeSimulationVisiblity));
                    OnPropertyChanged(nameof(AfterSimulationVisiblity));
                    OnPropertyChanged(nameof(IsUndoPossible));
                    OnPropertyChanged(nameof(IsRunPossible));
                }
            }
        }

        public bool IsUndoPossible => m_isGameOn & m_game.GameMovements.Count() > 0;

        public bool IsRunPossible => m_isGameOn & m_game.GameMovements.Count() > 0;

        public Visibility BeforeSimulationVisiblity => m_isGameOn ? Visibility.Visible : Visibility.Collapsed;

        private bool m_isSimulationOn;
        public bool IsSimulationOn
        {
            get => m_isSimulationOn;
            private set
            {
                if(Set(ref m_isSimulationOn, value))
                {
                    OnPropertyChanged(nameof(AfterSimulationVisiblity));
                }
            }
        }

        public Visibility AfterSimulationVisiblity => !m_isGameOn && !m_isSimulationOn ? Visibility.Visible : Visibility.Collapsed;

        private string m_simulationMessage = string.Empty;
        public string SimulationMessage
        {
            get => m_simulationMessage;
            set
            {
                Set(ref m_simulationMessage, value);
            }
        }

        public GameViewModel()
        {
            m_game = new Game();
        }

        public IEnumerable<GameObject> Initialize()
        {
            Map map = m_game.LoadMap();
            IsGameOn = m_game.IsGameOn;
            IsSimulationOn = m_game.IsSimulationOn;
            MapName = map.Name;
            Points = m_game.Points;
            Message = m_game.GameMessage;
            return m_game.GameObjects;
        }

        public GameMovement MoveUp()
        {
            if(m_game.IsGameOn && !m_game.IsSimulationOn)
            {
                GameMovement result = m_game.MoveUp();
                OnPropertyChanged(nameof(IsUndoPossible));
                return result;
            }
            return null;
        }

        public GameMovement MoveDown()
        {
            if (m_game.IsGameOn && !m_game.IsSimulationOn)
            {
                GameMovement result = m_game.MoveDown();
                OnPropertyChanged(nameof(IsUndoPossible));
                OnPropertyChanged(nameof(IsRunPossible));
                return result;
            }
            return null;
        }

        public GameMovement MoveLeft()
        {
            if (m_game.IsGameOn && !m_game.IsSimulationOn)
            {
                GameMovement result = m_game.MoveLeft();
                OnPropertyChanged(nameof(IsUndoPossible));
                OnPropertyChanged(nameof(IsRunPossible));
                return result;
            }
            return null;
        }

        public GameMovement MoveRight()
        {
            if (m_game.IsGameOn && !m_game.IsSimulationOn)
            {
                GameMovement result = m_game.MoveRight();
                OnPropertyChanged(nameof(IsUndoPossible));
                OnPropertyChanged(nameof(IsRunPossible));
                return result;
            }
            return null;
        }

        public bool UndoMovement()
        {
            if (m_game.IsGameOn && !m_game.IsSimulationOn)
            {
                bool result = m_game.UndoMovement();
                OnPropertyChanged(nameof(IsUndoPossible));
                OnPropertyChanged(nameof(IsRunPossible));
                return result;
            }
            return false;
        }

        public bool StartSimulation()
        {
            if(m_game.IsGameOn)
            {
                if(m_game.StartSimulation())
                {
                    SimulationMessage = string.Format("Simulating moves left {0}", m_game.GameMovements.Count());
                    IsGameOn = false;
                    IsSimulationOn = true;
                    return true;
                }
            }
            return false;
        }

        public bool SimulateOneRound()
        {
            if (m_game.IsSimulationOn)
            {
                bool isOn = m_game.SimulateOneRound();
                SimulationMessage = string.Format("Simulating moves left {0}", m_game.GameMovements.Count());
                Points = m_game.Points;
                Message = m_game.GameMessage;
                IsSimulationOn = isOn;
                return isOn;
            }
            IsSimulationOn = false;
            return false;
        }

    }
}
