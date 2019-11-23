using AddressBook.API.DTOs;
using AddressBook.API.Models;
using AutoMapper;

namespace AddressBook.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Contact, ContactListData>();
            CreateMap<Contact, ContactDetailsData>();
            CreateMap<Contact, ContactEditData>();
            CreateMap<ContactEditData, Contact>()
                .ForMember(p => p.PhoneNumbers, p => p.Ignore());
            CreateMap<PhoneNumber, PhoneNumberInfoData>().ReverseMap();
        }
    }
}