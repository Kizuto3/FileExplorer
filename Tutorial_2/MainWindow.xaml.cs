using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Tutorial_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new DirectoryStructureViewModel();
        }
        #endregion        
    }
}
