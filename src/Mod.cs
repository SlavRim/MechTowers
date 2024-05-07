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
        DebugMessage("Loading.", color: Color.magenta);
        try
        {
#if !DEBUG
            DebugMessage("Verifiying dependencies.");

            foreach (var dependency in Content.ModMetaData.Dependencies)
                if (!ModsConfig.IsActive(dependency.packageId))
                    throw new Exception("Not found needed dependency.");
#endif

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