using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using ReactiveUI.Avalonia;

namespace Artermis.Modules.Spice2xHook.PluginConfig;

public partial class Spice2xPluginConfigurationView : ReactiveUserControl<Spice2xPluginConfigurationViewModel>
{
    public Spice2xPluginConfigurationView()
    {
        InitializeComponent();
    }
}