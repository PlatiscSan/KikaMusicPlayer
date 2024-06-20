using KiKaMusicPlayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KiKaMusicPlayer.View
{
    /// <summary>
    /// Interaction logic for PlayQueue.xaml
    /// </summary>
    public partial class PlayQueue : UserControl
    {

        private PlayQueueViewModel m_view_model = new();
        public PlayQueue()
        {
            InitializeComponent();
            DataContext = m_view_model;
        }
    }
}
