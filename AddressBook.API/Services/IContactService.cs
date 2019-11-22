using System.Threading.Tasks;
using AddressBook.API.DTOs;
using AddressBook.API.Helpers;

namespace AddressBook.API.Services
{
    public interface IContactService
    {
        Task<PagedList<ContactListData>> GetContactsListAsync(ContactParams contactParams);

        Task<ServiceResult<ContactDetailsData>> GetContactDetailsAsync(int id);

        Task<ServiceResult> InsertContactAsync(ContactEditData contactData);
        Task<ServiceResult> UpdateContactAsync(ContactEditData contactData);
        Task<ServiceResult> DeleteContactAsync(int id);
    }
}