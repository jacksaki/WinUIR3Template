using System.Reflection;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;
using R3;
using Windows.ApplicationModel;

using WinUIR3Template.Contracts.Services;
using WinUIR3Template.Helpers;

namespace WinUIR3Template.ViewModels;

public partial class SettingsViewModel : ViewModelBase
{
    private readonly IThemeSelectorService _themeSelectorService;

    public BindableReactiveProperty<ElementTheme> ElementTheme
    {
        get;
    }

    public BindableReactiveProperty<string> VersionDescription
    {
        get;
    }

    public ReactiveCommand<ElementTheme> SwitchThemeCommand
    {
        get;
    }

    public SettingsViewModel(IThemeSelectorService themeSelectorService)
    {
        _themeSelectorService = themeSelectorService;
        this.ElementTheme = new BindableReactiveProperty<ElementTheme>(_themeSelectorService.Theme);
        this.VersionDescription = new BindableReactiveProperty<string>(GetVersionDescription());
        this.SwitchThemeCommand = new ReactiveCommand<ElementTheme>();
        this.SwitchThemeCommand.SubscribeAwait(async (x, ct) =>
        {
            if (this.ElementTheme.Value != x)
            {
                this.ElementTheme.Value = x;
                await _themeSelectorService.SetThemeAsync(x);
            }
        });
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
