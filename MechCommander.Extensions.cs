using Result = MechTowers.MechCommander.CommandResult;

namespace MechTowers;

public static partial class MechCommander_Extensions
{
    public static bool IsSuccess(this Result result) => (int)result > 0;
    public static bool Handle(this Result result)
    {
        var message = result switch
        {
            Result.PossibleLinksExceed => MayExceedLimit.Translate(),
            _ => null
        };
        var success = result.IsSuccess();
        MechCommander.SendMessage(message, success);
        return success;
    }
}
