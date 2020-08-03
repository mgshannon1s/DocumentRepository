# DocumentRepository
A simple .Net Core Api to upload and download files over rest asynchronously.

This app truly is a Minimum Viable Product. Since we are using .Net Core and want to implement the basic CRUD operations on a document, it makes the most to sense to implement a basic RESTful web service with Entity Framework to abstract the persistence of the data model. The model we will be concerned with is the Document model. We will use an in memory database context, but this will need to be enhanced in the future to enable long term storage. 

## The Document Model
The model is essentially just the document which will be expressed using a byte array. It also includes some metadata information, for convenience when viewing overall members.

## The Architectural Choices
Our basic .Net Core API with Entity Framework app will give us the functionality to create, update, and delete a basic object as well as view a list of the object. We are also tasked with the issue of passing a file over the web service. To achieve this, we have the following options 
	1. Pass the file and it's metadata through the IFormFile interface. This is the quickest but limits the data that can be passed into the Api. 
	1. use base64 encoding to encode the document as part of the json object itself. That encoding will cost us about 33% in increased size.
	1. Pass the metadata information in the first request, have the server store the metadata and return an ID, then use the PUT method in combination with the ID returned from the server to pass in the Document To Store.
	1. Pass the metadata information in the header and pass the Document To Store in the body of the request.

I chose to go with option 1 given my limited available time but the most effecient, flexible, RESTful option would have been option 3.
The app uses aync function calls and awaits the return in order to avoid blocking where possible. 

Enhancements
	1. Ideally, I would have taken a test driven approach to developing an application like this. Given my schedule, and the overhead to find a client side api to learn, I chose to simply use Postman to test the api.
	1. Add file size limitation
	1. Create web ui
		1. Enable authorizations
		1. Enable user to edit document, then enable multiple users to edit the same document.
		1. Enable sharing of documents between users
	1. Upgrade storage to use something like Azure Blob Storage for long term storage of Documents.
	1. Upgrade service to use a DTO object to prevent over-posting.
	1. Security Considerations
		1. Immediately following upload of a file, it should be scanned for malware and viruses.
		1. The filename that is stored on the server should not be the one provided by the user. In other words, the filename should be chosen by the server so that direct sql injection or some other attack using the character in the user's filename is not possible. 
		1. Need to limit the file extensions allowed to be uploaded. Whitelisting file extensions is the safest, but Blacklisting certain files can provide some security in a pinch.
	1. Add Swagger to document api
	1. Clean up around the edges. This is my first foray into C# Rest Web Services so I'm sure there are performance improvements and standards that could be improved upon.

