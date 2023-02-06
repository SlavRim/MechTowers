namespace MechTowers;

partial class MechOrder
{
    public enum CommandResult : sbyte
    {
        Disallowed = -100,
        NotAllMechsAllowed,
        PossibleLinksExceed,
        Allowed = 1
    }
}
