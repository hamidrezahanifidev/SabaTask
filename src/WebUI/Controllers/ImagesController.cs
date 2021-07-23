using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Images.Commands.GetImagesWithSimilarity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
    [Authorize]
    public class ImagesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ImagesVm>> Get([FromBody]string data)
        {
            if ( String.IsNullOrEmpty(data))
            {
                return BadRequest();
            }

            try
            {
                return await Mediator.Send(new GetImagesWithSimilarityQuery { Image = data });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
