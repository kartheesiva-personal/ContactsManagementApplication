using ContactsManagementApplication.Models;
using ContactsManagementApplication.Server.services;
using ContactsManagementApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ContactManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // Get All Contacts
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            return Ok(contacts);
        }


        // Get Contact by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        // Create Contact
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }

            var createdContact = await _contactService.CreateContactAsync(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = createdContact.Id }, createdContact);
        }

        // Update Contact by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] Contact contact)
        {
            if (contact == null || id != contact.Id)
            {
                return BadRequest();
            }

            var updatedContact = await _contactService.UpdateContactAsync(id, contact);
            if (updatedContact == null)
            {
                return NotFound();
            }

            return Ok(updatedContact);
        }

        // Delete Contact by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var deleted = await _contactService.DeleteContactAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
