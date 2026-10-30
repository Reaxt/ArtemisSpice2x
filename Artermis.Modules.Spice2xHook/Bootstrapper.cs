using Artemis.Core;
using Artemis.UI.Shared;
using Artermis.Modules.Spice2xHook.PluginConfig;

namespace Artermis.Modules.Spice2xHook;

public class Bootstrapper : PluginBootstrapper
{
    public override void OnPluginLoaded(Plugin plugin)
    {
        plugin.ConfigurationDialog = new PluginConfigurationDialog<Spice2xPluginConfigurationViewModel>();

    }

    public override void OnPluginEnabled(Plugin plugin)
    {
        
    }
    
    public override void OnPluginDisabled(Plugin plugin)
    {
        
    }
}
