using KaelStore.Domain.Entities;
using KaelStore.Service.DTO.Response;
using KaelStore.Service.Features.CategoryFeatures.Commands;
using KaelStore.Service.Features.CategoryFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KaelStore.Controllers.V1
{
    [Authorize]
    public class CategoryController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add(CreateCategoryCommand command)
        {
            //return Ok(await Mediator.Send(new CreateCategoryCommand));
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCategoryQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdQuery { Id = id }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteCategoryByIdCommand(id)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateCategoryCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command.WithId(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(new Response<Category>(ex.Message));
            }
        }
    }
}
