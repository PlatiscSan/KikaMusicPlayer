using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace KiKaMusicPlayer.Model
{
    class PlaylistModel
    {

        private string m_playlist_name = string.Empty;
        public string PlaylistName
        {
            get => m_playlist_name;
            set => m_playlist_name = value;
        }

        private BitmapImage m_playlist_cover;
        public BitmapImage PlaylistCover
        {
            get => m_playlist_cover;
            set => m_playlist_cover = value;
        }

        private ObservableCollection<SongModel> m_songs = [];
        public ObservableCollection<SongModel> Songs
        {
            get => m_songs;
            set => m_songs = value;
        }

    }
}
