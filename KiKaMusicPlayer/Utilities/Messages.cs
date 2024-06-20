

using System.Collections;
using System.Windows.Documents;
using static KiKaMusicPlayer.Utilities.PlayStateChangedMessage;

namespace KiKaMusicPlayer.Utilities
{
    abstract class BaseMessage
    {

    }

    public enum PlayState
    {
        Stopped,
        Playing,
        Paused,
        Resume
    }

    class PlayStateChangedMessage : BaseMessage
    {

        private PlayState m_state;
        public PlayState State { get => m_state; }

        public PlayStateChangedMessage(PlayState new_state)
        {
            m_state = new_state;
        }

    }

    public enum PageView
    {
        HomePage,
        LocalMusicPage,
        OnlineMusicPage,
        PlaylistPage,
        PlayQueuePage,
        SettingPage,
        PlayPage,
    }

    class ViewChangedMessage : BaseMessage
    {

        private PageView m_view;
        public PageView View { get => m_view; }

        public ViewChangedMessage(PageView new_view)
        {
            m_view = new_view;
        }

    }
    class OpenFilesMessage : BaseMessage
    {
        private List<string> m_paths;
        public List<string> Paths { get => m_paths; }
        public OpenFilesMessage(string[] paths)
        {
            m_paths = new List<string>(paths);
        }
        public OpenFilesMessage(List<string> paths)
        {
            m_paths = new List<string>(paths);
        }

    }

    enum DialogMessageType
    {
        Show,
        Close,
    }

    class DialogMessage : BaseMessage
    {

        private DialogMessageType m_type;
        public DialogMessageType DialogMessageType
        {
            get { return m_type; }
        }

        private object m_dialog_content;
        public object DialogContent { get => m_dialog_content; }

        public DialogMessage(DialogMessageType type, object dialog_content)
        {
            m_type = type;
            m_dialog_content = dialog_content;
        }

    }


}
