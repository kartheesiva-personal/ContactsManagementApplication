using ContactsManagementApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContactsManagementApplication.Services
{
    public class ContactService
    {
        private readonly string _dataFile = Path.Combine(Directory.GetCurrentDirectory(), "DB\\mock.json");

        public List<Contact> GetContacts()
        {
            try
            {
                if (!File.Exists(_dataFile))
                {
                    return new List<Contact>();
                }

                var jsonData = File.ReadAllText(_dataFile);
                return JsonConvert.DeserializeObject<List<Contact>>(jsonData);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error reading contact data.");
            }
        }

        public Contact GetContactById(int id)
        {
            var contacts = GetContacts();
            return contacts.FirstOrDefault(c => c.Id == id);
        }

        public void AddContact(Contact contact)
        {
            var contacts = GetContacts();
            contact.Id = contacts.Any() ? contacts.Max(c => c.Id) + 1 : 1;
            contacts.Add(contact);
            SaveContacts(contacts);
        }

        public void UpdateContact(int id, Contact contact)
        {
            var contacts = GetContacts();
            var existingContact = contacts.FirstOrDefault(c => c.Id == id);

            if (existingContact == null)
            {
                throw new ApplicationException("Contact not found.");
            }

            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;

            SaveContacts(contacts);
        }

        public void DeleteContact(int id)
        {
            var contacts = GetContacts();
            var contactToDelete = contacts.FirstOrDefault(c => c.Id == id);

            if (contactToDelete == null)
            {
                throw new ApplicationException("Contact not found.");
            }

            contacts.Remove(contactToDelete);
            SaveContacts(contacts);
        }

        private void SaveContacts(List<Contact> contacts)
        {
            var jsonData = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            File.WriteAllText(_dataFile, jsonData);
        }
    }
}
