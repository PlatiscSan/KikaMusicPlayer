using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiKaMusicPlayer.Model
{
    class MenuItemModel
    {
        private PackIconKind m_icon_kind;
        public PackIconKind IconKind
        {
            get { return m_icon_kind; }
            set { m_icon_kind = value; }
        }

        private string m_header_text = string.Empty;
        public string HeaderText
        {
            get { return m_header_text; }
            set { m_header_text = value; }
        }

        private string m_description_text = string.Empty;
        public string DescriptionText
        {
            get { return m_description_text; }
            set { m_description_text = value; }
        }
    }
}
