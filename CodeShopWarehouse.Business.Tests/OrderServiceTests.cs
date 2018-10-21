using System;
using CodeShopWarehouse.Data;
using CodeShopWarehouse.Entities;
using Xunit;

namespace CodeShopWarehouse.Business.Tests
{
	public class OrderServiceTests : BaseTestClass
	{
		private OrdersRepo _repo;
		private OrdersRepo Repo => _repo ?? (_repo = new OrdersRepo(Context));
		private OrdersService _service;
		private OrdersService Service => _service ?? (_service = new OrdersService(Repo));

		private Order GetNewOrder(bool processed, int? productId)
		{
			var rnd = new Random();
			return new Order
			{
				Id = rnd.Next(100000),
				ProcessedAt = processed ? (DateTimeOffset?)DateTimeOffset.Now : null,
				CreatedAt = DateTimeOffset.Now,
				ProductId = productId ?? rnd.Next(100000)
			};
		}

		[Fact]
		public async void UpdatingProcessedOrderThrowsException()
		{
			var order = GetNewOrder(true, null);
			Context.Orders.Add(order);
			await Context.SaveChangesAsync();

			await Assert.ThrowsAsync<Exception>(async () => await Service.Put(order.Id, order));
		}
	}
}
