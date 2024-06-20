
using System.IO;
using System.Windows.Media.Imaging;
using static KiKaMusicPlayer.Utilities.PlayStateChangedMessage;

namespace KiKaMusicPlayer.Utilities
{
    class AudioInfoFetcher
    {

        private TagLib.File m_file;

        public void OpenFile(string path)
        {
            if (m_file != null)
            {
                m_file.Dispose();
            }

            try
            {
                m_file = TagLib.File.Create(path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CloseFile()
        {
            if(m_file == null)
            {
                return;
            }
            m_file.Dispose();
            m_file = null;
        }

        public string Title
        {
            get { return m_file == null ? string.Empty : m_file.Tag.Title; }
        }

        public string Album
        {
            get { return m_file == null ? string.Empty : m_file.Tag.Album; }
        }

        public string[]? AlbumArtisis
        {
            get { return m_file.Tag.AlbumArtists; }
        }

        public string[]? Artists
        {
            get { return m_file.Tag.Artists; }
        }

        public uint Track
        {
            get { return m_file.Tag.Track; }
        }

        public int BitRate
        {
            get { return m_file.Properties.AudioBitrate; }
        }

        public BitmapImage GetAlbumCover()
        {
            BitmapImage bitmap = new BitmapImage();
            if (m_file == null && m_file.Tag.Pictures.Length <= 0)
            {
                return bitmap;
            }

            byte[] cover_data = m_file.Tag.Pictures[0].Data.Data;
            using (MemoryStream ms = new MemoryStream(cover_data))
            {
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }
            return bitmap;

        }
        public TimeSpan Duration
        {
            get { return m_file == null ? TimeSpan.Zero : m_file.Properties.Duration; }
        }

    }
}
