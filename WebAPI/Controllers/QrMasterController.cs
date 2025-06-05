using Application.Commands.QrMaster;
using Application.Queries.QrMaster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QrMasterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QrMasterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddQrMaster([FromBody] AddQrCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateQrMaster([FromBody] UpdateQrCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteQrMaster([FromBody] DeleteQrCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetQrMaster()
        {
            var command = new GetAllQrMasterQuery();
            var result = await _mediator.Send(command);

            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("get-gr/{qrId:guid}")]
        public async Task<IActionResult> GetProductById(Guid qrId)
        {
            var command = new GetQrMasterByIdQuery(qrId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
