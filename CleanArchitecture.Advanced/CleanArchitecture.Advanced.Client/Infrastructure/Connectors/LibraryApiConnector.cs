using CleanArchitecture.Advanced.Client.Application.Interfaces.Connectors;
using CleanArchitecture.Advanced.Client.Infrastructure.Connectors.Base;
using System.Net.Http;

namespace CleanArchitecture.Advanced.Client.Infrastructure.Connectors
{
    public class LibraryApiConnector : ApiConnectorBase, ILibraryApiConnector
    {
        public LibraryApiConnector(HttpClient httpClient) : base(httpClient)
        {
        }

        // Eventually implement specific methods for LibraryApiConnector
    }
}
