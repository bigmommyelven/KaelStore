using KaelStore.Domain.Entities;
using KaelStore.Service.DTO.Customer;
using KaelStore.Service.DTO.Response;
using KaelStore.Service.Features.CustomerFeatures.Commands;
using KaelStore.Service.Features.CustomerFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KaelStore.Controllers.V1
{
    [Authorize]
    public class CustomerController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new GetCustomerByIdQuery { Id = id }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteCustomerByIdCommand { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, Customer request)
        {
            try
            {
                var customer = await Mediator.Send(new UpdateCustomerCommand(request));
                if (customer == null)
                    return NotFound(new Response<string>($"Cannot found customer with Id = {request.Id}"));

                return Ok(new Response<Customer>(customer, "Updated!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>(ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCustomerModel request)
        {
            try
            {
                var customer = await Mediator.Send(new CreateCustomerCommand(request));
                return Ok(new Response<Customer>(customer));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>(ex.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var customerList = await Mediator.Send(new GetAllCustomerQuery());
                return Ok(new Response<IEnumerable<Customer>>(customerList));

            }
            catch (Exception ex)
            {
                return BadRequest(new Response<string>(ex.Message));
            }
        }
    }
}
