using ContactsManagementApplication.Models;
using ContactsManagementApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ContactManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public ActionResult<List<Contact>> GetContacts()
        {
            try
            {
                return Ok(_contactService.GetContacts());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> GetContact(int id)
        {
            var contact = _contactService.GetContactById(id);
            if (contact == null)
                return NotFound("Contact not found.");

            return Ok(contact);
        }

        [HttpPost]
        public IActionResult CreateContact([FromBody] Contact contact)
        {
            try
            {
                _contactService.AddContact(contact);
                return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error creating contact.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] Contact contact)
        {
            try
            {
                _contactService.UpdateContact(id, contact);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Contact not found.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            try
            {
                _contactService.DeleteContact(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Contact not found.");
            }
        }
    }
}
