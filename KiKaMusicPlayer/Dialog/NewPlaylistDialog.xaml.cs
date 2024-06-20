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

namespace KiKaMusicPlayer.Dialog
{
    /// <summary>
    /// Interaction logic for NewPlaylistDialog.xaml
    /// </summary>
    public partial class NewPlaylistDialog : UserControl
    {
        public NewPlaylistDialog()
        {
            InitializeComponent();
        }



        public ICommand CreateCommand
        {
            get { return (ICommand)GetValue(CreateCommandProperty); }
            set { SetValue(CreateCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CreateCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CreateCommandProperty =
            DependencyProperty.Register("CreateCommand", typeof(ICommand), typeof(NewPlaylistDialog));



        public string PlaylistName
        {
            get { return (string)GetValue(PlaylistNameProperty); }
            set { SetValue(PlaylistNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlaylistName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaylistNameProperty =
            DependencyProperty.Register("PlaylistName", typeof(string), typeof(NewPlaylistDialog));



    }
}
