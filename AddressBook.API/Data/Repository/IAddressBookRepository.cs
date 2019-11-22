using System.Threading.Tasks;
using AddressBook.API.Helpers;
using AddressBook.API.Models;

namespace AddressBook.API.Data.Repository
{
    public interface IAddressBookRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<Contact>> GetContacts(ContactParams userParams);
        Task<Contact> GetContact(int id);
    }
}