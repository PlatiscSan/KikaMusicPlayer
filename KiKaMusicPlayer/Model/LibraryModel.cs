using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiKaMusicPlayer.Model
{
    class LibraryModel
    {

        private string m_library_path;
        public string LibraryPath
        {
            get { return m_library_path; }
            set { m_library_path = value;}
        }

    }
}
