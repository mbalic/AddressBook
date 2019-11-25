using System.Threading.Tasks;
using AddressBook.Contracts.DTOs;
using AddressBook.Contracts.Shared;

namespace AddressBook.Contracts.Services
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