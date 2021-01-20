//***********************************************************************
//<author>Adnan Masood</author>
//***********************************************************************
using System;

namespace DataGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using NUnit.Framework.Internal;
    using Microsoft.Azure.Documents.Client;


    
    //Class which contains code for creating randomized data for a grocery 
    //store and inserts it into a Cosmos DB collection to Azure.
    
    public class Program
    {
        
        //Instance of the Cosmos DB client that access the service.
        private static readonly DocumentClient Client = new DocumentClient(
            new Uri(ConfigurationManager.AppSettings["endpoint"]),
            ConfigurationManager.AppSettings["authKey"],
            new ConnectionPolicy()
            {
                UserAgentSuffix = " samples-net/3",
                ConnectionMode = ConnectionMode.Direct,
                ConnectionProtocol = Protocol.Tcp
            });

        
        //Initializing Uri for the Cosmos DB collection.
        
        private static readonly Uri CollectionUri = UriFactory.CreateDocumentCollectionUri(
            ConfigurationManager.AppSettings["database"],
            ConfigurationManager.AppSettings["collection"]);

        
        //Contains the valid actions a user can take.
        
        public enum Action
        {
                  
            //User has purchased this product.
            Purchased,
            
            //User has viewed this product.
            Viewed,

            //User has added this product to cart. 
             Added

        }


        //Main method that calls DataCreation().
        // <param name="args"> Default main arguments. </param>
        public static void Main(string[] args)
        {
            DataCreation();
            Console.ReadKey();
            return;
        }

        
        //Method that creates randomized data by generating a random number for the CartID, selecting a 
        //random item from the list of items, and matching it with a random Action from GetRandomAction.
        
        public static async void DataCreation()
        {
            Randomizer random = new Randomizer();
            string[] items = new string[]
            {
                "Springdale Vitamin D Whole Milk", "Planet Oat Original Oatmilk", "Similac Infant Formula", "Promised Land Dairy Very Berry Strawberry Whole Milk",
                "Silk Dairy Free Vanilla Yogurt ", "Activia Lowfat Probiotic", "Chobani Vanilla Blended Non-Fat Greek Yogurt", "Siggi's Icelandic Style Skyr Non-Fat Yogurt",
                "Simple Truth Organic Strained Plain Nonfat Greek Yogurt", "KozyShack Original Recipe Chocolate Pudding", "Kozyshack Rice Pudding, Gluten Free", "KozyShack Tapioca Pudding",
                "Vita Coco Coconut Water, Pure", "Welch's 100% Concord Grape Juice", "Vita Coco Coconut Water, Pure", "Welch's 100% Concord Grape Juice",
                "MiO Lemonade Liquid Water Enhancer", "Pedialyte Electrolyte Powder Strawberry Lemonade", "Red Bull Energy Drink, Sugar Free", "BodyArmor Sports Drink, Berry Punch, Lyte, 8 Pack",
                "Sprout Baked Toddler Snack, Organic, Broccoli", "Pedialyte Electrolyte Solution", "Sprout Baked Toddler Snack, Organic, Broccoli", "Ensure High Protein Nutrition Shake Strawberry",
                "Desitin Rapid Relief Cream", "One A Day Kids Dragons Complete", "Kraft EZ Mac Cup 18", "Kraft ABC Mac 20 Cups",
                "One A Day for Her VitaCraves Teen Multi Gummies", "One a Day Women's VitaCraves Multivitamin Gummies", "Vital Proteins Collagen Peptides, Unflavored", "Women's Pant", "Sister Schubert's Sausage Wrap Rolls", "Kellogg's Eggo Minis Frozen Waffles Cinnamon Toast",
                "Benadryl Allergy Dye-Free Liqui-Gels", "Applegate Natural Chicken & Apple Breakfast Sausage", "Daiya Pizza, Gluten-Free, Meatless Meat Lover's, Thin Crust", "Kraft EZ Mac Cup 8", "Smart Ones Tasty American Favorites", "Califlour Foods Cauliflower Pizza Crust, Sweet Red Pepper",
                "Zyrtec Children's Children’s Zyrtec", "Annie's Homegrown Macaroni & Cheese", "Applegate Organic Chicken Patties", "Califlour Foods Cauliflower Pizza Crust, Sweet Red Pepper", "Kraft EZ Mac Cup", "Private Selection Five Cheese & Marmalade Thin Crust Pizza",
                "Claritin Loratadine, 10 mg, Chewable Tablets", "Kraft EZ Mac Pure 4 Cups", "Just Egg, Plant-Based Scramble"
            };

            double[] prices = new double[]
            {
               3.75, 6.40, 11.00, 9.10,
                18.00, 21.00, 15.00, 16.80,
                9.90, 22.10, 25.00, 28.00, 24.80,
                22.60, 32.00, 30.50, 47.98, 38.10,
                55.00, 50.00, 65.00, 31.99, 49.00,
                22.00, 33.99, 17.09, 80.00, 95.90,
                90.00, 33.00, 25.20, 40.00, 87.50, 99.99,
                15.99, 66.00, 70.00, 65.00, 92.00, 95.00,
                51.20, 25.00, 120.00, 105.00, 2.00, 8.99,
                44.89, 15.00, 25.50, 68.99, 1.00, 2.50, 1.10, 5.21, 8.00, 2.89
            };

            bool loop = true;
            while (loop)
            {
                int itemIndex = random.Next(0, items.Length - 1);
                Event e = new Event()
                {
                    CartID = random.Next(1000, 9999),
                    Action = random.NextEnum<Action>(),
                    Item = items[itemIndex],
                    Price = prices[itemIndex]
                };
                await InsertData(e);

                List<Action> previousActions = new List<Action>();
                switch (e.Action)
                {
                    case Action.Viewed:
                        break;
                    case Action.Added:
                        previousActions.Add(Action.Viewed);
                        break;
                    case Action.Purchased:
                        previousActions.Add(Action.Viewed);
                        previousActions.Add(Action.Added);
                        break;
                    default:
                        break;
                }

                foreach (Action previousAction in previousActions)
                {
                    Event previousEvent = new Event()
                    {
                        CartID = e.CartID,
                        Action = previousAction,
                        Item = e.Item,
                        Price = e.Price
                    };
                    await InsertData(previousEvent);
                }
            }

            string key = Console.ReadKey().Key.ToString();
            if (key == " ")
            {
                loop = false;
            }
            else
            {
                loop = true;
            }

            DataCreation();
        }

        
        //Inserts each event  to the database by using Azure DocumentClient library.
        
        //<param name="e"> An instance of the Event class representing a user click. </param>/
        /// <returns>returns a Task</returns>        
        private static async Task InsertData(Event e)
        {
            await Client.CreateDocumentAsync(CollectionUri, e);
            Console.Write("* New Entry Added * |");
        }

        
        //Class that defines the parameters of an event a users can make.
        
        internal class Event
        {
            //Gets or sets item from the item list.
             public string Item { get; set; }
            
            //Gets or sets price associated with each Item by index from the price list.
             public double Price { get; set; }

            //Gets or sets an ID to represent the user that is shopping.
              public int CartID { get; set; }

            //Gets or sets action from the action list.
            [JsonConverter(typeof(StringEnumConverter))]
            public Action Action { get; set; }

        }
    }
}

