using System.Threading.Tasks;
using AddressBook.API.DTOs;
using AddressBook.API.Extensions;
using AddressBook.API.Helpers;
using AddressBook.API.Services;
using AddressBook.API.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AddressBook.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;

        public ContactsController(IContactService contactService, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            this._contactService = contactService;
            this._hubContext = hubContext;
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

        [HttpPost]
        public async Task<IActionResult> InsertContact(ContactEditData contactEditData)
        {
            var response = await this._contactService.InsertContactAsync(contactEditData);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            await _hubContext.Clients.All.BroadcastMessage();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, ContactEditData contactEditData)
        {
            var response = await this._contactService.UpdateContactAsync(contactEditData);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            await _hubContext.Clients.All.BroadcastMessage();

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

            await _hubContext.Clients.All.BroadcastMessage();

            return Ok();
        }
    }
}