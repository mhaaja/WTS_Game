using System;

using Windows.UI.Xaml.Controls;
using WTS_Game.Core.Helpers;
using WTS_Game.Services;
using WTS_Game.ViewModels;

namespace WTS_Game.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }

        private void PlayGameButton_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(GamePage));
        }
    }
}
