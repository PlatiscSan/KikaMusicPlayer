
using KiKaMusicPlayer.Model;
using KiKaMusicPlayer.Utilities;

using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace KiKaMusicPlayer.ViewModel
{
    class SettingsViewModel : BaseViewModel
    {

        private ObservableCollection<LibraryModel> m_libraries = [];
        public ObservableCollection<LibraryModel> Libraries
        {
            get { return m_libraries; }
        }

        public SettingsViewModel() 
        {
            AddCommand = new RelayCommand(Add);
            RemoveCommand = new RelayCommand(Remove);
        }

        public ICommand AddCommand { get; set; }
        private void Add(object parameter)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string folder_path = dialog.SelectedPath;
            m_libraries.Add(new LibraryModel { LibraryPath = folder_path });
        }

        public ICommand RemoveCommand { get; set; }
        private void Remove(object paramter)
        {
            LibraryModel library = (LibraryModel)paramter;
            m_libraries.Remove(library);
        }

    }
}
