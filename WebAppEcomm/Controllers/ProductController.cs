//***********************************************************************
//<author>Adnan Masood</author>
//***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAppEcomm.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WebAppEcomm.Models;

    public class ProductController : Controller
    {
        private readonly Services.ICosmosDbService _cosmosDbService;
        public ProductController(Services.ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _cosmosDbService.GetItemsAsync("SELECT * FROM c"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Item,Action,UnitPrice,CartID")] Product productitem)
        {
            if (ModelState.IsValid)
            {
                productitem.id = Guid.NewGuid().ToString();
                await _cosmosDbService.AddItemAsync(productitem);
                return RedirectToAction("Index");
            }

            return View(productitem);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("id,Item,Action,UnitPrice,CartID")] Product productitem)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.UpdateItemAsync(productitem.Item, productitem);
                return RedirectToAction("Index");
            }

            return View(productitem);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            string[] _data = id.Split('|');
            if (id == null)
            {
                return BadRequest();
            }

            Product item = await _cosmosDbService.GetItemAsync(_data[0],_data[1]);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            string[] _data = id.Split('|');
            if (id == null)
            {
                return BadRequest();
            }

            Product productitem = await _cosmosDbService.GetItemAsync(_data[0], _data[1]);
            if (productitem == null)
            {
                return NotFound();
            }

            return View(productitem);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("id")] string id)
        {
            string[] _data = id.Split('|');
            await _cosmosDbService.DeleteItemAsync(_data[0], _data[1]);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            string[] _data = id.Split('|');
            return View(await _cosmosDbService.GetItemAsync(_data[0], _data[1]));
        }
    }
}