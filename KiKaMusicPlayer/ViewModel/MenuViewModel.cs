using KiKaMusicPlayer.Model;
using KiKaMusicPlayer.Utilities;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using static KiKaMusicPlayer.Utilities.PageView;

namespace KiKaMusicPlayer.ViewModel
{
    class MenuViewModel : BaseViewModel
    {

        private Visibility m_close_menu_button_visibility;
        public Visibility CloseMenuButtonVisibility
        {
            get => m_close_menu_button_visibility;
            set
            {
                m_close_menu_button_visibility = value;
                OnPropertyChanged(nameof(CloseMenuButtonVisibility));
            }
        }

        private Visibility m_open_menu_button_visibility;
        public Visibility OpenMenuButtonVisibility
        {
            get => m_open_menu_button_visibility;
            set
            {
                m_open_menu_button_visibility = value;
                OnPropertyChanged(nameof(OpenMenuButtonVisibility));
            }
        }

        private View.Menu m_view;
        public MenuViewModel(View.Menu view)
        {
            m_view = view;
            OpenMenuCommand = new RelayCommand(OpenMenu);
            CloseMenuCommand = new RelayCommand(CloseMenu);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            CloseMenuButtonVisibility = Visibility.Collapsed;

            InitAnimation();

            MessageBus.Instance.Subscribe<ViewChangedMessage>(ViewChanged);
        }

        private void ViewChanged(ViewChangedMessage message)
        {
            if(message.View == PlayPage)
            {
                SelectedIndex = -1;
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

        private Storyboard m_open_menu_storyboard = new();
        private Storyboard m_close_menu_storyboard = new();
        private void InitAnimation()
        {

            DoubleAnimationUsingKeyFrames open_animation = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetName(open_animation, m_view.GridMenu.Name);
            Storyboard.SetTargetProperty(open_animation, new PropertyPath("(FrameworkElement.Width)"));
            open_animation.KeyFrames.Add(new EasingDoubleKeyFrame(70, KeyTime.FromTimeSpan(TimeSpan.Zero)));
            open_animation.KeyFrames.Add(new EasingDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            m_open_menu_storyboard.Children.Add(open_animation);

            DoubleAnimationUsingKeyFrames close_animation = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetName(close_animation, m_view.GridMenu.Name);
            Storyboard.SetTargetProperty(close_animation, new PropertyPath("(FrameworkElement.Width)"));
            close_animation.KeyFrames.Add(new EasingDoubleKeyFrame(200, KeyTime.FromTimeSpan(TimeSpan.Zero)));
            close_animation.KeyFrames.Add(new EasingDoubleKeyFrame(70, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            m_close_menu_storyboard.Children.Add(close_animation);
        }

        public ICommand OpenMenuCommand { get; set; }
        private void OpenMenu(object paramter)
        {
            CloseMenuButtonVisibility = Visibility.Visible;
            OpenMenuButtonVisibility = Visibility.Collapsed;
            m_open_menu_storyboard.Begin(m_view.GridMenu);
            
        }

        public ICommand CloseMenuCommand { get; set;}
        private void CloseMenu(object paramter)
        {
            CloseMenuButtonVisibility = Visibility.Collapsed;
            OpenMenuButtonVisibility = Visibility.Visible;
            m_close_menu_storyboard.Begin(m_view.GridMenu);
        }

        public ICommand SelectionChangedCommand {  get; set; }
        private void SelectionChanged(object paramter)
        {
            if (paramter is int item_index)
            {
                switch (item_index)
                {
                    case 0:
                        MessageBus.Instance.Publish(new ViewChangedMessage(HomePage));
                        break;
                    case 1:
                        MessageBus.Instance.Publish(new ViewChangedMessage(LocalMusicPage)); 
                        break;
                    case 2:
                        MessageBus.Instance.Publish(new ViewChangedMessage(OnlineMusicPage));
                        break;
                    case 3:
                        MessageBus.Instance.Publish(new ViewChangedMessage(PlayQueuePage));
                        break;
                    case 4:
                        MessageBus.Instance.Publish(new ViewChangedMessage(PlaylistPage));
                        break;
                    case 5:
                        MessageBus.Instance.Publish(new ViewChangedMessage(SettingPage));
                        break;
                }
            }
        }


    }
}
