# ChangeFeed-CosmosDB

The change feeds feature to understand user patterns with real-time data analysis visualization. Approaching change feed from an e-commerce company's perspective and work with a collection of events that we will capture when a user views an item, adds an item to their cart, or makes any purchases—triggering a series of steps. Resulting in the visualization of metrics analyzing company performance and site activities.

Prerequisites
64-bit Windows 10 Operating System
Microsoft .NET Framework 4.8
Microsoft .NET Core 3.1
Visual Studio community 2019
Microsoft Azure Subscription
Microsoft Power BI Account


Please refer to the youtube video here:- (************). How to setup these steps.

Step 1: Creating a database and collection to load data.
Step 2: Creating a leases collection.
Step 3: Creating a storage account & save key and connection string.
Step 4: Creating event hub namespace connection string.
Step 5: Setting Up Azure function with Cosmos DB account.
Step 6: Inserting random data into Cosmos DB in Real-Time.
Step 7: Setting Up Azure Stream Analytics and Data Analysis Visualization.
Step 8: Connection to PowerBI.

Change Feed Function set up:-
Right-click the file named WebAppEcommChangeFeedFunction.sln and select Open With Visual Studio.
Navigate to local.settings.json in Visual Studio. Then use the save values from key.txt file to fill in the blanks.
Execute the function.
Random data generation:-
Right-click the file named DataGenerator.sln and select Open With Visual Studio.
Navigate to App.config in Visual Studio. Then use the save values from key.txt file to fill in the blanks.
Execute the application.

E-commerce site with CRUD functionality:-
Right-click the file named WebAppEcomm.sln and select Open With Visual Studio.
Navigate to appsettings.json in Visual Studio. Then use the save values from key.txt file to fill in the blanks.
Execute the WebAppEcomm.
