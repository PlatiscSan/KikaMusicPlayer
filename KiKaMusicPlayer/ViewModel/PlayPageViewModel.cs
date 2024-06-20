using KiKaMusicPlayer.Utilities;
using KiKaMusicPlayer.Module;
using System.Windows.Media.Imaging;

namespace KiKaMusicPlayer.ViewModel
{
    class PlayPageViewModel : BaseViewModel
    {
        private BitmapImage m_album_cover;
        public BitmapImage AlbumCover
        {
            get { return m_album_cover; }
            set
            {
                m_album_cover = value; 
                OnPropertyChanged(nameof(AlbumCover));
            }

        }

        private string m_title;
        public string Title 
        {
            get { return m_title; }
            set
            {
                m_title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string m_artists;
        public string Artists
        {
            get { return m_artists; }
            set 
            { 
                m_artists = value; 
                OnPropertyChanged(nameof(Artists));
            }
        }

        private string m_album;
        public string Album
        {
            get { return m_album; }
            set
            {
                m_album = value;
                OnPropertyChanged(nameof(Album));
            }
        }

        public PlayPageViewModel()
        {
            MessageBus.Instance.Subscribe<PlayStateChangedMessage>(PlayStateChanged);
        }

        private void PlayStateChanged(PlayStateChangedMessage message)
        {

            switch(message.State)
            {
                case PlayState.Playing:
                    AlbumCover = AudioPlayer.Instance.GetAlbumCover();
                    Title = AudioPlayer.Instance.Title;
                    Artists = AudioPlayer.Instance.Artists;
                    Album = AudioPlayer.Instance.Album;
                    break;
                case PlayState.Stopped:
                    AlbumCover = null;
                    Title = string.Empty;
                    Artists = string.Empty;
                    Album = string.Empty;
                    break;
            }

        }

    }
}
