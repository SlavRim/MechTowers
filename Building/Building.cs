namespace MechTowers;

public partial class Building : Verse.Building
{
    public Building() { }
    public BuildingDef Def => def as BuildingDef;
    public virtual bool Active => PowerActive && Owner is not null;
    public virtual float Radius => Def.Radius;
    public virtual int BandwidthGain => Def.BandwidthGain;
    public virtual int MaxLinkable => Def.MaxLinkableMechs;

    private CompBoost boost;
    public CompBoost Boost => boost ??= GetComp<CompBoost>();

    public IReadOnlyList<T> GetComponents<T>() => comps.OfType<T>().ToList();

    public void ForEachComponent<T>(Action<T> action)
    {
        var list = GetComponents<T>();
        foreach (var comp in list)
        {
            try
            {
                action?.Invoke(comp);
            }
            catch(Exception ex)
            {
                Log.Error(ex+"");
            }
        }
    }

    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        InitializeTerritory();

        base.SpawnSetup(map, respawningAfterLoad);
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
        ExposeData_Territory();
    }

    public override void Tick()
    {
        if (!Active) return;

        Tick_Territory();
    }
}
