namespace MechTowers;

partial class Building
{
    public CompPowerTrader Power => GetComp<CompPowerTrader>();
    public bool PowerActive => Power.PowerOn;
}
