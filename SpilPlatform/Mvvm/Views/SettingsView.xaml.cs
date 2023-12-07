using SpilPlatform.Mvvm.ViewModels;

namespace SpilPlatform.Mvvm.Views;

public partial class SettingsView : ContentPage
{
    private readonly IServiceProvider _serviceProvider;

    public SettingsView(IServiceProvider serviceProvider)
	{
        InitializeComponent();
        _serviceProvider = serviceProvider;
        BindingContext = new SettingsViewModel(serviceProvider);
    }
}