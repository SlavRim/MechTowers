global using Defs = MechTowers.Definitions;

namespace MechTowers;

[DefOf]
public static partial class Definitions 
{
    public static HediffDef
        MechTowerBoost;
    public static BuildingDef
        MechControlBeacon,
        MechControlTower,
        BandwidthControlTower,
        MechOverdriveBeacon;
    public static StatCategoryDef 
        MechBuildingCategory;
    public static StatDef 
        MechBuildingMaxLinks,
        MechBuildingRadius,
        MechBuildingBandwidth;
}