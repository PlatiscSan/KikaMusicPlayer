using KiKaMusicPlayer.Utilities;
using Meting4Net.Core.Models.Tencent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace KiKaMusicPlayer.Model
{
    class SongModel
    {

        private string m_title = string.Empty;
        private string m_artists = string.Empty;
        private string m_album = string.Empty;
        private string m_file_path = string.Empty;
        private TimeSpan m_duration = TimeSpan.Zero;
        private BitmapImage m_album_cover;
        public string Title { get { return m_title; } }
        public string Artists { get {  return m_artists; } }
        public string Album { get { return m_album; } }
        public TimeSpan Duration { get { return m_duration; } }

        public string DurationString
        {
            get { return m_duration.ToString().Remove(8); }
        }

        public BitmapImage AlbumCover { get { return m_album_cover; } }

        public string FilePath { get { return m_file_path; } }

        public SongModel(string file_path, string title, string artists, string album, TimeSpan duration, BitmapImage album_cover) 
        {
            m_album = album;
            m_title = title;
            m_artists = artists;
            m_duration = duration;
            m_file_path = file_path;
            m_album_cover = album_cover;
        }

        public SongModel(string file_path)
        {

            AudioInfoFetcher fetcher = new AudioInfoFetcher();

            try
            {
                fetcher.OpenFile(file_path);
            }
            catch 
            {
                throw;
            }

            string artists = string.Empty;
            if (fetcher.Artists != null)
            {
                foreach (string artist in fetcher.Artists)
                {
                    artists += artist;
                    artists += "/";
                }
                artists = artists.Remove(artists.Length - 1, 1);
            }

            m_file_path = file_path;
            m_album = fetcher.Album;
            m_title = fetcher.Title;
            m_artists = artists;
            m_duration = fetcher.Duration;
            m_file_path = file_path;
            m_album_cover = fetcher.GetAlbumCover();

            fetcher.CloseFile();


        }

    }
}
