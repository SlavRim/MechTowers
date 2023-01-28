namespace MechTowers;

public class InfinityStatWorker : StatWorker
{
    public override string ValueToString(float val, bool finalized, ToStringNumberSense numberSense = ToStringNumberSense.Absolute)
    {
        if (val >= stat.maxValue)
            return "∞";
        return base.ValueToString(val, finalized, numberSense);
    }
}
