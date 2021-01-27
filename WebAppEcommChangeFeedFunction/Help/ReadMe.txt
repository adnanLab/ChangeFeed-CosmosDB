On this project, WebAppEcommChangeFeedFunction, When a document is created or modified in the collection you made, the Azure Function will be trigger by the change feed. Then the Azure Function will send the revised document to the Event Hub.

Right-click your project and select Manage NuGet Packages.

In the NuGet Package Manager, search for and select 
Microsoft.Azure.EventHubs, 
Microsoft.Azure.WebJobs.Extensions.CosmosDB, 
Microsoft.NET.Sdk.Functions. Select Install.

Navigate to local.settings.json in Visual Studio. 
Then use the values you recorded from your Azure Portal.


Local.setting Key values
{
    //Connection String from your Azure Storage account
    "AzureWebJobsStorage": "your Azure Storage account Connection String here",
    //Primary connection string from your Azure Cosmos DB account
    "cosmosdbconnection": "Azure Cosmos DB account Primary connection string here",
    //Event Hubs Namespace Connection string–primary key here
    "EventHubNamespaceConnectionString": "Event Hubs Namespace Connection string–primary key here"
    }