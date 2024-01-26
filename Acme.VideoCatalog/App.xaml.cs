using System;
using System.Net.Http;
using System.Windows;
using Acme.VideoCatalog.DataAccess;
using Acme.VideoCatalog.DataAccess.Dtos;
using Acme.VideoCatalog.Domain.Services;
using Acme.VideoCatalog.Services;
using Acme.VideoCatalog.Services.Repositories;
using Microsoft.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
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
            containerRegistry.RegisterInstance(loggerFactory);

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://assets.acmeaom.com/interview-project/uwpvideos.json")
            };

            containerRegistry.RegisterInstance(httpClient);
            containerRegistry.RegisterSingleton<IRepository<VideoDto>, VideoRepository<VideoDto>>();

            containerRegistry.RegisterSingleton<IVideoCatalogService, VideoCatalogService>();

            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
        }

        protected override Window CreateShell()
        {

            var w = Container.Resolve<MainWindow>();
            return w;
        }
    }
}
