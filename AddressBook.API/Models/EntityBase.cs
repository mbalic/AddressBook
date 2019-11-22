using System;

namespace AddressBook.API.Models
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}