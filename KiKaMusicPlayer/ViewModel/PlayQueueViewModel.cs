using KiKaMusicPlayer.Model;
using KiKaMusicPlayer.Utilities;
using KiKaMusicPlayer.Module;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace KiKaMusicPlayer.ViewModel
{
    class PlayQueueViewModel : BaseViewModel
    {
        public ObservableCollection<SongModel> SongItems
        {
            get => AudioPlayer.Instance.SongItems;
            set
            {
                AudioPlayer.Instance.SongItems = value;
                OnPropertyChanged(nameof(SongItems));
            }
        }

        private int m_selected_index;
        public int SelectedIndex
        {
            get { return m_selected_index; }
            set 
            { 
                m_selected_index = value; 
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        private int m_playing_index;
        public int PlayingIndex
        {
            get { return m_playing_index; }
            set 
            { 
                m_playing_index = value; 
                OnPropertyChanged(nameof(PlayingIndex));
            }
        }

        private bool m_has_selected_items;
        public bool HasSelectedItems
        {
            get { return m_has_selected_items; }
            set
            {
                m_has_selected_items = value;
                OnPropertyChanged(nameof(HasSelectedItems));
            }
        }

        private Visibility m_menu_visibility;
        public Visibility MenuVisibility
        {
            get { return m_menu_visibility; }
            set
            {
                m_menu_visibility = value;
                OnPropertyChanged(nameof(MenuVisibility));
            }
        }

        public PlayQueueViewModel()
        {
            AddCommand = new RelayCommand(Add);
            ClearCommand = new RelayCommand(Clear);
            PlayCommand = new RelayCommand(Play);
            RemoveCommand = new RelayCommand(Remove);
            MoveUpCommand = new RelayCommand(MoveUp);
            MoveDownCommand = new RelayCommand(MoveDown);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            PropertiesCommand = new RelayCommand(Properties);
            SelectCommand = new RelayCommand(Select);

            HasSelectedItems = false;
            MenuVisibility = Visibility.Collapsed;


            MessageBus.Instance.Subscribe<OpenFilesMessage>(OpenFile);
            MessageBus.Instance.Subscribe<PlayStateChangedMessage>(PlayStateChanged);
        }

        private void PlayStateChanged(PlayStateChangedMessage message)
        {
            switch (message.State) 
            {
                case PlayState.Playing:

                    break;
                case PlayState.Stopped:
                    if(SongItems.Count == 0)
                    {
                        return;
                    }

                    break;
            }
        }

        public void HandleDrop(System.Windows.DragEventArgs e)
        {
            
        }

        private void OpenFile(OpenFilesMessage message)
        {

            int play_begin = -1;

            foreach (string file_path in message.Paths)
            {
                int added_index = AudioPlayer.Instance.AddFile(file_path);
                if(play_begin == -1)
                {
                    play_begin = added_index;
                }
            }
            if (play_begin != -1)
            {
                AudioPlayer.Instance.Play(play_begin);

            }
        }

        public ICommand SelectionChangedCommand { get; private set; }
        private void SelectionChanged(object parameter)
        {
            var selected_items = parameter as IList;
            HasSelectedItems = selected_items != null && selected_items.Count > 0;
            MenuVisibility = selected_items != null && selected_items.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public ICommand ClearCommand { get; private set; }
        private void Clear(object parameter)
        {
            AudioPlayer.Instance.Stop();
            SongItems.Clear();
            AudioPlayer.Instance.ClearQueue();
        }

        public ICommand AddCommand { get; private set; }
        private void Add(object parameter)
        {

            
        }

        public ICommand PlayCommand { get; private set; }
        private void Play(object parameter)
        {
            AudioPlayer.Instance.Play(SelectedIndex);
        }

        public ICommand RemoveCommand { get; private set; }
        private void Remove(object parameter)
        {
            AudioPlayer.Instance.RemoveAt(SelectedIndex);
        }

        public ICommand MoveUpCommand { get; private set; }
        private void MoveUp(object parameter)
        {
            AudioPlayer.Instance.MoveUp(PlayingIndex);
        }
        public ICommand MoveDownCommand { get; private set;}
        private void MoveDown(object parameter)
        {
            AudioPlayer.Instance.MoveDown(SelectedIndex);
        }

        public ICommand PropertiesCommand { get; private set; }
        private void Properties(object parameter)
        {

        }

        public ICommand SelectCommand { get; private set; }
        private void Select(object parameter)
        {

        }

    }
    
}
