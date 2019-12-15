using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Tutorial_2
{
    /// <summary>
    /// The view model for the application main Directory view
    /// </summary>
    public class DirectoryStructureViewModel : BaseViewModel
    {

        #region Public Properties
        /// <summary>
        /// A list of all directories on the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }
        #endregion

        #region Default constructor

        public DirectoryStructureViewModel()
        {
            // Get the logical data
            var children = DirectoryStructure.GetLogicalDrives();

            // Create the view models for the data
            this.Items = new ObservableCollection<DirectoryItemViewModel>(children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
        }
        #endregion
    }
}
