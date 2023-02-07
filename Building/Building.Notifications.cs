namespace MechTowers;

partial class Building
{
    /// <summary>
    /// Notifies owner(mechanitor) and adds/removes building to/from bound cache.
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="remove"></param>
    /// <returns></returns>
    public virtual bool NotifyOwner(Pawn owner = null, bool remove = false)
    {
        // if null try use Owner
        if ((owner ??= Owner) is null) return false;

        var buildings = owner.GetBoundBuildings();
        if (remove) buildings.Remove(this); // remove from cache
        else buildings.AddDistinct(this); // add to cache

        return NotifyMechanitor(owner);
    }

    protected virtual bool NotifyMechanitor(Pawn pawn)
    {
        if (!pawn.IsMechanitor(out var mechanitor)) return false;
        mechanitor.Notify_BandwidthChanged();
        return true;
    }
}
