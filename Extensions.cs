global using static MechTowers.Extensions;

namespace MechTowers;

public static partial class Extensions
{
    public const string LogPrefix = $"[{nameof(MechTowers)}] ";
    public static void DebugMessage(object text, Action<string> logAction = null) => (logAction ??= Log.Message).Invoke(LogPrefix + text);

    public static Func<T, bool> ToFunc<T>(this Predicate<T> predicate) => new(predicate);
    public static bool TryInvoke<T>(this Predicate<T> predicate, T obj) => predicate?.Invoke(obj) ?? true;

    public static void DrawCommandRadius(Pawn pawn)
    {
        var cells = GenRadial.RadialCellsAround(pawn.Position, Pawn_MechanitorTracker.MechCommandRange, false).ToHashSet();

        var selectedDraftedMechs = GetSelectedDraftedMechs(pawn).ToList();

        foreach (var building in pawn.GetActiveBoundBuildings())
            if(selectedDraftedMechs.Any(building.IsLinkedOrAble))
                cells.AddRange(building.Territory.Cells);

        DrawCommandRadius(cells.ToList());
    }
    public static void DrawCommandRadius(List<IntVec3> cells) => GenDraw.DrawFieldEdges(cells ?? new(), Color.white);

    public static bool IsMechanitor(this Pawn pawn, out Pawn_MechanitorTracker mechanitor)
    {
        mechanitor = pawn?.mechanitor;
        return mechanitor is not null;
    }
    public static bool IsMechanitor(this Pawn pawn) => IsMechanitor(pawn, out _);
    public static bool IsPlayerFaction(this Thing thing) => thing?.Faction?.IsPlayer ?? false;

    public static IEnumerable<Pawn> GetSelectedDraftedMechs(Pawn mechanitor) => 
        Find.Selector.SelectedPawns
        .Where(x => 
            x.GetOverseer() == mechanitor &&
            x.Drafted
        );
}