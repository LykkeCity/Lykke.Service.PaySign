using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.PayInternal.Contract.PaymentRequest;
using Lykke.Service.PaySign.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            ILog log)
        {
            _callbackStubService = callbackStubService ?? throw new ArgumentNullException(nameof(callbackStubService));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        [HttpPost]
        [SwaggerOperation("RegisterCall")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterCall([FromBody] string data)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<PaymentRequestDetailsMessage>(data);

                await _callbackStubService.RegisterCall(message);

                return Ok();
            }
            catch (Exception e)
            {
                await _log.WriteErrorAsync(nameof(CallbackStubController), nameof(RegisterCall), e);
            }

            return StatusCode((int) HttpStatusCode.InternalServerError);
        }

        [HttpGet("/latest")]
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
                await _log.WriteErrorAsync(nameof(CallbackStubController), nameof(GetLatestCalls), e);
            }

            return StatusCode((int) HttpStatusCode.InternalServerError);
        }
    }
}
