namespace MechTowers;

partial class Building
{
    protected HashSet<Pawn> linkedPawns = new();
    public IReadOnlyCollection<Pawn> LinkedPawns => linkedPawns.Where(IsLinkedInternal).ToList();

    public bool IsReachedMaxLinks => !IsAbleToLink();
    public bool IsAbleToLink(int additionalLinks = 1) => LinkedPawns.Count + additionalLinks <= Def.MaxLinkableMechs;

    internal bool CanCommandTo(Pawn mech, LocalTargetInfo localTarget)
    {
        if (!Active) return false;
        if (!IsLinkedOrAble(mech)) return false;
        return IsInside(localTarget);
    }
    public bool IsLinked(Pawn pawn) => LinkedPawns.Contains(pawn);

    public bool IsLinkedOrAble(Pawn pawn)
    {
        if (!IsLinkable(pawn)) 
            return false;
        return IsLinked(pawn) || IsAbleToLink();
    }
    public bool IsLinkedOrAble(ICollection<Pawn> pawns) =>
        IsAbleToLink(pawns.Count) &&
        pawns.All(IsLinkedOrAble);

    public bool IsLinkable(Pawn pawn)
    {
        if (pawn?.GetOverseer() is not { } overseer)
            return false;
        if(pawn is not { RaceProps.IsMechanoid: true }) 
            return false;
        return overseer == Owner;
    }

    protected virtual bool IsLinkedInternal(Pawn pawn) =>
        IsLinkable(pawn) &&
        IsInside(pawn);

    protected void RemoveLinked(Pawn pawn)
    {
        linkedPawns.Remove(pawn);
        pawn.health.RemoveBoosts(this);
    }
    protected bool TryAddLinked(Pawn pawn)
    {
        if (!IsLinkedInternal(pawn)) return false;
        var linked = linkedPawns.Contains(pawn);
        if (linked || !IsAbleToLink()) return linked;
        linkedPawns.Add(pawn);
        return true;
    }

    protected virtual void ExposeData_Linked()
    {
        Scribe_Collections.Look(ref linkedPawns, nameof(linkedPawns), LookMode.Reference);
    }
}
