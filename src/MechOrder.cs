using Result = MechTowers.MechOrder.CommandResult;

namespace MechTowers;

public partial class MechOrder
{
    public ICollection<Pawn> Mechs { get; }
    public LocalTargetInfo Target { get; }
    public MechOrder(ICollection<Pawn> mechs, LocalTargetInfo target)
    {
        Mechs = mechs;
        Target = target;
    }

    static DateTime lastMessageTime;
    internal static void SendMessage(string message, bool success)
    {
        if ((DateTime.Now - lastMessageTime).TotalSeconds < 2)
            return;
        var messageDef = MessageTypeDefOf.NegativeEvent;

        if (!string.IsNullOrWhiteSpace(message))
            Messages.Message(message, messageDef, false);

        if (!success)
            Messages.Message(UnableOrderTo.Translate(), messageDef, false);

        lastMessageTime = DateTime.Now;
    }

    public Result AllowedFor(Pawn mechanitor)
    {
        var buildings = mechanitor
            .GetActiveBoundBuildingsForTarget(Target)
            .Select(AllowedFor)
            .Prepend(Result.Disallowed)
            .ToList();
        return buildings.MaxBy(x => (int)x);
    }

    public Result AllowedFor(Building building)
    {
        if (!building.IsLinkedOrAble(Mechs))
            return Result.PossibleLinksExceed;

        if (!Mechs.All(x => building.CanCommandTo(x, Target)))
            return Result.NotAllMechsAllowed;

        return Result.Allowed;
    }
}