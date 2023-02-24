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

    /// <summary>
    /// Gets cache of buildings bound to the <paramref name="pawn"/>.
    /// </summary>
    /// <param name="pawn"></param>
    public static List<Building> GetBoundBuildings(this Pawn pawn)
    {
        if (pawn is not { MapHeld: not null }) return new();

        bool BoundPredicate(Building building) => 
            building.Owner == pawn;

        if (!PawnBuildingsCache.Global.TryGetValue(pawn, out var cache))
        {
            var buildings = pawn.MapHeld.GetBuildingsByPredicate(BoundPredicate);
            cache = new(pawn, buildings);
        }
        cache.RemoveAll(x => !BoundPredicate(x));

        return (PawnBuildingsCache.Global[pawn] = cache);
    }
    public static IEnumerable<Building> GetBoundBuildings(this Pawn pawn, Predicate<Building> predicate) =>
        pawn.GetBoundBuildings()
        .Where(predicate.ToFunc());
    public static IEnumerable<Building> GetActiveBoundBuildings(this Pawn pawn, Predicate<Building> predicate = null) =>
        pawn.GetBoundBuildings(x =>
            x.Active &&
            predicate.TryInvoke(x)
        );
    public static IEnumerable<Building> GetActiveBoundBuildingsForTarget(this Pawn pawn, LocalTargetInfo target, Predicate<Building> predicate = null) =>
        pawn.GetActiveBoundBuildings(x =>
            x.IsInside(target) &&
            predicate.TryInvoke(x)
        );
}
