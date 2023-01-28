namespace MechTowers;

partial class Building
{
    protected void PawnEnter(Pawn pawn)
    {
        NotifyMechanitor(pawn);
    }

    protected void PawnExit(Pawn pawn)
    {
        NotifyMechanitor(pawn);
        RemoveLinked(pawn);
    }

    protected void PawnStay(Pawn pawn)
    {
        if (!TryAddLinked(pawn)) return;
        var boosts = pawn.health.GetBoosts(this);
        if (boosts.Count <= 0)
            pawn.health.AddBoost(this);
    }
}
