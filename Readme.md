# Angular 6 With Web API 2- CRUD Operations
This is an Angular 6 App using Web API 2 to demonstrate CRUD Operations(Insert, Update and Delete) With EF & SQL Server.

1. Install the below packages in the WebAPI Project using Nuget Package Manager 

	1. Antlr.3.4.1.9004 - Install only if doesnt exist
	2. EntityFramework.6.1.3 - Install only if doesnt exist
	3. Microsoft.AspNet.Cors.5.2.6

2. Build the WebAPI Project

###### Before Running the Angular project
3. Install npm packages using 'npm install' command.


## PostMan Script - Refer Screenshot ##
1. Run the WebAPI and get the Rest URL. e.g.http://localhost:49559/api/Companies
2. Open PostMan and Select "POST"
3. Click the "Body" link and Select "raw" option - Paste the below postman script 
{
	"CompanyName" : "Delta",
	"LastName" : "Technologies",
	"Email" : "info@deltatech.com",
	"PhoneNumber" : "5678942545"	
}
4. Ensure that the "Content-Type" in "Headers" is "application/json" and click "Send"