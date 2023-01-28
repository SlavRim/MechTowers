namespace MechTowers;

partial class MechCommander
{
    public enum CommandResult
    {
        Disallowed = -100,
        NotAllMechsAllowed,
        PossibleLinksExceed,
        Allowed = 1
    }
}
