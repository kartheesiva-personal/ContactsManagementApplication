<h1>ASP.NET Core REST API - Contact Management (JSON File Backend)</h1>
<p>This is a simple ASP.NET Core REST API application that manages contacts. It reads and writes contact data from a local JSON file. The application supports basic CRUD operations (Create, Read, Update, Delete) for managing contact data.</p>
<p>&nbsp;</p>
<p><strong>Table of Contents</strong></p>
<p><a href="#_Setup_Instructions">Setup Instructions</a></p>
<p><a href="#_How_to_Run">How to Run the Application</a></p>
<p><a href="#_Design_Decisions">Design Decisions</a></p>
<p><a href="#_Application_Structure">Application Structure</a></p>
<p>&nbsp;</p>
<h2>Setup Instructions</h2>
<h2>Prerequisites</h2>
<p>To run this application, you need the following:</p>
<h3>.NET SDK 8.0+</h3>
<p>This application uses .NET 8.0 or later. Download the latest version of .NET from the official .NET Download Page.</p>
<h3>Text Editor/IDE</h3>
<p>Visual Studio or Visual Studio Code is recommended. If you're using Visual Studio Code, make sure you have the C# extension installed.</p>
<h3>SQL Server or other databases (Optional)</h3>
<p>This application uses a JSON file for data storage. However, if you choose to modify the app to use a database later, SQL Server or SQLite can be used easily.</p>
<h3>Postman or cURL (for testing the API)</h3>
<p>Postman or cURL will help you test the API endpoints. You can download Postman from here.</p>
<h2>Steps to Set Up the Application</h2>
<h3>Clone the Repository</h3>
<p>Clone the repository from GitHub to your local machine:</p>
<h3>bash</h3>
<p>git clone https://github.com/kartheesiva-personal/ContactsManagementApplication.git</p>
<p>cd ContactsManagementApplication.Server</p>
<h3>Install .NET Dependencies</h3>
<p>Open the terminal (or use the Package Manager Console in Visual Studio) and restore the required NuGet packages:</p>
<p>&nbsp;</p>
<h3>bash</h3>
<p>dotnet restore</p>
<p>This will install all necessary dependencies for the application.</p>
<p>Configure the mock.json File</p>
<p>&nbsp;</p>
<p>The application reads and writes data to a local JSON file named mock.json. Create the mock.json file in the DB folder of the project (if not already present).</p>
<p>Example structure for the mock.json file:</p>
<p>json</p>
<p>[</p>
<p>{</p>
<p>"Id":0,</p>
<p>"FirstName":"Alex",</p>
<p>"LastName":"Roy",</p>
<p>"Email":<a href="mailto:alex.roy@aol.net">alex.roy@aol.net</a></p>
<p>},</p>
<p>{</p>
<p>"Id":1,</p>
<p>"FirstName":"Otto",</p>
<p>"LastName":"Blur",</p>
<p>"Email":<a href="mailto:otto.blur@aol.de">otto.blur@aol.de</a></p>
<p>}</p>
<p>]</p>
<p>Ensure that the mock.json file is accessible and can be read and written by the application.</p>
<p>&nbsp;</p>
<h2>Build the Application</h2>
<p>&nbsp;</p>
<p>Once the dependencies are restored, build the application by running:</p>
<h3>bash</h3>
<p>dotnet build</p>
<p>This will compile the application and prepare it for execution.</p>
<p>&nbsp;</p>
<p>Configure appsettings.json</p>
<p>&nbsp;</p>
<p>Open the appsettings.json file and configure the path to the mock.json file, if necessary. Here's an example configuration:</p>
<p>&nbsp;</p>
<h3>json</h3>
<p>{</p>
<p>&nbsp; "ContactDataFilePath": "DB/mock.json"</p>
<p>}</p>
<p>This configuration specifies the relative path where the mock.json file will be read from and written to.</p>
<p>&nbsp;</p>
<h2>How to Run the Application</h2>
<p>Once the application is set up, follow these steps to run it:</p>
<p>Run the Application Locally</p>
<p>To start the application, run the following command:</p>
<h3>bash</h3>
<p>dotnet run</p>
<h3>Access the API Endpoints</h3>
<p>You can interact with the following REST API endpoints using Postman, cURL, or any HTTP client:</p>
<p>&nbsp;</p>
<p>GET /api/contacts - Retrieve all contacts.</p>
<p>GET /api/contacts/{id} - Retrieve a specific contact by id.</p>
<p>POST /api/contacts - Add a new contact.</p>
<p>PUT /api/contacts/{id} - Update an existing contact.</p>
<p>DELETE /api/contacts/{id} - Delete a contact by id.</p>
<h2>Example cURL commands:</h2>
<p>&nbsp;</p>
<h3>GET all contacts:</h3>
<p>&nbsp;</p>
<h4>bash</h4>
<p>curl http://localhost:5000/api/contacts</p>
<p>POST a new contact:</p>
<h4>bash</h4>
<p>curl -X POST http://localhost:5000/api/contacts -H "Content-Type: application/json" -d '{"id": 3, "name": "Alice Johnson", "email": "alice.johnson@example.com"}'</p>
<h1>Design Decisions</h1>
<p>JSON File as Data Store</p>
<p>The application uses a JSON file (mock.json) as the data source, making it lightweight and simple. This is ideal for small applications or learning projects.</p>
<p>The application reads the JSON file into memory at startup and writes updates back to the file whenever a contact is added, updated, or deleted.</p>
<h2>CRUD Operations</h2>
<p>The application provides basic CRUD operations (Create, Read, Update, Delete) to manage contact information.</p>
<p>Contacts are uniquely identified by their id, and updates to contacts are handled based on this unique identifier.</p>
<h2>In-Memory Data Management</h2>
<p>The contact data is loaded into memory when the application starts. This ensures quick access and manipulation of data. The updates are written back to the JSON file after any modifications.</p>
<p>This approach is efficient for small-scale applications, but for production use, a database should be considered.</p>
<h3>Error Handling</h3>
<p>Basic error handling is implemented using standard HTTP status codes:</p>
<p>200 OK for successful requests.</p>
<p>400 Bad Request for invalid data.</p>
<p>404 Not Found when a requested contact does not exist.</p>
<p>500 Internal Server Error for unexpected issues.</p>
<h3>Application Structure</h3>
<p>The application is organized as follows:</p>
<p>&nbsp;</p>
<ol>
<li>Controllers</li>
</ol>
<p>ContactController.cs: Handles HTTP requests for managing contacts (CRUD operations). It interacts with the service layer to perform actions like retrieving, adding, updating, and deleting contacts.</p>
<ol start="2">
<li>Services</li>
</ol>
<p>ContactService.cs: Manages reading from and writing to the contacts.json file. It provides data access functionality for the ContactService.</p>
<ol start="3">
<li>Models</li>
</ol>
<p>Contact.cs: Represents the data model for a contact (contains properties like id, name, and email).</p>
<ol start="4">
<li>Configuration</li>
</ol>
<p>appsettings.json: Configuration file for storing application settings such as the file path for mock.json.</p>
