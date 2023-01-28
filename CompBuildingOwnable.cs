namespace MechTowers;

public class CompBuildingOwnable : CompAssignableToPawn
{
    public Building Building => parent as Building;

    Pawn pawn;
    public Pawn Owner
    {
        get => pawn ??= assignedPawns.FirstOrDefault();
        protected set
        {
            if (Owner == value) return;
            var oldOwner = Owner;
            assignedPawns.Clear();
            Building?.NotifyOwner(oldOwner, true);
            if (value is null) return;
            assignedPawns.Add(pawn = value);
            Building?.NotifyOwner();
        }
    }


    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        base.PostSpawnSetup(respawningAfterLoad);
        Building?.NotifyOwner();
    }

    public override IEnumerable<Pawn> AssigningCandidates => PawnsFinder
        .AllMapsCaravansAndTravelingTransportPods_Alive_FreeColonists
        .Where(IsAssignable);
    
    public bool IsAssignable(Pawn pawn) => 
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