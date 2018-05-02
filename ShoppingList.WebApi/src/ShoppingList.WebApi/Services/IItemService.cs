using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingList.WebApi.Models;

namespace ShoppingList.WebApi.Services
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAll();
        Task<Item> Upsert(Item item);
        Task<bool> Remove(int id);
    }
}
