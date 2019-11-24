using System;
using System.Collections.Generic;

namespace AddressBook.API.DTOs
{
    public class ContactDetailsData
    {
        public ContactDetailsData()
        {
            this.PhoneNumbers = new List<PhoneNumberInfoData>();
        }
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public List<PhoneNumberInfoData> PhoneNumbers { get; set; }
    }
}