namespace MechTowers;

public class PawnBuildingsCache : List<Building>
{
    internal static readonly ConditionalWeakDictionary<Pawn, PawnBuildingsCache> Global = new();

    public PawnBuildingsCache(Pawn pawn, IEnumerable<Building> list = null)
    {
        Pawn = pawn;
        if (list is null) return;
        AddRange(list);
    }
    public Pawn Pawn { get; }
}