using System.Linq.Dynamic.Core;
namespace CarSale.Extensions
{
    public static class SortExtension
    {
        public static IQueryable Sort(this IQueryable query, string sortBy, bool direction) => query.OrderBy($"{sortBy} {(direction ? "descending" : "")}");
    }
}
