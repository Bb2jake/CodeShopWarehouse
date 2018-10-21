using CodeShopWarehouse.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShopWarehouse.Data
{
	public interface IOrdersRepo
	{
		Task<IEnumerable<Order>> Get();
		Task<Order> Get(int id);
		Task<IEnumerable<Order>> GetByProductId(int productId);
		Task Post(Order order);
		Task Put(int id, Order order);
		Task Delete(int id, Order order);
	}

	public class OrdersRepo : IOrdersRepo
	{
		private readonly CodeShopWarehouseContext _db;

		public OrdersRepo(CodeShopWarehouseContext db)
		{
			_db = db;
		}

		public async Task<IEnumerable<Order>> Get()
		{
			return await _db.Orders.Where(o => o.ProcessedAt == null).ToListAsync();
		}

		public async Task<Order> Get(int id)
		{
			return await _db.Orders.AsNoTracking().SingleOrDefaultAsync(o => o.Id == id);
		}

		public async Task<IEnumerable<Order>> GetByProductId(int productId)
		{
			return await _db.Orders.Where(o => o.ProductId == productId).ToListAsync();
		}

		public async Task Post(Order order)
		{
			_db.Orders.Add(order);
			await _db.SaveChangesAsync();
		}

		public async Task Put(int id, Order order)
		{
			_db.Orders.Update(order);
			await _db.SaveChangesAsync();
		}

		public async Task Delete(int id, Order order)
		{
			_db.Orders.Remove(order);
			await _db.SaveChangesAsync();
		}
	}
}
