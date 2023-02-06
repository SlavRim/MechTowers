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

    [HarmonyPatch(typeof(Pawn_MechanitorTracker), nameof(DrawCommandRadius)), HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> DrawCommandRadius(IEnumerable<CodeInstruction> instructions)
    {
        var list = instructions.ToList();
        var drawRing = 
            typeof(GenDraw)
            .GetMethods()
            .Where(x => x.Name == nameof(GenDraw.DrawRadiusRing))
            .MaxBy(x => x.GetParameters().Count());

        var getPawnField = CodeInstruction.LoadField(typeof(Pawn_MechanitorTracker), nameof(Pawn_MechanitorTracker.pawn));

        var idx = list.FindLastIndex(x => x.operand is MethodInfo method && method.DeclaringType == typeof(GenDraw))+1; // last line where draw is called

        list.InsertRange(idx, new List<CodeInstruction>
        {
            new(OpCodes.Ldarg_0),
            getPawnField,
            CodeInstruction.Call(() => Extensions.DrawCommandRadius(null as Pawn))
        });

        return list;
    }
}