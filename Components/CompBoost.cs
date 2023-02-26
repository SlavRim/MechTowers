namespace MechTowers;

public class CompBoost : ThingComp
{
    public Building Building => this.GetBuilding();
    public CompProperties_Boost Props => props as CompProperties_Boost;

    public override void PostSpawnSetup(bool respawningAfterLoad) 
    {
        base.PostSpawnSetup(respawningAfterLoad);
        Building.Territory.PawnEvents.OnStay += PawnStay;
    }

    public void PawnStay(Pawn pawn) 
    {
        if (!Building.Active) return;

        ApplyBoost(pawn);
    }

    public void ApplyBoost(Pawn pawn)
    {
        var health = pawn.health;
        var boosts = health.GetBoosts(Building);
        if (boosts.Count <= 0)
            health.AddBoost(Props.Modifiers, Building);
    }
}