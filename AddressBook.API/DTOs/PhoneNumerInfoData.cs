using System;

namespace AddressBook.API.DTOs
{
    public class PhoneNumberInfoData
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string CountryCode { get; set; }
    }
}