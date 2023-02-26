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
        TryAddLinked(pawn);
    }
}
