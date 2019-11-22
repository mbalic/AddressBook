using System.Threading.Tasks;
using AddressBook.API.DTOs;
using AddressBook.API.Extensions;
using AddressBook.API.Helpers;
using AddressBook.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IContactService _contactService;

        public HomeController(IContactService contactService)
        {
            this._contactService = contactService;
        }

        [HttpGet("{id}", Name = "GetContact")]
        public async Task<IActionResult> GetContact(int id)
        {
            var response = await this._contactService.GetContactDetailsAsync(id);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts([FromQuery]ContactParams contactParams)
        {
            var response = await this._contactService.GetContactsListAsync(contactParams);
            Response.AddPagination(response.CurrentPage, response.PageSize, response.TotalCount, response.TotalPages);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, ContactEditData contactEditData)
        {
            var response = await this._contactService.UpdateContactAsync(contactEditData);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var response = await this._contactService.DeleteContactAsync(id);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok();
        }

    }
}