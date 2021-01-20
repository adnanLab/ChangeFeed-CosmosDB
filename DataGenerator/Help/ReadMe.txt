
On this project DataGenerator simulated data into Cosmos DB in real time, you will create randomized data for a grocery 
store and inserts it into a Cosmos DB collection to Azure.

Right-click your project and select Manage NuGet Packages.

In the NuGet Package Manager, search for and select 
Microsoft.Azure.DocumentDB.Core, 
System.Configuration.ConfigurationManager, 
NUnit. Select Install.

App.config setting should be like this :-

  <appSettings>
   <add key="endpoint" value="your Azure Cosmos DB account URI endpoint here" />
    <add key="authKey" value="your Azure Cosmos DB account PRIMARY KEY here" />
    <add key="database" value="your Azure Cosmos database name here" />
    <add key="collection" value="your Azure Cosmos collection name here" />
  </appSettings>
