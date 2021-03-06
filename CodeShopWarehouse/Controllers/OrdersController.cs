﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CodeShopWarehouse.Business;
using CodeShopWarehouse.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeShopWarehouse.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrdersService _ordersService;

		public OrdersController(IOrdersService ordersService)
		{
			_ordersService = ordersService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Order>>> Get()
		{
			return new JsonResult(await _ordersService.Get());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Order>> Get(int id)
		{
			return new JsonResult(await _ordersService.Get(id));
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] OrderUploadDto orderDto)
		{
			await _ordersService.Post(orderDto);
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromBody] Order order)
		{
			await _ordersService.Put(id, order);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id, Order order)
		{
			await _ordersService.Delete(id, order);
			return Ok();
		}
	}
}
