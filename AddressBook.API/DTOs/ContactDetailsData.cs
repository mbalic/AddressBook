using System;
using System.Collections.Generic;

namespace AddressBook.API.DTOs
{
    public class ContactDetailsData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public ICollection<PhoneNumberInfoData> PhoneNumbers { get; set; }
    }
}