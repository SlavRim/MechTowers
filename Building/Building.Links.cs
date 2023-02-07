namespace MechTowers;

partial class Building
{
    protected HashSet<Pawn> linkedPawns = new();
    public IReadOnlyCollection<Pawn> LinkedPawns => linkedPawns.Where(IsLinkedInternal).ToList();

    public bool IsReachedMaxLinks => !IsAbleToLink();

    /// <summary>
    /// Basically, returns true if <paramref name="additionalLinks"/> is within limit.
    /// </summary>
    /// <param name="additionalLinks"></param>
    /// <returns>linked pawns count + <paramref name="additionalLinks"/> &lt;= limit of linkable mechs</returns>

    internal bool CanCommandTo(Pawn mech, LocalTargetInfo localTarget)
    {
        if (!Active) return false;
        if (!IsLinkedOrAble(mech)) return false;

        return IsInside(localTarget);
    }

    public bool IsLinked(Pawn pawn) => LinkedPawns.Contains(pawn);

    public bool IsLinkedOrAble(Pawn pawn)
    {
        if (!IsLinkable(pawn)) // disallow if not linkable
            return false;
        return // allow if building able to link 1 or is already linked  
            IsAbleToLink() ||
            IsLinked(pawn);
    }
    public bool IsLinkedOrAble(ICollection<Pawn> pawns)
    {
        if (pawns.All(IsLinked)) return true; // allow if all pawns is linked

        return // allow if building able to link N mechs and all is linkable
            IsAbleToLink(pawns.Count) &&
            pawns.All(IsLinkable);
    }

    /// <summary>
    /// Determines if pawn is able to be linked.
    /// </summary>
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
