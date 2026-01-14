using CleanArchitecture.Advanced.Client.Domain.Models;
using System.Windows.Input;

namespace CleanArchitecture.Advanced.Client.Presentation.Interfaces
{
    public interface ILibraryViewModel
    {
        IEnumerable<LibraryModel> Libraries { get; set; }
        LibraryModel SelectedLibrary { get; set; }
        string Details { get; set; }
        string Name { get; set; }
        string Address { get; set; }

        ICommand LoadAllLibrariesCommand { get; }
        ICommand LoadFilteredLibrariesCommand { get; }
        ICommand PrintDetailsCommand { get; }
        ICommand InsertLibraryCommand { get; }
        ICommand UpdateLibraryCommand { get; }
        ICommand DeleteLibraryCommand { get; }
    }
}
