namespace Manager.Core.Extensions
{
    public static class Extensions
    {
        public static bool IsNull(this object obj)
        {
            if (obj is null)
                return true;

            return false;
        }
    }
}