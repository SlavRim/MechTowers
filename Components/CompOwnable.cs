namespace MechTowers;

public class CompOwnable : CompAssignableToPawn
{
    public Building Building => this.GetBuilding();

    private Pawn owner;
    public Pawn Owner
    {
        get => owner ??= assignedPawns.FirstOrDefault();
        protected set
        {
            if (Owner == value) return;
            var oldOwner = Owner;
            assignedPawns.Clear();
            Building?.NotifyOwner(oldOwner, true);
            owner = value;
            if (value is null) return;
            assignedPawns.Add(value);
            Building?.NotifyOwner();
        }
    }

    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        base.PostSpawnSetup(respawningAfterLoad);
        Building?.NotifyOwner();
    }

    public override IEnumerable<Pawn> AssigningCandidates => Building
        .MapHeld
        .mapPawns
        .PawnsInFaction(Faction.OfPlayer)
        .Where(IsAssignable);

    public bool IsAssignable(Pawn pawn) =>
            pawn.MapHeld == Building.MapHeld &&
            pawn.IsPlayerFaction() &&
            pawn.IsMechanitor();

    public override AcceptanceReport CanAssignTo(Pawn pawn) =>
        IsAssignable(pawn);
    public override void TryAssignPawn(Pawn pawn) =>
        TrySetOwner(pawn);
    public override void TryUnassignPawn(Pawn pawn, bool sort = true, bool uninstall = false) =>
        Owner = null;
    public Pawn TrySetOwner(Pawn pawn) =>
        Owner = CanAssignTo(pawn) ? pawn : Owner;
}