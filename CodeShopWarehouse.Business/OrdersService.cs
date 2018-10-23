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
		Task<IEnumerable<Order>> GetByProductId(int productId);
		Task Post(OrderUploadDto orderDto);
		Task Put(int id, Order order);
		Task Delete(int id, Order order);
	}

	public class OrdersService : IOrdersService
	{
		private readonly IOrdersRepo _repo;

		public OrdersService(IOrdersRepo repo)
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

		public async Task<IEnumerable<Order>> GetByProductId(int productId)
		{
			return await _repo.GetByProductId(productId);
		}

		public async Task Post(OrderUploadDto orderDto)
		{
			var order = new Order
			{
				CreatedAt = DateTimeOffset.Now,
				ProductId = orderDto.ProductId,
				Quantity = orderDto.Quantity
			};

			await _repo.Post(order);
		}

		public async Task Put(int id, Order order)
		{
			var dbOrder = await _repo.Get(id);
			if (dbOrder == null) throw new Exception($"Order with id: {id} does not exist!");
			if (dbOrder.ProcessedAt != null) throw new Exception("Cannot update an order that has already been processed!");

			await _repo.Put(id, order);
		}

		public async Task Delete(int id, Order order)
		{
			var dbOrder = await _repo.Get(id);
			if (dbOrder == null) throw new Exception($"Order with id: {id} does not exist!");
			if (dbOrder.ProcessedAt != null) throw new Exception("Cannot delete an order that has already been processed!");

			await _repo.Delete(id, order);
		}

	}
}
