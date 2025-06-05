using Application.Commands.DecisionMap;
using Application.Queries.DecisionMap;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DecisionMapController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DecisionMapController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDecisionMap([FromBody] CreateDecisionMapCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpDelete("delete/{dmId:guid}")]
        public async Task<IActionResult> DeleteDecisionMap(Guid dmId)
        {
            var command = new DeleteDecisionMapCommand(dmId);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }


        [HttpGet("get/{dmId:guid}")]
        public async Task<IActionResult> GetDecisionMapById(Guid dmId)
        {
            var query = new GetDecisionMapByIdQuery(dmId);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();

        }

        [HttpGet("getUserId/{userId:guid}")]
        public async Task<IActionResult> GetDecisionMapsByUserId(Guid userId)
        {
            var query = new GetDecisionMapsByUserIdQuery(userId);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();
        }


        [HttpPost("project/")]
        public async Task<IActionResult> AddProjectId([FromBody] UpsertProjectQrsCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpGet("projectget/{projectId}")]
        public async Task<IActionResult> GetProjectQr(Guid projectId)
        {
            var query = new GetProjectQrsQuery(projectId);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : BadRequest();
        }

    }
}
