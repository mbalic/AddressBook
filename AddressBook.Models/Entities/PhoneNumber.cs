namespace AddressBook.Models
{
    public class PhoneNumber
    {
        public int ContactId { get; set; }
        public string Number { get; set; }
        public string CountryCode { get; set; }
        public string Description { get; set; }
        public Contact Contact { get; set; }
    }
}