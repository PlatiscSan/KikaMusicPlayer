
using KiKaMusicPlayer.Model;
using KiKaMusicPlayer.Module;
using KiKaMusicPlayer.Utilities;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace KiKaMusicPlayer.ViewModel
{
    class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel()
        {
            ItemClickCommand = new RelayCommand(ItemClick);
            ItemMenuCommand = new RelayCommand(ItemMenu);
            ItemMenuSelectionChangedCommand = new RelayCommand(ItemMenuSelectionChanged);

            MessageBus.Instance.Subscribe<OpenFilesMessage>(OpenFile);
        }

        private Visibility m_title_text_visbility = Visibility.Visible;
        public Visibility TitleTextVisbility
        {
            get { return m_title_text_visbility; }
            private set
            {
                m_title_text_visbility = value; 
                OnPropertyChanged(nameof(TitleTextVisbility));
            }
        }

        private Visibility m_muti_select_stackpanel_visbility = Visibility.Collapsed;
        public Visibility MutiSelectStackPanelVisbility
        {
            get { return m_muti_select_stackpanel_visbility; }
            private set
            {
                m_muti_select_stackpanel_visbility = value;
                OnPropertyChanged(nameof(MutiSelectStackPanelVisbility));
            }
        }

        private bool m_popup_is_open = false;
        public bool PopupIsOpen
        {
            get => m_popup_is_open;
            set
            {
                m_popup_is_open = value;
                OnPropertyChanged(nameof(PopupIsOpen));
            }
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

        private ObservableCollection<SongModel> m_song_items = [];
        public ObservableCollection<SongModel> SongItems
        {
            get { return m_song_items; }
        }

        private void OpenFile(OpenFilesMessage message)
        {
            
            foreach(string path in message.Paths)
            {
                bool found = false;
                foreach(SongModel item in m_song_items) 
                {
                    if(item.FilePath == path)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    continue;
                }

                SongModel song = null;
                try
                {
                    song = new SongModel(path);
                }
                catch
                {
                    continue;
                }
                m_song_items.Add(song);

            }
        }

        private SongModel m_selected_item;

        public ICommand ItemClickCommand { get; private set; }
        public void ItemClick(object parameter)
        {
            m_selected_item = (SongModel)parameter;
            AudioPlayer.Instance.AddFile(m_selected_item.FilePath);
            AudioPlayer.Instance.PlayLastOne();
        }

        public ICommand ItemMenuCommand { get; private set; }
        private void ItemMenu(object parameter)
        {
            PopupIsOpen = !PopupIsOpen;
            m_selected_item = (SongModel)parameter;
        }

        public ICommand ItemMenuSelectionChangedCommand {  get; private set; }
        private void ItemMenuSelectionChanged(object parameter)
        {
            switch (ItemMenuSelectedIndex)
            {

                case 0:
                    AudioPlayer.Instance.AddFile(m_selected_item.FilePath);
                    AudioPlayer.Instance.PlayLastOne();
                    break;

                case 1:
                    AudioPlayer.Instance.Insert(AudioPlayer.Instance.Index + 1, m_selected_item.FilePath);
                    break;

                case 2:
                    AudioPlayer.Instance.AddFile(m_selected_item.FilePath);
                    break;

                case 3:
                    // Add to Playlist
                    break;

                case 4:
                    m_song_items.Remove(m_selected_item);
                    break;

                case 5:
                    // Select
                    break;

                case 6:
                    // Properties
                    break;

                default:
                    return;

            }

            ItemMenuSelectedIndex = -1;
            PopupIsOpen = !PopupIsOpen;

        }

        private int m_item_menu_selected_index = -1;
        public int ItemMenuSelectedIndex 
        {
            get => m_item_menu_selected_index;
            set
            {
                m_item_menu_selected_index = value;
                OnPropertyChanged(nameof(ItemMenuSelectedIndex));
            }
        }

    }
}
