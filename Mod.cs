global using Mod = MechTowers.Mod;

namespace MechTowers;

public sealed class Mod : Verse.Mod
{
    public static Mod Instance { get; private set; }
    public readonly Harmony Harmony = new(nameof(MechTowers));
    public Mod(ModContentPack content) : base(content)
    {
        Instance = this;
    }
    public void OnStartup()
    {
        try
        {
            DebugMessage("Patching.");
            Harmony.PatchAll();
            DebugMessage("Loaded successfully.");
        }
        catch (Exception ex)
        {
            DebugMessage("Failed to load.", Log.Error);
            Harmony.UnpatchAll(Harmony.Id);
            DebugMessage(ex, Log.Error);
        }
    }
}