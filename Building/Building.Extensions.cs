namespace MechTowers;

public static partial class Building_Extensions
{
    public static int GetBoundBandwidth(this Pawn pawn) =>
        pawn.GetActiveBoundBuildings()
        .Select(x => x.Def.BandwidthGain)
        .Sum();

    public static IEnumerable<Building> GetAllBuildings(this Map map) =>
        map.listerThings.AllThings.OfType<Building>();

    public static IEnumerable<Building> GetBuildingsByPredicate(this Map map, Predicate<Building> predicate) =>
        map.GetAllBuildings().Where(predicate.ToFunc());

    public static IEnumerable<Building> GetLinkedBuildings(this Pawn pawn, Predicate<Building> predicate = null) =>
        pawn.Map
        .GetBuildingsByPredicate(x =>
            x.IsLinked(pawn) &&
            predicate.TryInvoke(x)
        );

    public static List<Building> GetBoundBuildings(this Pawn pawn)
    {
        if (pawn is not { MapHeld: not null }) return new();
        if (!PawnBuildingsCache.Global.TryGetValue(pawn, out var cache))
        {
            var buildings = pawn.MapHeld.GetBuildingsByPredicate(x =>
                x.Owner == pawn
            );
            cache = new(pawn, buildings);
        }
        cache.RemoveAll(x => x.Owner != pawn);
        return (PawnBuildingsCache.Global[pawn] = cache);
    }
    public static IEnumerable<Building> GetBoundBuildings(this Pawn pawn, Predicate<Building> predicate) =>
        pawn.GetBoundBuildings()
        .Where(predicate.ToFunc());
    public static IEnumerable<Building> GetActiveBoundBuildings(this Pawn pawn, Predicate<Building> predicate = null) =>
        pawn.GetBoundBuildings(x =>
            x.MapHeld == pawn.MapHeld &&
            x.Active &&
            predicate.TryInvoke(x)
        );
    public static IEnumerable<Building> GetActiveBoundBuildingsForTarget(this Pawn pawn, LocalTargetInfo target, Predicate<Building> predicate = null) =>
        pawn.GetActiveBoundBuildings(x =>
            x.IsInside(target) &&
            predicate.TryInvoke(x)
        );
}
