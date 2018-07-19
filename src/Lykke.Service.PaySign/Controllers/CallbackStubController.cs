using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.Log;
using Lykke.Service.PaySign.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.PaySign.Controllers
{
    [Route("api/[controller]")]
    public class CallbackStubController : Controller
    {
        private readonly ICallbackStubService _callbackStubService;
        private readonly ILog _log;

        public CallbackStubController(
            ICallbackStubService callbackStubService,
            ILogFactory logFactory)
        {
            _callbackStubService = callbackStubService ?? throw new ArgumentNullException(nameof(callbackStubService));
            _log = logFactory?.CreateLog(this) ?? throw new ArgumentNullException(nameof(logFactory));
        }

        [HttpPost]
        [SwaggerOperation("RegisterCall")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterCall([FromBody] string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return BadRequest(ErrorResponse.Create("Data is empty"));

            try
            {
                await _callbackStubService.RegisterCall(data);

                return Ok();
            }
            catch (Exception e)
            {
                _log.Error(e);
            }

            return StatusCode((int) HttpStatusCode.InternalServerError);
        }

        [HttpGet("latest")]
        [SwaggerOperation("GetLatestCalls")]
        [ProducesResponseType(typeof(IEnumerable<string>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetLatestCalls()
        {
            try
            {
                IReadOnlyList<string> calls = await _callbackStubService.GetLatestCalls();

                return Ok(calls);
            }
            catch (Exception e)
            {
                _log.Error(e);
            }

            return StatusCode((int) HttpStatusCode.InternalServerError);
        }
    }
}
