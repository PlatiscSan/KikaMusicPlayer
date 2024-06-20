
using KiKaMusicPlayer.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace KiKaMusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NavigationViewModel m_view_model;

        public MainWindow()
        {
            InitializeComponent();
            m_view_model = new NavigationViewModel();
            DataContext = m_view_model;
        }


    }
}