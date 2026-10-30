namespace Artermis.Modules.Spice2xHook.GameData;

public class BeatmaniaIIDXDefinition
{
    //EFFECTIVELY this is a notes plugin, originally i was gonna use this data and hard code each game, but i later found you can use dynamic models instead.
    //That being said, this stays here cuz it may be useful for debugging or in the future. It is not necessary 
    public static string[] SingleColorLights =
    {
        "P1 1",
        "P1 2",
        "P1 3",
        "P1 4",
        "P1 5",
        "P1 6",
        "P1 7",
        "P2 1",
        "P2 2",
        "P2 3",
        "P2 4",
        "P2 5",
        "P2 6",
        "P2 7",
        "P1 Start",
        "P2 Start",
        "VEFX",
        "Effect",
        "Spot Light 1",
        "Spot Light 2",
        "Spot Light 3",
        "Spot Light 4",
        "Spot Light 5",
        "Spot Light 6",
        "Spot Light 7",
        "Spot Light 8",
        "Neon Lamp",
        "Turntable P1 Resistance",
        "Turntable P2 Resistance",
    };

    public static string[] RGBLights = new string[]
    {        
        "Woofer RGB",
        "IC Card Reader P1 RGB",
        "IC Card Reader P2 RGB",
        "Turntable P1 RGB",
        "Turntable P2 RGB",
        "Stage Left Avg RGB",
        "Stage Right Avg RGB",
        "Cabinet Left Avg RGB",
        "Cabinet Right Avg RGB",
        "Control Panel Under Avg RGB",
        "Ceiling Left Avg RGB",
        "Title Left Avg RGB",
        "Title Right Avg RGB",
        "Ceiling Right Avg RGB",
        "Touch Panel Left Avg RGB",
        "Touch Panel Right Avg RGB",
        "Side Panel Left Inner Avg RGB",
        "Side Panel Left Outer Avg RGB",
        "Side Panel Left Avg RGB",
        "Side Panel Right Outer Avg RGB",
        "Side Panel Right Inner Avg RGB",
        "Side Panel Right Avg RGB",
    };
}