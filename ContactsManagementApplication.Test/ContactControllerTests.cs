using ContactManagerApi.Controllers;
using ContactsManagementApplication.Models;
using ContactsManagementApplication.Server.services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace ContactsManagementApplication.Server.UnitTest
{
    public class ContactControllerTests
    {
        private readonly Mock<IContactService> _mockService;
        private readonly ContactController _controller;

        public ContactControllerTests()
        {
            _mockService = new Mock<IContactService>();
            _controller = new ContactController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllContacts_ReturnsOkResult_WithContacts()
        {
            // Arrange
            var contacts = new List<Contact>
        {
            new Contact { Id = 0, FirstName = "Alex",LastName="Roy",Email="alex.roy@aol.net" },
            new Contact { Id = 1,FirstName="Otto",LastName="Blur",Email="otto.blur@aol.de" }
        };

            _mockService.Setup(service => service.GetAllContactsAsync())
                 .ReturnsAsync(contacts);

            var result = await _controller.GetAllContacts();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnContacts = Assert.IsAssignableFrom<IEnumerable<Contact>>(okResult.Value);
            Assert.Equal(2, returnContacts.Count());
        }

        [Fact]
        public async Task GetContactById_ReturnsNotFound_WhenContactDoesNotExist()
        {
            // Arrange
            int contactId = 99;
            _mockService.Setup(service => service.GetContactByIdAsync(contactId))
                        .ReturnsAsync((Contact)null);

            // Act
            var result = _controller.GetContactById(contactId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateContact_ReturnsCreatedAtAction_WhenContactIsCreated()
        {
            // Arrange
            var newContact = new Contact { FirstName = "Alex", LastName = "Roy", Email = "alex.roy@aol.net" };
            var createdContact = new Contact { Id = 3, FirstName = "Alex", LastName = "Roy", Email = "alex.roy@aol.net" };

            _mockService.Setup(service => service.CreateContactAsync(newContact))
                     .ReturnsAsync(createdContact);

            var result = await _controller.CreateContact(newContact);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnContact = Assert.IsType<Contact>(createdResult.Value);
            Assert.Equal("Alex", returnContact.FirstName);
            Assert.Equal("Roy", returnContact.LastName);
            Assert.Equal("alex.roy@aol.net", returnContact.Email);
            Assert.Equal(3, returnContact.Id);
        }

        [Fact]
        public async Task UpdateContact_ReturnsOkResult_WhenContactIsUpdated()
        {
            // Arrange
            int contactId = 1;
            var existingContact = new Contact { Id = contactId, FirstName = "test",LastName="testlastname", Email = "test@example.com" };
            var updatedContact = new Contact { Id = contactId, FirstName = "test", LastName = "testlastname", Email = "test@example.com" };

            // Mock the service to return the updated contact
            _mockService.Setup(service => service.UpdateContactAsync(contactId, updatedContact))
                        .ReturnsAsync(updatedContact);

            // Act
            var result = await _controller.UpdateContact(contactId, updatedContact);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Check if the result is OkObjectResult
            var returnContact = Assert.IsType<Contact>(okResult.Value); // Check if the returned value is of type Contact
            Assert.Equal("test", returnContact.FirstName); // Assert the updated name
            Assert.Equal("testlastname", returnContact.LastName); // Assert the updated name
            Assert.Equal("test@example.com", returnContact.Email); // Assert the updated email
        }

        [Fact]
        public async Task UpdateContact_ReturnsNotFound_WhenContactDoesNotExist()
        {
            // Arrange
            int contactId = 99; // This ID doesn't exist
            var updatedContact = new Contact { Id = contactId, FirstName = "Non-existent Contact",LastName="Non existing contact", Email = "nonexistent@example.com" };

            // Mock the service to return null (i.e., contact not found)
            _mockService.Setup(service => service.UpdateContactAsync(contactId, updatedContact))
                        .ReturnsAsync((Contact)null);

            // Act
            var result = await _controller.UpdateContact(contactId, updatedContact);

            // Assert
            Assert.IsType<NotFoundResult>(result); // Assert that the result is NotFound
        }

        [Fact]
        public async Task DeleteContact_ReturnsNoContent_WhenContactIsDeleted()
        {
            // Arrange
            int contactId = 1; // Existing contact ID
            _mockService.Setup(service => service.DeleteContactAsync(contactId))
                        .ReturnsAsync(true); // Simulate successful deletion

            // Act
            var result = await _controller.DeleteContact(contactId);

            // Assert
            Assert.IsType<NoContentResult>(result); // Assert that the result is NoContent (HTTP 204)
        }

        [Fact]
        public async Task DeleteContact_ReturnsNotFound_WhenContactDoesNotExist()
        {
            // Arrange
            int contactId = 99; // This ID doesn't exist
            _mockService.Setup(service => service.DeleteContactAsync(contactId))
                        .ReturnsAsync(false); // Simulate contact not found

            // Act
            var result = await _controller.DeleteContact(contactId);

            // Assert
            Assert.IsType<NotFoundResult>(result); // Assert that the result is NotFound (HTTP 404)
        }

    }
}

