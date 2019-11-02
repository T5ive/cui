using cui.Abstractions;

namespace cui.Internal.Helpers
{
    static class EventCopyHelper
    {
        internal static void CopyEventHandlers(MenuBase original, MenuBase instance)
        {
            var entered = original.GetEnteredHandlers();
            var exited = original.GetExitedHandlers();

            if (!(entered is null))
            {
                foreach (var enter in entered) instance.OnEntered += enter;
            }

            if (!(exited is null))
            {
                foreach (var exit in exited) instance.OnExited += exit;
            }
        }
    }
}