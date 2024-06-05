using Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Contexts;
using Ernesto.Sanchez.OrderService.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ernesto.Sanchez.OrderService.Domain.Orders.Interfaces;


namespace Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly OrderContext _context;
        public ItemRepository(
            OrderContext context
        )
        {
            _context = context;
        }
        public async Task AddItemForOrderAsync(Guid orderId, Item item)
        {
            item.OrderId = orderId;
            await _context.Items.AddAsync(item);
        }
        public async Task DeleteItemAsync(Item item)
        {
            await Task.FromResult(_context.Items.Remove(item));
        }
        public async Task UpdateItemForOrderAsync(Item item)
        {
            Item itemUpdate = await GetItemForOrderAsync(item.OrderId, item.ItemId);
            if (itemUpdate != null)
            {
                itemUpdate.DescriptionProduct = item.DescriptionProduct;
                itemUpdate.Cant = item.Cant;
            }
        }
        public async Task<IEnumerable<Item>> GetItemsForOrderAsync(Guid orderId)
        {
            return await _context.Items.Where(b => b.OrderId == orderId).OrderBy(b => b.DescriptionProduct).ToListAsync();
        }
        public async Task<Item> GetItemForOrderAsync(Guid orderId, Guid itemId)
        {
            return await _context.Items.Where(b => b.OrderId == orderId && b.ItemId == itemId).FirstOrDefaultAsync();
        }

        public Task<Item> UpdateItemAsync(Item item)
        {
            _context.Items.Update(item);
            return Task.FromResult(item);
        }
    }
}
