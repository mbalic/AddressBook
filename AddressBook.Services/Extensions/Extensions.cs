using System.Linq;
using System.Threading.Tasks;
using AddressBook.Contracts.Shared;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Services
{
    public static class Extensions
    {
        public static async Task<PagedList<T>> CreateAsync<T>(this IOrderedQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
