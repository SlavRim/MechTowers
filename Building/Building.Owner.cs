namespace MechTowers;

partial class Building
{
    public CompBuildingOwnable Ownable => GetComp<CompBuildingOwnable>();

    public Pawn Owner
    {
        get => Ownable?.Owner;
        set => Ownable?.TrySetOwner(value);
    }
}
