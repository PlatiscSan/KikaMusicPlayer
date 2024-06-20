using KiKaMusicPlayer.Model;
using NAudio.Wave;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using KiKaMusicPlayer.Utilities;

namespace KiKaMusicPlayer.Module
{
    class AudioPlayer
    {
        private AudioPlayer()
        {
            m_song_items.CollectionChanged += (s, e) =>
            {
                switch(m_queue_changed)
                {
                    case PlayQueueChanged.Add:

                        m_queue_changed = PlayQueueChanged.Default;
                        break;

                    case PlayQueueChanged.Remove:
                        if(m_index == e.OldStartingIndex)
                        {
                            Stop();
                        }
                        m_queue_changed = PlayQueueChanged.Default;
                        break;

                    default:
                        if (m_index == e.OldStartingIndex)
                        {
                            m_index = e.NewStartingIndex;
                        }
                        break;

                }
            };
        }
        static readonly AudioPlayer m_instance = new();
        public static AudioPlayer Instance
        {
            get { return m_instance; }
        }

        // play queue

        private ObservableCollection<SongModel> m_song_items = new();
        public ObservableCollection<SongModel> SongItems
        {
            get { return m_song_items; }
            set { m_song_items = value; }
        }

        private int m_index = 0;
        public int Index
        {
            get { return m_index; }
        }

        // Audio Info

        public int AddFile(string path)
        {

            SongModel song = null;
            try
            {
                song = new SongModel(path);
            }
            catch
            {
                return -1;
            }

            foreach (SongModel item in m_song_items)
            {
                if(item.FilePath == song.FilePath)
                {
                    m_queue_changed = PlayQueueChanged.Remove;
                    m_song_items.RemoveAt(m_song_items.IndexOf(item));
                    break;
                }
            }
            m_queue_changed = PlayQueueChanged.Add;
            m_song_items.Add(song);

            return m_song_items.IndexOf(song);
        }

        public void Insert(int index, string path)
        {

            SongModel song = null;
            try
            {
                song = new SongModel(path);
            }
            catch
            {
                return;
            }

            foreach (SongModel item in m_song_items)
            {
                if (item.FilePath == song.FilePath)
                {
                    m_queue_changed = PlayQueueChanged.Remove;
                    m_song_items.RemoveAt(m_song_items.IndexOf(item));
                    break;
                }
            }
            m_queue_changed = PlayQueueChanged.Add;

            try
            {
                m_song_items.Insert(index, song);
            }
            catch
            {
                throw;
            }


        }

        private enum PlayQueueChanged
        {
            Default,
            Add,
            Remove,
        }

        private PlayQueueChanged m_queue_changed;

        public void RemoveAt(int index)
        {
            m_queue_changed = PlayQueueChanged.Remove;
            m_song_items.RemoveAt(index);
            Play();
        }

        public void MoveUp(int index)
        {
            if(m_index == index)
            {
                m_index--;
            }

            SongItems.Move(index, m_index);

        }

        public void MoveDown(int index)
        {
            if(m_index == index)
            {
                m_index++;
            }
            SongItems.Move(index, m_index);
        }

        public void ClearQueue()
        {
            m_song_items.Clear();
        }

        public int ItemCount { get { return m_song_items.Count; } }

        // player

        public enum PlayMode
        {
            RepeatOff = 0,
            RepeatAll = 1,
            RepeatOne = 2,
        } 

        public enum PlayerState
        {
            Stopped,
            Playing,
            Paused,
        }

        private PlayerState m_state = PlayerState.Stopped;
        public PlayerState State
        {
            get { return m_state; }
        }

        private PlayMode m_mode = PlayMode.RepeatOff;
        public PlayMode Mode
        {
            get
            {
                return m_mode;
            }
            set
            {
                if (m_mode != value)
                {
                    m_mode = value;
                }
            }
        }

        private WaveOutEvent m_output_device;
        private AudioFileReader m_reader;

        public bool IsWorking
        {
            get { return m_output_device != null; }
        }

        private float m_volume = 1.0f;

        public void PlayLastOne()
        {
            m_index = m_song_items.Count - 1;
            Play();
        }

        public void PlayFirst()
        {
            m_index = 0;
            Play();
        }

        public void Play(int index)
        {
            m_index = index;
            Play();
        }

        public void Play()
        {
            if(m_song_items.Count == 0)
            {
                return;
            }
            if (m_output_device != null)
            {
                m_output_device.Dispose();
                MessageBus.Instance.Publish(new PlayStateChangedMessage(PlayState.Stopped));
            }
            m_reader = new AudioFileReader(m_song_items[m_index].FilePath);
            m_output_device = new WaveOutEvent();
            m_output_device.Init(m_reader);
            m_output_device.Volume = m_volume;
            m_output_device.PlaybackStopped += PlaybackStopped;
            m_output_device.Play();
            m_state = PlayerState.Playing;

            MessageBus.Instance.Publish(new PlayStateChangedMessage(PlayState.Playing));

        }

