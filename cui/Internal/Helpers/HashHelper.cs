using System.Collections.Generic;
using cui.Abstractions;

namespace cui.Internal.Helpers
{
    static class HashHelper
    {
        internal static int MakeHash(IList<ControlBase> controls)
        {
            var hash = 0;

            for (var i = 0; i < controls.Count; i++)
            {
                unchecked
                {
                    hash += controls[i].GetHashCode();
                }
            }
            
            return hash;
        }

        internal static bool NeedsToRedraw(int previous, IList<ControlBase> controls)
        {
            return previous != MakeHash(controls);
        }
    }
}