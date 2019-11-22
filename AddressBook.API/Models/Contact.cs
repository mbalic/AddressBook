using System;
using System.Collections.Generic;

namespace AddressBook.API.Models
{
    public class Contact : EntityBase
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}