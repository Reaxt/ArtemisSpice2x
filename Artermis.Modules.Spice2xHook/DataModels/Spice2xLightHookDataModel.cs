using Artemis.Core.Modules;

namespace Artermis.Modules.Spice2xHook.DataModels;

public class LightsDataModel : DataModel
{
};

public class RGBDataModel : DataModel
{
    
}
public class Spice2xLightHookDataModel : DataModel
{
    public bool Connected { get; set; }
    public LightsDataModel MonoLights { get; set; }
    public RGBDataModel RGBLights { get; set; }
}
