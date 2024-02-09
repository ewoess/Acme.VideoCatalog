using System;
using System.Net.Http;
using System.Windows;
using Acme.VideoCatalog.Core;
using Acme.VideoCatalog.DataAccess;
using Acme.VideoCatalog.DataAccess.Dtos;
using Acme.VideoCatalog.DataAccess.HttpClient;
using Acme.VideoCatalog.DataAccess.Repositories;
using Acme.VideoCatalog.Domain.Services;
using Acme.VideoCatalog.Services;
using Acme.VideoCatalog.Services.Profiles;
using Acme.VideoCatalog.ViewModels;
using Acme.VideoCatalog.Views;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
using Serilog;

namespace Acme.VideoCatalog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string commonAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string logsDirectory = System.IO.Path.Combine(commonAppDataPath, "Acme", "VideoCatalog", "Logs");
            string logFilePath = System.IO.Path.Combine(logsDirectory, "log-.txt");

            if (!System.IO.Directory.Exists(logsDirectory))
            {
                System.IO.Directory.CreateDirectory(logsDirectory);
            }

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(System.IO.Path.Combine(logFilePath), rollingInterval: RollingInterval.Day)
                .MinimumLevel.Information()
                .CreateLogger();

            Log.Logger.Information("Starting application");

            base.OnStartup(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ILoggerFactory loggerFactory = new LoggerFactory().AddSerilog();
            containerRegistry.RegisterInstance<ILoggerFactory>(loggerFactory);

            RegisterHttpClient(containerRegistry);

            RegisterMapper(containerRegistry);

            containerRegistry.RegisterSingleton<IRepository<VideoDto>, VideoRepository<VideoDto>>();

            containerRegistry.RegisterSingleton<IVideoCatalogService, VideoCatalogService>();

            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
            containerRegistry.RegisterForNavigation<MovieListView, MovieListViewModel>(PageNames.MovieListPage);
        }

        /// <summary>
        /// Registers an instance of <see cref="System.Net.Http.HttpClient"/> with the specified Prism container registry.
        /// This <see cref="HttpClient"/> is pre-configured with a base address pointing to a specific URL.
        /// </summary>
        /// <param name="containerRegistry">The Prism container registry where the <see cref="HttpClient"/> instance will be registered.</param>
        /// <remarks>
        /// The method configures the <see cref="HttpClient"/> to use a predefined base address. It is intended
        /// to facilitate HTTP requests to a specific JSON resource containing video data. The registered <see cref="HttpClient"/>
        /// can be resolved and used throughout the application for accessing this resource. This simplifies the process
        /// of making HTTP requests by pre-configuring the client with common settings.
        /// </remarks>
        private static void RegisterHttpClient(IContainerRegistry containerRegistry)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://run.mocky.io/v3/")
            };
            containerRegistry.RegisterInstance(httpClient);
        }

        /// <summary>
        /// Registers the AutoMapper <see cref="IMapper"/> instance with the specified Prism container registry. 
        /// This method configures AutoMapper by adding the specified profile and then registers 
        /// the resulting IMapper instance as a singleton in the container, ensuring that it can be 
        /// injected wherever needed throughout the application.
        /// </summary>
        /// <param name="containerRegistry">The Prism container registry where the IMapper instance will be registered. 
        /// This allows the IMapper to be resolved through dependency injection throughout the application.</param>
        /// <remarks>
        /// The method specifically adds the <see cref="VideoProfile"/> to the AutoMapper configuration. 
        /// This profile should define all the necessary mapping configurations for the application related to videos.
        /// Ensure that the <see cref="VideoProfile"/> class is properly defined and includes all necessary mappings 
        /// before using this method. This method is private and static, intended to be called during application startup 
        /// or configuration phase.
        /// </remarks>
        private static void RegisterMapper(IContainerRegistry containerRegistry)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VideoProfile());
            });
            
            IMapper mapper = mapperConfiguration.CreateMapper();
            containerRegistry.RegisterInstance<IMapper>(mapper);
        }

        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RequestNavigate(RegionNames.MainRegion, PageNames.MovieListPage, result =>
            {
                if (result.Result == false)
                {
                    Log.Logger.Error("Failed to navigate to {PageName}", PageNames.MovieListPage);
                }
            });
        }
    }
}
