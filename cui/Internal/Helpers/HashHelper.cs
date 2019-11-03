using cui.Interfaces;

namespace cui.Internal.Helpers
{
    static class HashHelper
    {
        internal static int MakeHash(IMenu menu)
        {
            var hash = menu.Index.GetHashCode();

            foreach (var con in menu.Controls)
            {
                unchecked
                {
                    hash = 31 * hash + con.GetHashCode();
                }
            }
            
            return hash;
        }

        internal static bool NeedsToRedraw(int previous, IMenu menu)
        {
            return previous != MakeHash(menu);
        }
    }
}