//***********************************************************************
//<author>Adnan Masood</author>
//***********************************************************************

using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System.Text;

namespace WebAppEcommChangeFeedFunction
{

    //Using Cosmos DB Change Feed
    public static class ChangeFeedFunction
    {
        //Your Event Hub Instance Name here
        private static readonly string EventHubName = "event-hub1";

        [FunctionName("ChangeFeedFunction")]
        public static void Run([CosmosDBTrigger(
            databaseName: "changefeeddb",
            collectionName: "changefeeddbcollection",
            ConnectionStringSetting = "cosmosdbconnection",
            //When creating leases collection make sure to give /id as partition key
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, ILogger log)
        {

            string value = Environment.GetEnvironmentVariable("EventHubNamespaceConnectionString");
            {  
                
                // Build connection string to access event hub within event hub namespace.
                EventHubsConnectionStringBuilder eventHubConnectionStringBuilder =
                    new EventHubsConnectionStringBuilder(value)
                    {
                        EntityPath = EventHubName
                    };

                //Create event hub client to send changefeed events to hub.
                EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionStringBuilder.ToString());

                //Capturing records through loop modified documents from the change feed.
                foreach (var doc in input)
                {
                    //Convert documents to json.
                    string json = JsonConvert.SerializeObject(doc);
                    EventData data = new EventData(Encoding.UTF8.GetBytes(json));

                    // Use Event Hub client to send the change events to hub.
                    eventHubClient.SendAsync(data);
                }
            }

        }
    }
}