        public bool PlayNext()
        {
            if (m_song_items.Count == 0)
            {
                return false;
            }

            if (m_index + 1 >= m_song_items.Count)
            { 
                if (m_mode == PlayMode.RepeatAll)
                {
                    m_index = 0;
                }  
                else
                {
                    return false; 
                }
            }
            else
            {
                m_index++;
            }

            if (m_output_device != null) 
            {
                m_output_device.Dispose();
                MessageBus.Instance.Publish(new PlayStateChangedMessage(PlayState.Stopped));
            }
            m_reader = new AudioFileReader(m_song_items[m_index].FilePath);
            m_output_device = new WaveOutEvent();
            m_output_device.Init(m_reader);
            m_output_device.Volume = m_volume;
            m_output_device.PlaybackStopped += PlaybackStopped;
            m_output_device.Play();
            m_state = PlayerState.Playing;

            MessageBus.Instance.Publish(new PlayStateChangedMessage(PlayState.Playing));

            return true;
        }

        public bool PlayPrevious()
        {
            if (m_song_items.Count == 0)
            {
                return false;
            }

            if (m_index - 1 < 0)
            {
                if(m_mode == PlayMode.RepeatAll)
                {
                    m_index = m_song_items.Count - 1;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                m_index--;
            }

            if (m_output_device != null)
            {
                m_output_device.Dispose();
                MessageBus.Instance.Publish(new PlayStateChangedMessage(PlayState.Stopped));
            }
            m_reader = new AudioFileReader(m_song_items[m_index].FilePath);
            m_output_device = new WaveOutEvent();
            m_output_device.Init(m_reader);
            m_output_device.Volume = m_volume;
            m_output_device.PlaybackStopped += PlaybackStopped;
            m_output_device.Play();
            m_state = PlayerState.Playing;

            MessageBus.Instance.Publish(new PlayStateChangedMessage(PlayState.Playing));

            return true;
        }

        public void Pause()
        {
            if(m_output_device == null)
            {
                return;
            }
            m_output_device.Pause();
            m_state = PlayerState.Paused;
            MessageBus.Instance.Publish(new PlayStateChangedMessage(PlayState.Paused));
        }

        public void Resume()
        {
            if(m_output_device == null)
            {
                return;
            }
            m_output_device.Play();
            m_state = PlayerState.Playing;
            MessageBus.Instance.Publish(new PlayStateChangedMessage(PlayState.Resume));
        }

        public void Stop()
        {
            if (m_output_device == null)
            {
                return;
            }
            m_output_device.Dispose();
            m_state = PlayerState.Stopped;
            MessageBus.Instance.Publish(new PlayStateChangedMessage(PlayState.Stopped));
        }

        public void SetVolume(float volume)
        {
            m_volume = volume;
            if(m_output_device == null)
            {
                return;
            }
            m_output_device.Volume = m_volume;
        }

        public void SetPosition(double time_in_second)
        {
            if(m_output_device == null) 
            {
                return;
            }
            m_output_device.Pause();
            m_reader.CurrentTime = TimeSpan.FromSeconds(time_in_second);
            m_output_device.Play();
        }

        public TimeSpan CurrentPostionTimeSpan
        {
            get { return m_reader.CurrentTime; }
        }

        private void RepeatOff()
        {
            if (m_index + 1 >= m_song_items.Count)
            {
                return;
            }
            m_index++;
            Play();
        }

        private void RepeatAll()
        {
            if (m_index + 1 >= m_song_items.Count)
            {
                m_index = 0;
            }
            else
            {
                m_index++;
            }
            Play();
        }

        private void PlaybackStopped(object? sender, StoppedEventArgs e)
        {

            if (m_output_device.PlaybackState != PlaybackState.Stopped)
            {
                return;
            }

            m_output_device.Dispose();
            m_state = PlayerState.Stopped;

            MessageBus.Instance.Publish(new PlayStateChangedMessage(PlayState.Stopped));

            switch (m_mode)
            {
                case PlayMode.RepeatOff:
                    RepeatOff();
                    return;
                case PlayMode.RepeatAll:
                    RepeatAll();
                    return;
                case PlayMode.RepeatOne:
                    Play();
                    return;
            }

        }

        public string Title
        {
            get => m_song_items[m_index].Title;
        }

        public string Artists
        {
            get=> m_song_items[m_index].Artists;
        }

        public string Album
        {
            get => m_song_items[m_index].Album;
        }

        public TimeSpan Duration
        {
            get => m_song_items[m_index].Duration;
        } 

        public BitmapImage GetAlbumCover()
        {
            return m_song_items[m_index].AlbumCover;
        }

    }
}
