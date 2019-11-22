using System;
using System.Collections.Generic;

namespace AddressBook.API.Models
{
    public class PhoneNumber : EntityBase
    {
        public int ContactId { get; set; }
        public string Number { get; set; }
        public string CountryCode { get; set; }
        public Contact Contact { get; set; }
    }
}