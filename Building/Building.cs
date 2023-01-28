namespace MechTowers;

public partial class Building : Verse.Building
{
    public Building() { }
    public BuildingDef Def => def as BuildingDef;
    public virtual bool Active => PowerActive;
    public virtual float Radius => Def.Radius;
    public virtual int BandwidthGain => Def.BandwidthGain;
    public virtual int MaxLinkable => Def.MaxLinkableMechs;

    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);

        InitializeTerritory();
    }

    public override void DeSpawn(DestroyMode mode = DestroyMode.Vanish)
    {
        base.DeSpawn(mode);

        FinalizeTerritory();
    }

    public override void ExposeData()
    {
        base.ExposeData();

        ExposeData_Linked();
    }

    public override void Tick()
    {
        if (!Active) return;
        Tick_Territory();
    }
}
