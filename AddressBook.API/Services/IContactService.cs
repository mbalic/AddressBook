using System.Threading.Tasks;
using AddressBook.API.DTOs;
using AddressBook.API.Helpers;

namespace AddressBook.API.Services
{
    public interface IContactService
    {
        Task<PagedList<ContactListData>> GetContactsListAsync(ContactParams model);

        Task<ServiceResult<ContactDetailsData>> GetContactDetailsAsync(int id);

        Task<ServiceResult> InsertContactAsync(ContactEditData model);
        Task<ServiceResult> UpdateContactAsync(ContactEditData model);
        Task<ServiceResult> DeleteContactAsync(int id);
    }
}