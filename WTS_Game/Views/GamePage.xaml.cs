using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WTS_Game.Core.Models;
using WTS_Game.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WTS_Game.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public GameViewModel ViewModel { get; } = new GameViewModel();

        public GamePage()
        {
            this.InitializeComponent();
            KeyUp += GamePage_KeyUp;
        }

        private void GamePage_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (ViewModel.IsGameOn)
            {
                switch (e.Key)
                {
                    case Windows.System.VirtualKey.Left:
                        LeftButton_Tapped(null, null);
                        break;
                    case Windows.System.VirtualKey.Right:
                        RightButton_Tapped(null, null);
                        break;
                    case Windows.System.VirtualKey.Up:
                        UpButton_Tapped(null, null);
                        break;
                    case Windows.System.VirtualKey.Down:
                        DownButton_Tapped(null, null);
                        break;
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            FillGameObjects(ViewModel.Initialize());
        }

        private void FillGameObjects(IEnumerable<GameObject> gameObjects)
        {
            m_gameCanvas.Children.Clear();
            foreach(var gameObject in gameObjects)
            {
                FrameworkElement frameworkElement = CreateGameObject(gameObject);
                if(frameworkElement != null)
                {
                    m_gameCanvas.Children.Add(frameworkElement);
                }
                if(gameObject is MovableGameObject movableGameObject)
                {
                    if(gameObject is Monster monster)
                    {
                        bool isFirst = true;
                        foreach(var gameMovement in monster.Movements)
                        {
                            frameworkElement = CreatePatrolGameMovement(gameMovement, isFirst);
                            if (frameworkElement != null)
                            {
                                m_gameCanvas.Children.Add(frameworkElement);
                            }
                            isFirst = false;
                        }
                    }
                }
            }
        }

        private FrameworkElement CreateGameObject(GameObject gameObject)
        {
            ContentControl frameworkElement = new ContentControl();
            if(gameObject is Player)
            {
                frameworkElement.Template = (ControlTemplate)Resources["player"];
            }
            else if(gameObject is Goal)
            {
                frameworkElement.Template = (ControlTemplate)Resources["home"];
            }
            else if (gameObject is Wall)
            {
                frameworkElement.Template = (ControlTemplate)Resources["wall"];
            }
            else if (gameObject is Trap)
            {
                frameworkElement.Template = (ControlTemplate)Resources["trap"];
            }
            else if (gameObject is Monster)
            {
                frameworkElement.Template = (ControlTemplate)Resources["monster"];
            }
            if (frameworkElement != null)
            {
                Canvas.SetTop(frameworkElement, gameObject.PositionY * 40.0);
                Canvas.SetLeft(frameworkElement, gameObject.PositionX * 40.0);
                frameworkElement.Tag = gameObject;
                return frameworkElement;
            }
            return null;
        }

        private FrameworkElement CreateGameMovement(GameMovement gameMovement)
        {
            ContentControl frameworkElement = new ContentControl();
            if (gameMovement is MoveUp)
            {
                frameworkElement.Template = (ControlTemplate)Resources["moveUp"];
            }
            else if (gameMovement is MoveDown)
            {
                frameworkElement.Template = (ControlTemplate)Resources["moveDown"];
            }
            else if (gameMovement is MoveLeft)
            {
                frameworkElement.Template = (ControlTemplate)Resources["moveLeft"];
            }
            else if (gameMovement is MoveRight)
            {
                frameworkElement.Template = (ControlTemplate)Resources["moveRight"];
            }
            if (frameworkElement != null)
            {
                Canvas.SetTop(frameworkElement, gameMovement.PositionY * 40.0);
                Canvas.SetLeft(frameworkElement, gameMovement.PositionX * 40.0);
                frameworkElement.Tag = gameMovement;
                return frameworkElement;
            }
            return null;
        }

        private FrameworkElement CreatePatrolGameMovement(GameMovement gameMovement, bool isFirst)
        {
            ContentControl frameworkElement = new ContentControl();
            if (isFirst)
            {
                if (gameMovement is MoveUp)
                {
                    frameworkElement.Template = (ControlTemplate)Resources["patrolMoveUp"];
                }
                else if (gameMovement is MoveDown)
                {
                    frameworkElement.Template = (ControlTemplate)Resources["patrolMoveDown"];
                }
                else if (gameMovement is MoveLeft)
                {
                    frameworkElement.Template = (ControlTemplate)Resources["patrolMoveLeft"];
                }
                else if (gameMovement is MoveRight)
                {
                    frameworkElement.Template = (ControlTemplate)Resources["patrolMoveRight"];
                }
            }
            else
            {
                frameworkElement.Template = (ControlTemplate)Resources["patrolArea"];
            }
            if (frameworkElement != null)
            {
                Canvas.SetTop(frameworkElement, gameMovement.PositionY * 40.0);
                Canvas.SetLeft(frameworkElement, gameMovement.PositionX * 40.0);
                frameworkElement.Tag = gameMovement;
                return frameworkElement;
            }
            return null;
        }

        private void MoveGameObjects()
        {
            foreach(var item in m_gameCanvas.Children)
            {
                FrameworkElement frameworkElement = item as FrameworkElement;
                if(frameworkElement != null)
                {
                     MoveGameObject(frameworkElement);
                }
            }
        }

        private void MoveGameObject(FrameworkElement frameworkElement)
        {
            GameObject gameObject = frameworkElement.Tag as GameObject;
            if (gameObject != null && gameObject.IsMovable)
            {
                Canvas.SetTop(frameworkElement, gameObject.PositionY * 40.0);
                Canvas.SetLeft(frameworkElement, gameObject.PositionX * 40.0);
            }
        }

        private void UpButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AddMovement(ViewModel.MoveUp());
        }

        private void AddMovement(GameMovement gameMovement)
        {
            if (gameMovement != null)
            {
                FrameworkElement frameworkElement = CreateGameMovement(gameMovement);
                if (frameworkElement != null)
                {
                    m_gameCanvas.Children.Add(frameworkElement);
                }
            }
        }

        private void DownButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AddMovement(ViewModel.MoveDown());
        }

        private void LeftButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AddMovement(ViewModel.MoveLeft());
        }

        private void RightButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AddMovement(ViewModel.MoveRight());
        }

        private void UndoButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(ViewModel.UndoMovement())
            {
                m_gameCanvas.Children.RemoveAt(m_gameCanvas.Children.Count - 1);
            }
        }

        private DispatcherTimer m_dispatcherTimer;

        private void RunButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ViewModel.StartSimulation() && m_dispatcherTimer == null)
            {
                m_dispatcherTimer = new DispatcherTimer();
                m_dispatcherTimer.Tick += SimulationTimer_Tick;
                m_dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 250);
                RemoveGameMovements();
                m_dispatcherTimer.Start();
            }
        }

        private void RestartButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (m_dispatcherTimer == null)
            {
                FillGameObjects(ViewModel.Initialize());
            }
        }

        private void RemoveGameMovements()
        {
            List<FrameworkElement> elementsToDelete = new List<FrameworkElement>();
            foreach (var item in m_gameCanvas.Children)
            {
                FrameworkElement frameworkElement = item as FrameworkElement;
                if (frameworkElement != null && frameworkElement.Tag != null && frameworkElement.Tag is GameMovement)
                {
                    elementsToDelete.Add(frameworkElement);
                }
            }
            if (elementsToDelete.Any())
            {
                foreach (var item in elementsToDelete)
                {
                    m_gameCanvas.Children.Remove(item);
                }
            }
        }

        private void SimulationTimer_Tick(object sender, object e)
        {
            if(ViewModel.SimulateOneRound())
            {
                MoveGameObjects();
            }
            else
            {
                m_dispatcherTimer.Stop();
                m_dispatcherTimer = null;
            }
        }
    }
}
