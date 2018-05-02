using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.WebApi.Models;
using ShoppingList.WebApi.Services;

namespace ShoppingList.WebApi.Controllers
{
    [Route("/api/v1/items")]
    public sealed class ItemsController : Controller
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _itemService.GetAll();
        }

        [HttpPost]
        public async Task<Item> Upsert([FromBody]Item item)
        {
            if (item == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "No item provided");
            }

            return await _itemService.Upsert(item);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _itemService.Remove(id))
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Item not found");
            }

            return NoContent();
        }
    }
}
