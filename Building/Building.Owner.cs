namespace MechTowers;

partial class Building
{
    private CompOwnable ownable;
    public CompOwnable Ownable => ownable ??= GetComp<CompOwnable>();

    public Pawn Owner
    {
        get => Ownable.Owner;
        set => Ownable.TrySetOwner(value);
    }
}
