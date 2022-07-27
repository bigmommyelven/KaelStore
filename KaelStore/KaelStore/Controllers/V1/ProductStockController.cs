using KaelStore.Service.Features.ProductStockFeatures.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KaelStore.Controllers.V1
{
    [Authorize]
    public class ProductStockController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add(AddProductStockCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
