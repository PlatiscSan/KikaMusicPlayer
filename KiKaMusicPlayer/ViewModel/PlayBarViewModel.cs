
using KiKaMusicPlayer.Utilities;
using KiKaMusicPlayer.Module;
using static KiKaMusicPlayer.Utilities.PlayStateChangedMessage;

using MaterialDesignThemes.Wpf;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace KiKaMusicPlayer.ViewModel
{
    class PlayBarViewModel : BaseViewModel
    {

        private PackIconKind m_play_icon;
        public PackIconKind PlayIcon
        {
            get { return m_play_icon; }
            private set { m_play_icon = value; OnPropertyChanged(nameof(PlayIcon)); }
        }

        private PackIconKind m_play_mode_icon;
        public PackIconKind PlayModeIcon
        {
            get { return m_play_mode_icon; }
            private set { m_play_mode_icon = value; OnPropertyChanged(nameof(PlayModeIcon));  }
        }

        private string m_current_time;
        public string CurrentTime
        {
            get { return m_current_time; }
            private set { m_current_time = value; OnPropertyChanged(nameof(CurrentTime)); }
        }

        private string m_duration;
        public string Duration
        {
            get { return m_duration; }
            private set { m_duration = value; OnPropertyChanged(nameof(Duration)); }
        }
        private BitmapSource m_album_cover;
        public BitmapSource AlbumCover
        {
            get { return m_album_cover; }
            private set { m_album_cover = value; OnPropertyChanged(nameof(AlbumCover)); }
        }

        private double m_player_slider_max;
        public double PlayerSliderMax
        {
            get { return m_player_slider_max; }
            private set { m_player_slider_max = value; OnPropertyChanged(nameof(PlayerSliderMax)); }
        }

        private double m_player_slider_value;
        public double PlayerSliderValue
        {
            get { return m_player_slider_value; }
            set { m_player_slider_value = value; OnPropertyChanged(nameof(PlayerSliderValue)); }
        }

        private string m_artists;
        public string Artists
        {
            get { return m_artists; }
            private set { m_artists = value; OnPropertyChanged(nameof(Artists)); }
        }

        private string m_title;
        public string Title
        {
            get { return m_title; }
            private set { m_title = value; OnPropertyChanged(nameof(Title)); }
        }

        private bool m_volume_popup_is_open;
        public bool VolumePopUpIsOpen
        {
            get { return m_volume_popup_is_open; }
            private set { m_volume_popup_is_open = value; OnPropertyChanged(nameof(VolumePopUpIsOpen)); }
        }

        private double m_volume_slider_value;
        public double VolumeSliderValue
        {
            get { return m_volume_slider_value; }
            set { m_volume_slider_value = value; OnPropertyChanged(nameof(VolumeSliderValue)); }
        }

        public PlayBarViewModel()
        {
            PlayCommand = new RelayCommand(Play);
            PlayPreviousCommand = new RelayCommand(PlayPrevious);
            PlayNextCommand = new RelayCommand(PlayNext);
            PlayModeCommand = new RelayCommand(PlayMode);
            PlayerSliderValueChangedCommand = new RelayCommand(PlayerSliderValueChanged);
            VolumeCommand = new RelayCommand(Volume);
            VolumeSliderValueChangedCommand = new RelayCommand(VolumeSliderValueChanged);
            PlayPageCommand = new RelayCommand(PlayPage);

            PlayIcon = PackIconKind.Play;
            PlayModeIcon = PackIconKind.RepeatOff;
            CurrentTime = "00:00:00";
            Duration = "00:00:00";
            Artists = string.Empty;
            Title = string.Empty;
            VolumeSliderValue = 1.0f;

            MessageBus.Instance.Subscribe<PlayStateChangedMessage>(PlayStateChanged);
            InitTimer();


        }

        private System.Windows.Forms.Timer m_timer;

        bool m_is_internal_update = false;
        private void OnTimedEvent(object sender, EventArgs e) 
        {
            m_is_internal_update = true;
            PlayerSliderValue = AudioPlayer.Instance.CurrentPostionTimeSpan.TotalSeconds;
            CurrentTime = AudioPlayer.Instance.CurrentPostionTimeSpan.ToString().Remove(8);
            m_is_internal_update = false;
        }


        void InitTimer()
        {
            m_timer = new System.Windows.Forms.Timer();
            m_timer.Interval = 100;
            m_timer.Tick += new EventHandler(OnTimedEvent);
        }

        private void LoadInfo()
        {
            m_is_internal_update = true;

            AlbumCover = AudioPlayer.Instance.GetAlbumCover();
            Title = AudioPlayer.Instance.Title;
            Artists = AudioPlayer.Instance.Artists;

            PlayIcon = PackIconKind.Pause;
            PlayerSliderMax = AudioPlayer.Instance.Duration.TotalSeconds;
            Duration = AudioPlayer.Instance.Duration.ToString().Remove(8);

            m_is_internal_update = false;

        }

        private void Reset()
        {
            m_is_internal_update = true;

            AlbumCover = null;
            Title = string.Empty;
            Artists = string.Empty;
            PlayIcon = PackIconKind.Play;
            CurrentTime = "00:00:00";
            Duration = "00:00:00";
            PlayerSliderValue = 0;


            m_is_internal_update = false;
        }

        private void PlayStateChanged(PlayStateChangedMessage message)
        {
            switch(message.State)
            {
                case PlayState.Playing:
                    LoadInfo();
                    m_timer.Start();
                    break;
                case PlayState.Stopped:
                    Reset();
                    m_timer.Stop();
                    break;
            }
        }

        public ICommand PlayPageCommand { get; private set; }
        private void PlayPage(object parameter)
        {
            MessageBus.Instance.Publish(new ViewChangedMessage(PageView.PlayPage));
        }

        public ICommand PlayCommand { get; private set; }
        private void Play(object parameter)
        {
            if (!AudioPlayer.Instance.IsWorking)
            {
                AudioPlayer.Instance.Play();
                return;
            }

            switch (AudioPlayer.Instance.State)
            {
                case AudioPlayer.PlayerState.Playing:
                    AudioPlayer.Instance.Pause();
                    PlayIcon = PackIconKind.Play;
                    break;
                case AudioPlayer.PlayerState.Paused:
                    AudioPlayer.Instance.Resume();
                    PlayIcon = PackIconKind.Pause;
                    break;
                case AudioPlayer.PlayerState.Stopped:
                    AudioPlayer.Instance.PlayFirst();
                    break;
            }

        }

        public ICommand PlayPreviousCommand { get; private set; }
        private void PlayPrevious(object parameter)
        {
            if (AudioPlayer.Instance.PlayPrevious())
            {
                LoadInfo();
            }
        }

        public ICommand PlayNextCommand { get; private set; }
        private void PlayNext(object parameter)
        {
            if (AudioPlayer.Instance.PlayNext())
            {
                LoadInfo();
            }
            
        }

        public ICommand PlayModeCommand { get; private set; }
        private void PlayMode(object parameter)
        {
            switch(AudioPlayer.Instance.Mode)
            {
                case AudioPlayer.PlayMode.RepeatOff:
                    AudioPlayer.Instance.Mode = AudioPlayer.PlayMode.RepeatAll;
                    PlayModeIcon = PackIconKind.Repeat;
                    break;
                case AudioPlayer.PlayMode.RepeatAll:
                    AudioPlayer.Instance.Mode = AudioPlayer.PlayMode.RepeatOne;
                    PlayModeIcon = PackIconKind.RepeatOne;
                    break;
                case AudioPlayer.PlayMode.RepeatOne:
                    AudioPlayer.Instance.Mode = AudioPlayer.PlayMode.RepeatOff;
                    PlayModeIcon = PackIconKind.RepeatOff;
                    break;
            }

        }

        public ICommand PlayerSliderValueChangedCommand {  get; private set; }

        private void PlayerSliderValueChanged(object parameter)
        {
            if (m_is_internal_update)
            {
                return;
            }
            if(AudioPlayer.Instance.State == AudioPlayer.PlayerState.Stopped)
            {
                return;
            }
            double time = PlayerSliderValue == PlayerSliderMax ? PlayerSliderMax - 2 : PlayerSliderValue;
            AudioPlayer.Instance.SetPosition(time);
        }

        public ICommand VolumeCommand { get; private set; }
        private void Volume(object parameter)
        {
            VolumePopUpIsOpen = true;
        }

        public ICommand VolumeSliderValueChangedCommand { get; private set; }
        private void VolumeSliderValueChanged(object parameter)
        {
            AudioPlayer.Instance.SetVolume((float)VolumeSliderValue);
        }
    }
}
