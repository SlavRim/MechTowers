namespace MechTowers;

public class CompProperties_Ownable : CompProperties_AssignableToPawn
{
    public CompProperties_Ownable()
    {
        compClass = typeof(CompOwnable);
    }
}
