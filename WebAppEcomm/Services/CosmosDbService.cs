//***********************************************************************
//<author>Adnan Masood</author>
//***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppEcomm.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebAppEcomm.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;

    //This service does the CRUD operations. 
    //It also does read feed operations such as listing incomplete items, creating, editing, and deleting the items.
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Product productitem)
        {
            await this._container.CreateItemAsync<Product>(productitem, new PartitionKey(productitem.Item));
        }

        public async Task DeleteItemAsync(string id, string partition)
        {
            await this._container.DeleteItemAsync<Product>(id, new PartitionKey(partition));
        }

        public async Task<Product> GetItemAsync(string id, string partition)
        {
            try
            {
                Product tmp = new Product();
                tmp.id = id;

                ItemResponse<Product> response = await this._container.ReadItemAsync<Product>(id, new PartitionKey(partition));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Product>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Product>(new QueryDefinition(queryString));
            List<Product> results = new List<Product>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string productid, Product productitem)
        {
            await this._container.UpsertItemAsync<Product>(productitem, new PartitionKey(productitem.Item));
        }
    }
}
