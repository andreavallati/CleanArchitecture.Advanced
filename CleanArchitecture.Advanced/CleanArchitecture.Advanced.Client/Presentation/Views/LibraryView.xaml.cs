using CleanArchitecture.Advanced.Client.Presentation.Interfaces;
using System.Windows;

namespace CleanArchitecture.Advanced.Client.Presentation.Views
{
    /// <summary>
    /// Interaction logic for LibraryView.xaml
    /// </summary>
    public partial class LibraryView : Window
    {
        public LibraryView(ILibraryViewModel libraryViewModel)
        {
            DataContext = libraryViewModel;
            InitializeComponent();
        }
    }
}
