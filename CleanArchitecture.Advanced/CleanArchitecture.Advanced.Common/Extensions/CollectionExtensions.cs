namespace CleanArchitecture.Advanced.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static bool HasAny<T>(this IEnumerable<T> val)
        {
            return val != null && val.Any();
        }

        public static bool HasAny<T>(this IEnumerable<T> val, Func<T, bool> predicate)
        {
            return val != null && val.Any(predicate);
        }
    }
}
