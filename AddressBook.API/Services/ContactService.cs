using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.API.Data;
using AddressBook.API.DTOs;
using AddressBook.API.Helpers;
using AddressBook.API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.API.Services
{
    public class ContactService : ServiceBase, IContactService
    {
        protected ContactService(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ServiceResult<ContactDetailsData>> GetContactDetailsAsync(int id)
        {
            var contact = await this.Context.Contacts
                .Where(c => c.Id == id)
                .ProjectTo<ContactDetailsData>(this._mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            var contactDetailsData = this._mapper.Map<ContactDetailsData>(contact);

            return this.Success(contactDetailsData);
        }

        public async Task<PagedList<ContactListData>> GetContactsListAsync(ContactParams contactParams)
        {
            var contactsListData = this.Context.Contacts
                .ProjectTo<ContactListData>(this._mapper.ConfigurationProvider);

            return await PagedList<ContactListData>.CreateAsync(contactsListData, contactParams.PageNumber, contactParams.PageSize);
        }

        public async Task<ServiceResult> InsertContactAsync(ContactEditData contactData)
        {
            var contact = new Contact
            {
                Name = contactData.Name,
                DateOfBirth = contactData.DateOfBirth,
                Address = contactData.Address
            };

            foreach (var item in contactData.PhoneNumbers)
            {
                var phoneNumber = new PhoneNumber
                {
                    Number = item.Number,
                    CountryCode = item.CountryCode
                };

                contact.PhoneNumbers.Add(phoneNumber);
            }

            this.Context.Add(contact);

            var response = await this.SaveAll();

            return response;
        }

        public async Task<ServiceResult> UpdateContactAsync(ContactEditData contactData)
        {
            var contactFromDb = await this.Context.Contacts
                .FirstOrDefaultAsync(c => c.Id == contactData.Id);

            this._mapper.Map(contactData, contactFromDb);
            this.Context.Update(contactFromDb);

            var response = await this.SaveAll();

            return response;
        }

        public async Task<ServiceResult> DeleteContactAsync(int id)
        {
            var contact = await this.Context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id);

            this.Context.Remove(contact);

            var response = await this.SaveAll();

            return response;
        }
    }
}