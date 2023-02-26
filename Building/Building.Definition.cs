namespace MechTowers;

public class BuildingDef : ThingDef
{
    public BuildingDef()
    {
    }

    public float GetStat(StatDef stat) => this.GetStatValueAbstract(stat);

    public virtual float Radius => GetStat(Defs.MechBuilding_Radius);
    public virtual int MaxLinkableMechs => (int)GetStat(Defs.MechBuilding_MaxLinks);
    public virtual int BandwidthGain => (int)GetStat(Defs.MechBuilding_Bandwidth);
}