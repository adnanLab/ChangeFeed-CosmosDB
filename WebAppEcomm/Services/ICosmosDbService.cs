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
    using System.Threading.Tasks;
    using WebAppEcomm.Models;

    public interface ICosmosDbService
    {
        Task<IEnumerable<Product>> GetItemsAsync(string query);
        Task<Product> GetItemAsync(string productid,string partition);//<Product> string productid, Product productitem
        Task AddItemAsync(Product productitem);
        Task UpdateItemAsync(string productid, Product productitem);
        Task DeleteItemAsync(string id, string partition);
    }
}