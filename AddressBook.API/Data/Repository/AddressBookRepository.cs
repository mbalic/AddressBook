using System.Linq;
using System.Threading.Tasks;
using AddressBook.API.Helpers;
using AddressBook.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.API.Data.Repository
{
    public class AddressBookRepository : IAddressBookRepository
    {
        private readonly DataContext _context;
        public AddressBookRepository(DataContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            this._context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this._context.Remove(entity);
        }

        public async Task<Contact> GetContact(int id)
        {
            return await this._context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<PagedList<Contact>> GetContacts(ContactParams contactParams)
        {
            var contacts = this._context.Contacts
                .Include(c => c.PhoneNumbers)
                .AsQueryable()
                .OrderByDescending(c => c.Name);

            return await PagedList<Contact>.CreateAsync(contacts, contactParams.PageNumber, contactParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            // returns changes saved to db
            return await this._context.SaveChangesAsync() > 0;
        }
    }
}