// Import the necessary testing modules and classes
import { TestBed } from '@angular/core/testing';  // TestBed to configure the testing module
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';  // For mocking HTTP requests
import { ContactService } from './contact.service';  // The service we're testing
import { HttpClient } from '@angular/common/http';  // HttpClient for HTTP requests (optional for direct usage in tests)
import { Contact } from '../Modal/Contact';
const apiUrl = 'https://localhost:7218/api/contact';
describe('ContactService', () => {

  let service: ContactService;  // Service instance to be tested
  let httpMock: HttpTestingController;  // HttpTestingController for mocking HTTP requests

  beforeEach(() => {
    // Configure the testing module
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],  // Import HttpClientTestingModule to mock HTTP requests
      providers: [ContactService]          // Provide the service to be tested
    });

    service = TestBed.inject(ContactService);  // Inject the service
    httpMock = TestBed.inject(HttpTestingController);  // Inject the HttpTestingController
  });

  afterEach(() => {
    httpMock.verify();  // Ensure there are no outstanding HTTP requests after each test
  });

  it('should be created', () => {
    expect(service).toBeTruthy();  // Test that the service was successfully created
  });

  it('should fetch contacts from the API', () => {
    const mockContacts: Contact[] = [
      { id: 0, firstName: 'Alex', "lastName": "Roy", "email": "alex.roy@aol.net" },
      { id: 1, firstName: 'Otto', "lastName": "Blur", "email": "otto.blur@aol.de" }
    ];

    service.getContacts().subscribe((contacts) => {
      expect(contacts.length).toBe(2);  // Ensure there are two contacts
      expect(contacts).toEqual(mockContacts);  // Ensure contacts match the mock data
    });
   
    // Expect a GET request to the contacts API
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');  // Ensure the request method is GET
    req.flush(mockContacts);  // Return the mock data as the response
  });

  it('should handle errors gracefully', () => {
    const errorMessage = 'Error fetching contacts';

    service.getContacts().subscribe(
      () => { },
      (error) => {
        expect(error).toBe(errorMessage);  // Test that the error message is handled correctly
      }
    );

    // Expect a GET request to the contacts API
    const req = httpMock.expectOne(apiUrl);
    expect(req.request.method).toBe('GET');
    req.flush(errorMessage, { status: 500, statusText: 'Server Error' });  // Simulate an error response
  });
});
