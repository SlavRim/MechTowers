namespace MechTowers;

partial class MechOrder
{
    public enum CommandResult
    {
        Disallowed = -100,
        NotAllMechsAllowed,
        PossibleLinksExceed,
        Allowed = 1
    }
}
