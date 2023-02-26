namespace MechTowers;

partial class Building
{
    public Territory Territory
    {
        get;
        protected set;
    }
    protected virtual Territory TerritoryFactory(Building building) => new Territory.Radial(building, building.Radius);
    public Territory.Locator<Pawn> PawnLocator;

    protected virtual void InitializeTerritory()
    {
        Territory ??= TerritoryFactory(this);
        var events = Territory.PawnEvents;
        events.OnEnter += PawnEnter;
        events.OnExit += PawnExit;
        events.OnStay += PawnStay;
        PawnLocator = new(Territory, ThingRequest.ForGroup(ThingRequestGroup.Pawn))
        {
            Predicate = pawn => !pawn.health.Dead
        };
    }
    protected virtual void FinalizeTerritory()
    {
        Territory = null;
        PawnLocator = null;
    }
    protected virtual void Tick_Territory() => PawnLocator?.Tick();

    protected virtual void ExposeData_Territory() => Territory?.ExposeData();

    public virtual bool IsInside(LocalTargetInfo target) => Territory?.IsInsideLocal(target) ?? false;
}