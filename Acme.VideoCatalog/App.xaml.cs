using System;
using System.Net.Http;
using System.Windows;
using Acme.VideoCatalog.DataAccess;
using Acme.VideoCatalog.DataAccess.Dtos;
using Acme.VideoCatalog.Domain.Services;
using Acme.VideoCatalog.Services;
using Acme.VideoCatalog.Services.Repositories;
using Prism.DryIoc;
using Prism.Ioc;

namespace Acme.VideoCatalog
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://assets.acmeaom.com/interview-project/uwpvideos.json")
            };

            containerRegistry.RegisterInstance(httpClient);
            containerRegistry.RegisterSingleton<IRepository<VideoDto>, VideoRepository<VideoDto>>();

            containerRegistry.RegisterSingleton<IVideoCatalogStore, ApiVideoCatalogStore>();

            containerRegistry.Register<MainWindow, MainWindowViewModel>();
        }

        protected override Window CreateShell()
        {

            var w = Container.Resolve<MainWindow>();
            return w;
        }
    }
}
