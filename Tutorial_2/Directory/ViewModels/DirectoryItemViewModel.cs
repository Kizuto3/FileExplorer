using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Tutorial_2
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The type of this item
        /// </summary>

        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The absolute path to this item
        /// </summary>
        
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }

        /// <summary>
        /// A list of all children contained inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// A image for this item
        /// </summary>
        public string ImageName => Type == DirectoryItemType.Drive ? "drive.png" : (Type == DirectoryItemType.File ? "file.png" : (IsExpanded ? "folder-open.png" : "folder-closed.png"));

        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// Indicates if current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                // If the UI tells us to expand...
                if (value == true)
                    //Find all cheldren
                    Expand();
                // If the UI tells us to close
                else
                    this.ClearChildren();
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Removes all cheldren from the list, adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            // Clear items
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            //Show the expand arrow if we are not a file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }
        #endregion

        #region Public Commands

        /// <summary>
        /// The command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath">The full path of this item</param>
        /// <param name="type">The type of this item</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            //Set path and type
            this.FullPath = fullPath;
            this.Type = type;

            // Create commands
            this.ExpandCommand = new RelayCommand(Expand);

            // Setup the children we needed
            this.ClearChildren();
        }

        #endregion

        /// <summary>
        /// Expands this directory and finds all children
        /// </summary> 

        private void Expand()
        {
            //We cannot expand a file
            if (this.Type == DirectoryItemType.File)
                return;

            //Find all children
            var children = DirectoryStructure.GetDirectoryContent(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel> (children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }
}
