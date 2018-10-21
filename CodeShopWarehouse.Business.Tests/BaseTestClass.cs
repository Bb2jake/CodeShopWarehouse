using CodeShopWarehouse.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodeShopWarehouse.Business.Tests
{
	public class BaseTestClass
	{
		private CodeShopWarehouseContext _context;

		public CodeShopWarehouseContext Context => _context ?? (_context = GetInMemoryContext());

		protected void SetContextToNull()
		{
			_context = null;
		}

		protected string DataBaseName = Guid.NewGuid().ToString();
		private CodeShopWarehouseContext GetInMemoryContext()
		{
			var optionsBuilder = new DbContextOptionsBuilder<CodeShopWarehouseContext>();
			optionsBuilder.UseInMemoryDatabase(DataBaseName);
			return new CodeShopWarehouseContext(optionsBuilder.Options);
		}
	}
}