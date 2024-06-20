
using KiKaMusicPlayer.Utilities;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using static KiKaMusicPlayer.Utilities.PageView;

namespace KiKaMusicPlayer.ViewModel
{
    class TitleBarViewModel : BaseViewModel
    {

        private string m_title = string.Empty;
        public string Title
        {
            get { return m_title; }
            set 
            { 
                m_title = value; 
                OnPropertyChanged(nameof(Title));
            }
        }

        private int m_open_mode = -1;
        public int OpenMode
        {
            get { return m_open_mode; }
            set 
            { 
                m_open_mode = value; 
                OnPropertyChanged(nameof(OpenMode));
            }
        }

        private Visibility m_openfile_button_visibility;
        public Visibility OpenFileButtonVisbility
        {
            get { return m_openfile_button_visibility; }
            set 
            { 
                m_openfile_button_visibility = value; 
                OnPropertyChanged(nameof(OpenFileButtonVisbility)); 
            }
        }


        public TitleBarViewModel()
        {

            SplitButtonCommand = new RelayCommand(SplitButton);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            MessageBus.Instance.Subscribe<ViewChangedMessage>(ViewChanged);

            // start item
            ChangedToHome();
        }

        private void ChangedToPlayQueue()
        {
            Title = "Play Queue";
            OpenFileButtonVisbility = Visibility.Visible;
        }

        private void ChangedToHome()
        {
            Title = "Home";
            OpenFileButtonVisbility = Visibility.Visible;
        }

        private void ChangedToPlayPage()
        {
            Title = "Playing";
            OpenFileButtonVisbility = Visibility.Collapsed;
        }

        private void ChangedToLocalMusicPage()
        {
            Title = "Local Music";
            OpenFileButtonVisbility = Visibility.Collapsed;
        }

        private void ChangedToOnlineMusicPage()
        {
            Title = "Online Music";
            OpenFileButtonVisbility = Visibility.Collapsed;
        }

        private void ChangedToSettingPage()
        {
            Title = "Settings";
            OpenFileButtonVisbility = Visibility.Collapsed;
        }

        private void ChangedToPlaylistPage()
        {
            Title = "Playlist";
            OpenFileButtonVisbility = Visibility.Collapsed;
        }

        private void ViewChanged(ViewChangedMessage message)
        {
            switch (message.View)
            {
                case HomePage:
                    ChangedToHome();
                    break;
                case PlayQueuePage:
                    ChangedToPlayQueue();
                    break;
                case PlayPage:
                    ChangedToPlayPage();
                    break;
                case LocalMusicPage:
                    ChangedToLocalMusicPage();
                    break;
                case OnlineMusicPage:
                    ChangedToOnlineMusicPage();
                    break;
                case SettingPage:
                    ChangedToSettingPage();
                    break;
                case PlaylistPage:
                    ChangedToPlaylistPage();
                    break;
            }
        }

        private void OpenFile()
        {
            System.Windows.Forms.OpenFileDialog open_file_dialog = new();
            open_file_dialog.Multiselect = true;
            if (open_file_dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            MessageBus.Instance.Publish(new OpenFilesMessage(open_file_dialog.FileNames));
        }

        private void OpenFolder()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if(dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string folder_path = dialog.SelectedPath;
            DirectoryInfo directory_info = new DirectoryInfo(folder_path);
            FileInfo[] files = directory_info.GetFiles();
            List<string> paths = new List<string>();
            foreach(FileInfo file in files)
            {
                paths.Add(file.FullName);
            }
            MessageBus.Instance.Publish(new OpenFilesMessage(paths));
        }

        private void OpenURL()
        {
            System.Windows.Forms.MessageBox.Show("Open URL");
        }

        public ICommand SplitButtonCommand { get; set; }
        private void SplitButton(object parameter)
        {
            OpenFile();
        }

        public ICommand SelectionChangedCommand {  get; set; }
        private void SelectionChanged(object parameter)
        {
            switch (OpenMode)
            {
                case 0:
                    OpenFile();
                    break;
                case 1:
                    OpenFolder();
                    break;
                case 2:
                    OpenURL();
                    break;
                default:
                    break;

            }
            OpenMode = -1;
        }

    }
}
