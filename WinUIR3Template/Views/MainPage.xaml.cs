using Microsoft.UI.Xaml.Controls;

using WinUIR3Template.ViewModels;

namespace WinUIR3Template.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
