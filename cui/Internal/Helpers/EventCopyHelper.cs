namespace cui.Internal.Helpers;

internal static class EventCopyHelper
{
    internal static void CopyEventHandlers(INotifyWhenEnteredExited original, INotifyWhenEnteredExited? instance)
    {
        var entered = original.GetEnteredHandlers();
        var exited = original.GetExitedHandlers();

        if (entered is not null)
        {
            foreach (var enter in entered) instance.OnEntered += enter;
        }

        if (exited is not null)
        {
            foreach (var exit in exited) instance.OnExited += exit;
        }
    }
}