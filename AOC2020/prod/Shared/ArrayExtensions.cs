namespace Shared
{
    public static class ArrayExtensions
    {
        // Assumes the arrays are sorted
        public static bool ElementsAreEqual(this int[] me, int[] you)
        {
            if (me.Length != you.Length)
            {
                return false;
            }
            for (int i = 0; i < me.Length; ++i)
            {
                if (me[i] != you[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}