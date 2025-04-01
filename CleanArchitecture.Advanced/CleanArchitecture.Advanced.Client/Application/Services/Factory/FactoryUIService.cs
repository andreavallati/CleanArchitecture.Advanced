using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Advanced.Client.Application.Interfaces.Services.Factory;

namespace CleanArchitecture.Advanced.Client.Application.Services.Factory
{
    public class FactoryUIService : IFactoryUIService
    {
        private readonly IServiceProvider _serviceProvider;

        public FactoryUIService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public TService CreateUIService<TService>() where TService : class
        {
            return _serviceProvider.GetRequiredService<TService>();
        }
    }
}
