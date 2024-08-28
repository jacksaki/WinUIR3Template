using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Navigation;
using R3;
using WinUIR3Template.Contracts.Services;
using WinUIR3Template.Views;

namespace WinUIR3Template.ViewModels;

public partial class ShellViewModel : ViewModelBase
{
    public BindableReactiveProperty<bool> IsBackEnabled
    {
        get;
    }

    public BindableReactiveProperty<object?> Selected
    {
        get;
    }

    public INavigationService NavigationService
    {
        get;
    }

    public INavigationViewService NavigationViewService
    {
        get;
    }

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService)
        : base()
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        this.IsBackEnabled = new BindableReactiveProperty<bool>();
        this.Selected = new BindableReactiveProperty<object?>();
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        this.IsBackEnabled.Value = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            this.Selected.Value = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            this.Selected.Value = selectedItem;
        }
    }
}
