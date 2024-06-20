using KiKaMusicPlayer.Dialog;
using KiKaMusicPlayer.Model;
using KiKaMusicPlayer.Utilities;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace KiKaMusicPlayer.ViewModel
{
    class PlaylistViewModel : BaseViewModel
    {

        private ObservableCollection<PlaylistModel> m_playlists = [];
        public ObservableCollection<PlaylistModel> Playlists
        {
            get => m_playlists;
        }

        private Visibility m_muti_select_stackpanel_visbility = Visibility.Collapsed;
        public Visibility MutiSelectStackPanelVisbility
        {
            get => m_muti_select_stackpanel_visbility;
        }

        private string m_check_text;
        public string CheckText
        {
            get => m_check_text;
            private set
            {
                m_check_text = value;
                OnPropertyChanged(nameof(CheckText));
            }
        }

        public PlaylistViewModel()
        {
            CreateCommand = new RelayCommand(Create);
        }

        public ICommand CreateCommand { get; private set; }
        private void Create(object parameter)
        {
            MessageBus.Instance.Publish(new DialogMessage(DialogMessageType.Show, new NewPlaylistDialog { CreateCommand = new RelayCommand(DialogCreate) }));
        }

        private void DialogCreate(object parameter)
        {
            string playlist_name = (string)parameter;
            if(playlist_name == string.Empty || playlist_name == null)
            {
                return;
            }
            m_playlists.Add(new PlaylistModel { PlaylistName = playlist_name});
            MessageBus.Instance.Publish(new DialogMessage(DialogMessageType.Close, null));
        }


    }
}
