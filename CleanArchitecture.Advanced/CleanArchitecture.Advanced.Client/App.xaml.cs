using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using BaseApplication = System.Windows.Application;
using CleanArchitecture.Advanced.Client.Application.Interfaces.Connectors;
using CleanArchitecture.Advanced.Client.Application.Interfaces.Services.Factory;
using CleanArchitecture.Advanced.Client.Application.Interfaces.Services;
using CleanArchitecture.Advanced.Client.Application.Mappings;
using CleanArchitecture.Advanced.Client.Application.Services.Factory;
using CleanArchitecture.Advanced.Client.Application.Services;
using CleanArchitecture.Advanced.Client.Application.Validation;
using CleanArchitecture.Advanced.Client.Domain.Models;
using CleanArchitecture.Advanced.Client.Infrastructure.Connectors;
using CleanArchitecture.Advanced.Client.Presentation.Interfaces;
using CleanArchitecture.Advanced.Client.Presentation.ViewModels;
using CleanArchitecture.Advanced.Client.Presentation.Views;

namespace CleanArchitecture.Advanced.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : BaseApplication
    {
        // The service provider that holds the DI container
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            // Configure DI services
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Build the service provider
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // Register AutoMapper and mapping profiles
            services.AddAutoMapper(typeof(LibraryProfile));

            // Register validators
            services.AddScoped<IValidator<LibraryModel>, LibraryValidator>();

            // Register HttpClient for API connectors
            services.AddHttpClient<ILibraryApiConnector, LibraryApiConnector>();

            // Register services
            services.AddSingleton<IFactoryUIService, FactoryUIService>();
            services.AddSingleton<ILibraryUIService, LibraryUIService>();

            // Register ViewModels
            services.AddSingleton<ILibraryViewModel, LibraryViewModel>();

            // Register the main window
            services.AddSingleton<LibraryView>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Resolve MainWindow and show it
            var mainWindow = _serviceProvider.GetRequiredService<LibraryView>();
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
