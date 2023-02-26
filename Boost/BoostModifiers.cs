namespace MechTowers;

public record struct BoostModifiers : IExposable
{
    public float 
        MoveSpeedModifier = 1f, 
        WorkSpeedModifier = 1f;

    public BoostModifiers() { }

    public void ExposeData()
    {
        Scribe_Values.Look(ref MoveSpeedModifier, nameof(MoveSpeedModifier));
        Scribe_Values.Look(ref WorkSpeedModifier, nameof(WorkSpeedModifier));
    }
}