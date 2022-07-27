using KaelStore.Domain.Entities;
using KaelStore.Service.DTO.Response;
using KaelStore.Service.Features.ProductFeatures.Command;
using KaelStore.Service.Features.ProductFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KaelStore.Controllers.V1
{
    [Authorize]
    public class ProductController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var productList = await Mediator.Send(new GetAllProductQuery());
                return Ok(new Response<IEnumerable<Product>>(productList));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<IEnumerable<Product>>(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetProductByIdQuery(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Product>(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Product>(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateProductCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command.WithId(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Product>(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteProductCommand(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Product>(ex.Message));
            }
        }
    }
}
