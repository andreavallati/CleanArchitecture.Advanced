namespace CleanArchitecture.Advanced.Client.Application.Interfaces.Services.Factory
{
    public interface IFactoryUIService
    {
        TService CreateUIService<TService>() where TService : class;
    }
}
