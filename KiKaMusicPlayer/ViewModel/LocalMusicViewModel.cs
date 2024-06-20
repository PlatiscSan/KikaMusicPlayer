using KiKaMusicPlayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiKaMusicPlayer.ViewModel
{
    class LocalMusicViewModel : BaseViewModel
    {
        private bool m_is_songs_tab_selected = true;
        public bool IsSongsTabSelected
        {
            get=>m_is_songs_tab_selected;
            set
            {
                m_is_songs_tab_selected = value;
                OnPropertyChanged(nameof(IsSongsTabSelected));
            }
        }

        private bool m_is_albums_tab_selected = false;
        public bool IsAlbumsTabSelected
        {
            get => m_is_albums_tab_selected;
            set
            {
                m_is_albums_tab_selected = value;
                OnPropertyChanged(nameof(IsAlbumsTabSelected));
            }
        }

        private bool m_is_artists_tab_selected = false;
        public bool IsArtistsTabSelected
        {
            get => m_is_artists_tab_selected;
            set
            {
                m_is_artists_tab_selected = value;
                OnPropertyChanged(nameof(IsArtistsTabSelected));
            }
        }


    }
}
