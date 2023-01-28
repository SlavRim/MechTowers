namespace MechTowers;

public class BoostHediff : HediffWithComps
{
    BoostModifiers modifiers;
    public BoostModifiers Modifiers 
    { 
        get => modifiers;
        set
        {
            modifiers = value;
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
    }
    public Building Tower;
    private HediffStage stage = new();
    public override HediffStage CurStage => stage;
}