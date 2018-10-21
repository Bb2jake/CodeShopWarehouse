using System;
using System.Linq;
using System.Threading.Tasks;
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

		private async Task<Order> CreateAndAddNewOrder(bool processed, int? productId)
		{
			var order = GetNewOrder(processed, productId);
			Context.Orders.Add(order);
			await Context.SaveChangesAsync();
			return order;
		}

		[Fact]
		private async void UnresolvedFillOrdersCanBeRetrieved()
		{
			for (var i = 0; i < 100; i++)
			{
				Context.Orders.Add(GetNewOrder(i % 2 == 0, null));
			}

			await Context.SaveChangesAsync();

			var orders = await Service.Get();
			Assert.Equal(50, orders.Count());
		}

		[Fact]
		public async void UpdatingProcessedOrderThrowsException()
		{
			var order = await CreateAndAddNewOrder(true, null);

			await Assert.ThrowsAsync<Exception>(async () => await Service.Put(order.Id, order));
		}

		[Fact]
		public async void UpdatingOrderWorksCorrectly()
		{
			var order = await CreateAndAddNewOrder(false, null);
			
			SetContextToNull();

			order.ProcessedAt = DateTimeOffset.Now;
			await Service.Put(order.Id, order);

			var dbOrder = await Context.Orders.FindAsync(order.Id);
			Assert.True(dbOrder.ProcessedAt != null);
		}

		[Fact]
		public async void DeletingProcessedOrderThrowsException()
		{
			var order = await CreateAndAddNewOrder(true, null);

			await Assert.ThrowsAsync<Exception>(async () => await Service.Delete(order.Id, order));
		}

		[Fact]
		public async void DeletingOrderWorksCorrectly()
		{
			var order = await CreateAndAddNewOrder(false, null);

			await Service.Delete(order.Id, order);

			var dbOrder = await Context.Orders.FindAsync(order.Id);
			Assert.Null(dbOrder);
		}
	}
}
