namespace MechTowers;

public class BoostHediff : HediffWithComps
{
    private BoostModifiers modifiers;
    public BoostModifiers Modifiers 
    { 
        get => modifiers;
        set
        {
            modifiers = value;
            UpdateStage();
        }
    }
    public Building Tower { get; internal set; }
    private HediffStage stage = new();
    private void UpdateStage()
    {
        stage = new()
        {
            statFactors = new()
                {
                    new()
                    {
                        stat = StatDefOf.MoveSpeed,
                        value = modifiers.MoveSpeedModifier
                    },
                    new()
                    {
                        stat = StatDefOf.WorkSpeedGlobal,
                        value = modifiers.WorkSpeedModifier
                    }
                }
        };
    }
    public override HediffStage CurStage => stage;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Deep.Look(ref modifiers, nameof(modifiers));
        UpdateStage();
    }
}