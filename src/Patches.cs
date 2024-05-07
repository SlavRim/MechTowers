using System.Reflection.Emit;

namespace MechTowers;

[Harmony]
public static partial class Patches
{
    [HarmonyPatch(typeof(Pawn_MechanitorTracker), nameof(get_TotalBandwidth)), HarmonyPostfix]
    public static void get_TotalBandwidth(Pawn_MechanitorTracker __instance, ref int __result)
    {
        var pawn = __instance.Pawn;
        var bandwidth = pawn.GetBoundBandwidth();
        __result += bandwidth;
    }

    [HarmonyPatch(typeof(Pawn_MechanitorTracker), nameof(CanCommandTo)), HarmonyPostfix]
    public static void CanCommandTo(Pawn_MechanitorTracker __instance, ref bool __result, LocalTargetInfo target)
    {
        var pawn = __instance.Pawn;
        var mechs = GetSelectedDraftedMechs(pawn).ToList();
        MechOrder order = new(mechs, target);
        if (!__result) 
            __result = order.AllowedFor(pawn).Handle();
    }

    [HarmonyPatch(typeof(Pawn_MechanitorTracker), nameof(DrawCommandRadius)), HarmonyPrefix]
    public static bool DrawCommandRadius(Pawn_MechanitorTracker __instance)
    {
        var mechanitor = __instance;
        var pawn = __instance.pawn;

        if (!pawn.Spawned || !mechanitor.AnySelectedDraftedMechs)
            return false;

        Extensions.DrawCommandRadius(pawn);

        return false;
    }
}