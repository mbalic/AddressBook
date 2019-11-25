using System.Collections.Generic;
using AddressBook.Models;
using Newtonsoft.Json;

namespace AddressBook.Data.EF
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            this._context = context;
        }

        public void SeedContacts()
        {
            var contactData = System.IO.File.ReadAllText("Data/ContactSeedData.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(contactData);

            this._context.AddRangeAsync(contacts);
            this._context.SaveChanges();
        }
    }
}