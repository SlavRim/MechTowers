namespace MechTowers;

public class BuildingDef : ThingDef
{
    public BuildingDef()
    {
    }
    public BoostModifiers? Boost = null;
    public float GetStat(StatDef stat) => this.GetStatValueAbstract(stat);

    public float Radius => GetStat(Defs.MechBuildingRadius);
    public int MaxLinkableMechs => (int)GetStat(Defs.MechBuildingMaxLinks);
    public int BandwidthGain => (int)GetStat(Defs.MechBuildingBandwidth);
}