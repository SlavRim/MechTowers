using Result = MechTowers.MechOrder.CommandResult;

namespace MechTowers;

public static partial class MechOrder_Extensions
{
    public static bool IsSuccess(this Result result) => (sbyte)result > 0;
    public static bool Handle(this Result result)
    {
        var message = result switch
        {
            Result.PossibleLinksExceed => MayExceedLimit.Translate(),
            _ => null
        };
        var success = result.IsSuccess();
        MechOrder.SendMessage(message, success);
        return success;
    }
}
