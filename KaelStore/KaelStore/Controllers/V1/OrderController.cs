using KaelStore.Service.DTO.Order;
using KaelStore.Service.DTO.Response;
using KaelStore.Service.Features.OrderFeatures.Commands;
using KaelStore.Service.Features.OrderFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KaelStore.Controllers.V1
{
    [Authorize]
    public class OrderController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add(CreateOrderModel request)
        {
            try
            {
                var createOrderResponse = await Mediator.Send(new CreateOrderCommand(request));
                return Ok(createOrderResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>(ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllOrderQuery()));
        }
    }
}
