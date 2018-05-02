using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingList.WebApi.Models;
using ShoppingList.WebApi.Repositories;

namespace ShoppingList.WebApi.Services
{
    public sealed class ItemService : IItemService
    {
        private readonly ShoppingListDbContext _context;

        public ItemService(ShoppingListDbContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<Item>> IItemService.GetAll()
        {
            return await _context.Items.ToListAsync();
        }

        async Task<bool> IItemService.Remove(int id)
        {
            var item = await _context.Items.SingleOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return false;
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        async Task<Item> IItemService.Upsert(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var existingItem = await _context.Items.SingleOrDefaultAsync(x => x.Id == item.Id);

            Item newItem;

            if (existingItem == null)
            {
                newItem = (await _context.Items.AddAsync(item)).Entity;
            }
            else
            {
                existingItem.Name = item.Name;
                newItem = existingItem;
            }

            await _context.SaveChangesAsync();

            return newItem;
        }
    }
}
