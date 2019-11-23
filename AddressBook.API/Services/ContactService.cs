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

        public ContactService(DataContext context, IMapper mapper) : base(context, mapper)
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
                //.ProjectTo<ContactDetailsData>(this._mapper.ConfigurationProvider)
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
                });
            //.ProjectTo<ContactListData>(this._mapper.ConfigurationProvider);

            return await PagedList<ContactListData>.CreateAsync(contactsListData, model.PageNumber, model.PageSize);
        }

        public async Task<ServiceResult> InsertContactAsync(ContactEditData model)
        {
            var contact = this.CreateNewEntity<Contact>();
            contact.Name = model.Name;
            contact.DateOfBirth = model.DateOfBirth;
            contact.Address = model.Address;

            //this._mapper.Map(model, contact);

            foreach (var phoneNumber in model.PhoneNumbers)
            {
                var newPhoneNumber = new PhoneNumber
                {
                    Number = phoneNumber.Number,
                    CountryCode = phoneNumber.CountryCode,
                    Description = phoneNumber.Description
                };

                //this._mapper.Map(model.PhoneNumbers, phoneNumber);

                contact.PhoneNumbers.Add(newPhoneNumber);
            }

            this.Context.Add(contact);

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

                //this._mapper.Map(model.PhoneNumbers, phoneNumber);

                contact.PhoneNumbers.Add(newPhoneNumber);
            }

            //this._mapper.Map(model, entity);
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