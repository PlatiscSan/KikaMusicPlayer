using KiKaMusicPlayer.Model;
using KiKaMusicPlayer.Utilities;
using KiKaMusicPlayer.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static KiKaMusicPlayer.Utilities.ViewChangedMessage;

namespace KiKaMusicPlayer.ViewModel
{
    class NavigationViewModel : BaseViewModel
    {

        private object m_current_view;
        public object CurrentView
        {
            get { return m_current_view; }
            private set { m_current_view = value; OnPropertyChanged(nameof(CurrentView)); }
        }

        private HomePage m_homepage = new HomePage();

        private PlayQueue m_play_queue = new PlayQueue();

        private LocalMusic m_local_music = new LocalMusic();

        private OnlineMusic m_online_music = new OnlineMusic();

        private Settings m_settings = new Settings();

        private PlayPage m_play_page = new PlayPage();

        private Playlist m_playlist = new Playlist();

        private void ViewChanged(ViewChangedMessage message)
        {
            switch (message.View)
            {
                case PageView.HomePage: 
                    CurrentView = m_homepage; 
                    break;
                case PageView.PlayQueuePage:
                    CurrentView = m_play_queue;
                    break;
                case PageView.LocalMusicPage:
                    CurrentView = m_local_music;
                    break;
                case PageView.OnlineMusicPage:
                    CurrentView = m_online_music;
                    break;
                case PageView.PlaylistPage:
                    CurrentView = m_playlist;
                    break;
                case PageView.SettingPage:
                    CurrentView = m_settings;
                    break;
                case PageView.PlayPage:
                    CurrentView = m_play_page;
                    break;
            }

        }

        private object m_dialog_content;
        public object DialogContent
        {
            get => m_dialog_content;
            set
            {
                m_dialog_content = value;
                OnPropertyChanged(nameof(DialogContent));
            }
        }

        private bool m_dialog_is_open = false;
        public bool DialogIsOpen
        {
            get => m_dialog_is_open;
            set
            {
                m_dialog_is_open = value;
                OnPropertyChanged(nameof(DialogIsOpen));
            }
        }

        public NavigationViewModel() 
        {

            MessageBus.Instance.Subscribe<ViewChangedMessage>(ViewChanged);
            MessageBus.Instance.Subscribe<DialogMessage>(Dialog);


            // start page 
            CurrentView = m_homepage;

        }

        private void Dialog(DialogMessage message)
        {
            switch (message.DialogMessageType)
            {
                case DialogMessageType.Show:
                    DialogContent = message.DialogContent;
                    DialogIsOpen = true;
                    break;
                case DialogMessageType.Close:
                    DialogIsOpen = false;
                    break;
            }
        }


    }
}
