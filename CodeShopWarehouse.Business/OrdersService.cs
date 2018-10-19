using CodeShopWarehouse.Data;
using CodeShopWarehouse.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShopWarehouse.Business
{
	public interface IOrdersService
	{
		Task<IEnumerable<Order>> Get();
		Task<Order> Get(int id);
		Task Post(Order order);
		Task Put(int id, Order order);
		Task Delete(int id, Order order);
	}

	public class OrdersService : IOrdersService
	{
		private readonly OrdersRepo _repo;

		public OrdersService(OrdersRepo repo)
		{
			_repo = repo;
		}

		public async Task<IEnumerable<Order>> Get()
		{
			return await _repo.Get();
		}

		public async Task<Order> Get(int id)
		{
			return await _repo.Get(id);
		}

		// TODO: Change to uploadDto
		public async Task Post(Order order)
		{
			await _repo.Post(order);
		}

		public async Task Put(int id, Order order)
		{
			var dbOrder = await _repo.Get(id);
			if (dbOrder == null) throw new Exception($"Order with id: {id} does not exist!");
			if (dbOrder.Processed) throw new Exception("Cannot update an order that has already been processed!");

			await _repo.Put(id, order);
		}

		public async Task Delete(int id, Order order)
		{
			var dbOrder = await _repo.Get(id);
			if (dbOrder == null) throw new Exception($"Order with id: {id} does not exist!");
			if (dbOrder.Processed) throw new Exception("Cannot delete an order that has already been processed!");

			await _repo.Delete(id, order);
		}

	}
}
