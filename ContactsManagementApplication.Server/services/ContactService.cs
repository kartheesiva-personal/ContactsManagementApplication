using ContactsManagementApplication.Models;
using ContactsManagementApplication.Server.services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContactsManagementApplication.Services
{
    public class ContactService: IContactService
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "DB\\mock.json");
        private readonly ILogger<ContactService> _logger;

        public ContactService(ILogger<ContactService> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            try
            {
                var contacts = await GetAllContactsAsync();
                return contacts;
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError("File not found: {FilePath}", _filePath+",Message:"+ ex.Message);
                return new List<Contact>();  // Return empty list if file is not found
            }
            catch (JsonException ex)
            {
                _logger.LogError("Error deserializing JSON: {Message}", ex.Message);
                return new List<Contact>();  // Return empty list in case of JSON parsing error
            }
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            var contacts = await GetAllContactsAsync();
            return contacts.FirstOrDefault(c => c.Id == id);
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            var contacts = await ReadContactsFromFileAsync();
            contact.Id = contacts.Any() ? contacts.Max(c => c.Id) + 1 : 1; // Set ID based on the current max ID
            contacts.Add(contact);
            await WriteContactsToFileAsync(contacts);
            return contact;
        }

        public async Task<Contact> UpdateContactAsync(int id, Contact contact)
        {
            var contacts = await ReadContactsFromFileAsync();
            var existingContact = contacts.FirstOrDefault(c => c.Id == id);

            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Email = contact.Email;
                await WriteContactsToFileAsync(contacts);
                return existingContact;
            }
            return null;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contacts = await ReadContactsFromFileAsync();
            var contactToDelete = contacts.FirstOrDefault(c => c.Id == id);

            if (contactToDelete != null)
            {
                contacts.Remove(contactToDelete);
                await WriteContactsToFileAsync(contacts);
                return true;
            }
            return false;
        }

        // Helper method to read contacts from the file
        private async Task<List<Contact>> ReadContactsFromFileAsync()
        {
            if (!File.Exists(_filePath))
            {
                // If the file doesn't exist, return an empty list
                return new List<Contact>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonConvert.DeserializeObject<List<Contact>>(json) ?? new List<Contact>();
        }

        // Helper method to write contacts to the file
        private async Task WriteContactsToFileAsync(List<Contact> contacts)
        {
            var json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            await File.WriteAllTextAsync(_filePath, json);
        }

    }
}
