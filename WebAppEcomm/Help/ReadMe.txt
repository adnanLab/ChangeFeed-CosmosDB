This project shows you how to use Azure Cosmos DB to store and access data from an ASP.NET MVC application hosted on Azure.
I'm using the .NET SDK V3.
This project will covers:-
Creating an Azure Cosmos account (Azure Portal)
Creating an ASP.NET Core MVC app (Visual Studio 2019)
Connecting the app to Azure Cosmos DB
Performing creates, read, update, and delete (CRUD) operations on the data.

Step 1: Create an Azure Cosmos account

From the Azure portal menu or the Home page, select Create a resource.
On the New page, search for and select Azure Cosmos DB.
On the Azure Cosmos DB page, select Create.
On the Create Azure Cosmos DB Account page, enter the new Azure Cosmos account's basic settings.
The type of account to create Core (SQL) creates a document database and query by using SQL syntax.
Select Review + create. You can skip the Network and Tags sections.

Review the account settings, and then select Create. It takes a few minutes to create the account. 
Wait for the portal page to display Your deployment is complete.

Select Go to resource to go to the Azure Cosmos DB account page.
Go to the Azure Cosmos DB account page, and select Keys. Copy the values to use in the web application you create next.


In Solution Explorer, right-click your project and select Manage NuGet Packages.

In the NuGet Package Manager, search for and select Microsoft.Azure.Cosmos. Select Install.

Set up the ASP.NET Core MVC application.
Add the model class (Product.cs).
Next, let's add the following views.
A create item view
A delete item view
A details view
An edit item view
An index view to get all the products.

Declare and initialize services
CosmosDbService.cs
ICosmosDbService.cs

Define the configuration in the project's appsettings.json file as follows

{ "Logging": { "LogLevel": { "Default": "Warning" } }, "AllowedHosts": "*", "CosmosDb": { "Account": "<Endpoint URI of your Azure Cosmos account>", "Key": "<PRIMARY KEY of your Azure Cosmos account>", "DatabaseName": "Your database name", "ContainerName": "your container name" } }

Add a controller (ProductController.cs)
Now try to run the application locally


appsetting
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*",
  "CosmosDb": {
    //"Account": "<Endpoint URI of your Azure Cosmos account>",
    "Account": "your Azure Cosmos DB account URI endpoint here",
    //"Key": "<PRIMARY KEY of your Azure Cosmos account>",
    "Key": "your Azure Cosmos DB account PRIMARY KEY here",
    //"Database Name": "Your DB Name",
    "DatabaseName": "changefeeddb",
    // "Collection Name": "Your collection Name"
    "ContainerName": "changefeeddbcollection"
  }
}




















