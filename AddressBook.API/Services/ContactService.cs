using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.API.Data;
using AddressBook.API.DTOs;
using AddressBook.API.Helpers;
using AddressBook.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.API.Services
{
    public class ContactService : ServiceBase, IContactService
    {

        public ContactService(DataContext context) : base(context)
        {
        }

        public async Task<ServiceResult<ContactDetailsData>> GetContactDetailsAsync(int id)
        {
            var contact = await this.Context.Contacts
                .Where(c => c.Id == id)
                .Select(c => new ContactDetailsData
                {
                    Id = c.Id,
                    Name = c.Name,
                    DateOfBirth = c.DateOfBirth,
                    DateCreated = c.DateCreated,
                    Address = c.Address,
                    PhoneNumbers = c.PhoneNumbers
                        .Select(p => new PhoneNumberInfoData
                        {
                            Number = p.Number,
                            CountryCode = p.CountryCode,
                            Description = p.Description
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (contact == null)
            {
                return this.Failure<ContactDetailsData>("Contact not found");
            }

            return this.Success(contact);
        }

        public async Task<PagedList<ContactListData>> GetContactsListAsync(ContactParams model)
        {
            var contactsListData = this.Context.Contacts
                .Select(c => new ContactListData
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name);

            return await PagedList<ContactListData>.CreateAsync(contactsListData, model.PageNumber, model.PageSize);
        }

        public async Task<ServiceResult> InsertContactAsync(ContactEditData model)
        {
            var contact = this.CreateNewEntity<Contact>();
            contact.Name = model.Name;
            contact.DateOfBirth = model.DateOfBirth;
            contact.Address = model.Address;

            var newPhoneNumbers = model.PhoneNumbers
                .Select(p => new PhoneNumber
                {
                    ContactId = contact.Id,
                    Number = p.Number,
                    CountryCode = p.CountryCode,
                    Description = p.Description
                }).ToList();

            contact.PhoneNumbers = newPhoneNumbers;
            await this.Context.AddAsync(contact);

            var response = await this.SaveAll();

            return response;
        }

        public async Task<ServiceResult> UpdateContactAsync(ContactEditData model)
        {
            var contact = await this.Context.Contacts
                .Include(c => c.PhoneNumbers)
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (contact == null)
            {
                return this.Failure("Contact not found");
            }

            contact.Name = model.Name;
            contact.DateOfBirth = model.DateOfBirth;
            contact.Address = model.Address;

            // Wipeout old numbers and insert new ones
            if (contact.PhoneNumbers.Any())
            {
                foreach (var oldPhoneNumber in contact.PhoneNumbers)
                {
                    this.Context.Remove(oldPhoneNumber);
                }
            }

            foreach (var phoneNumber in model.PhoneNumbers)
            {
                var newPhoneNumber = new PhoneNumber
                {
                    Number = phoneNumber.Number,
                    CountryCode = phoneNumber.CountryCode,
                    Description = phoneNumber.Description
                };

                contact.PhoneNumbers.Add(newPhoneNumber);
            }

            this.Context.Update(contact);

            var response = await this.SaveAll();

            return response;
        }

        public async Task<ServiceResult> DeleteContactAsync(int id)
        {
            var contact = await this.Context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contact == null)
            {
                return this.Failure("Contact not found");
            }

            this.Context.Remove(contact);

            var response = await this.SaveAll();

            return response;
        }
    }
}