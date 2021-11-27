namespace ShishaProject.Common.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    public static class LinqHelper
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static IEnumerable<T> FilterNulls<T>(this IEnumerable<T> collection)
        {
            return collection.Where(x => x != null);
        }
    }
}
