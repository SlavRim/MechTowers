namespace MechTowers;

partial class Building
{
    public virtual void DrawLinkedLines()
    {
        if (Owner is null) return;
        foreach (var pawn in LinkedPawns)
            GenDraw.DrawLineBetween(DrawPos, pawn.DrawPos);
    }
    public override void DrawExtraSelectionOverlays()
    {
        base.DrawExtraSelectionOverlays();
        DrawCommandRadius(Territory.Cells as List<IntVec3>);
        DrawLinkedLines();
    }
}
