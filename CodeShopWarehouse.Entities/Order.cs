namespace CodeShopWarehouse.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public bool Processed { get; set; }
	}
}
