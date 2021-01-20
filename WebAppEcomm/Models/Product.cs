//***********************************************************************
//<author>Adnan Masood</author>
//***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//The Model classes represents domain-specific data and business logic in the MVC application. 
//It represents the shape of the data as public properties and business logic as methods

namespace WebAppEcomm.Models
{
    using Newtonsoft.Json;

    public class Product
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "Item")]
        public string Item { get; set; }

        [JsonProperty(PropertyName = "Price")]
        public double UnitPrice { get; set; }

        [JsonProperty(PropertyName = "CartID")]
        public int CartID { get; set; }

        [JsonProperty(PropertyName = "Action")]
        public string Action { get; set; }


    }
       
   
}