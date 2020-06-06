## EmailSender Api 

This application has an enpoint for sending emails to various users using send grid API. Other providers can easily swapped in because the implementation relies heavily on depndency inversion  principle as well as the template pattern design. The application also has an api documentation hosted with swagger.

## Installation
Clone the application
Install dependencies using the command `dotnet restore`
Build the project using the command `dotnet build`
Run tests using the command `dotnet tests`
Visit the swagger documentation for details on the request body