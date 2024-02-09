using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acme.VideoCatalog.Domain.Models;
using Acme.VideoCatalog.Domain.Services;
using Prism.Mvvm;
using Prism.Regions;

namespace Acme.VideoCatalog.ViewModels;

public class MovieListViewModel : BindableBase, INavigationAware
{
    private readonly IVideoCatalogService _videoCatalogService;

    public MovieListViewModel(IVideoCatalogService videoCatalogService)
    {
        _videoCatalogService = videoCatalogService;
    }
    
    private ObservableCollection<Video> _videos;
    public ObservableCollection<Video> Videos
    {
        get => _videos;
        set => SetProperty(ref _videos, value);
    }
    
    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        private set => SetProperty(ref _isLoading, value);
    }

    private bool _hasError;
    public bool HasError
    {
        get => _hasError;
        set => SetProperty(ref _hasError, value);
    }
    
    private string _errorMessage;
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public async void OnNavigatedTo(NavigationContext navigationContext)
    {
        await LoadVideoCatalogAsync();
    }

    private async Task LoadVideoCatalogAsync()
    {
        IsLoading = true;
        var result = await _videoCatalogService.GetAllAsync();
        if (result.IsSuccess)
        {
            Videos = new ObservableCollection<Video>(result.Data);
        }
        else
        {
            HasError = true;
            ErrorMessage = result.ErrorMessage;
        }

        IsLoading = false;
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}