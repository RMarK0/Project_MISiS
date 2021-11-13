using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_MISiS
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        private NavigationService _navigation;
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            _navigation = NavigationService.GetNavigationService(this);
            GamePage gamePage = new GamePage();
            if (_navigation != null) _navigation.Navigate(gamePage);
        }
    }
}
