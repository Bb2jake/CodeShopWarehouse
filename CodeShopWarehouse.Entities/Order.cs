using System;

namespace CodeShopWarehouse.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset? ProcessedAt { get; set; }
	}
}
