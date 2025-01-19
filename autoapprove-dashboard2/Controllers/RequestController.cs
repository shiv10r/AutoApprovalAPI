using autoapprove_dashboard2.Interfaces;
using autoapprove_dashboard2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace autoapprove_dashboard2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            var requests = await _requestService.GetRequestsAsync();
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _requestService.GetRequestByIdAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            await _requestService.AddRequestAsync(request);
            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            await _requestService.UpdateRequestAsync(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            await _requestService.DeleteRequestAsync(id);
            return NoContent();
        }

        [HttpPost("auto-approve-all")]
        public async Task<IActionResult> AutoApproveAllRequests()
        {
            await _requestService.AutoApproveAllRequests();
            return NoContent();
        }

        [HttpPost("auto-reject-all")]
        public async Task<IActionResult> AutoRejectAllRequests()
        {
            await _requestService.AutoRejectAllRequests();
            return NoContent();
        }
    }
}
