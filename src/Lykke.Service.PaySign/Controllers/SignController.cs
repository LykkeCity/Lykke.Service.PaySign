using System;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.PaySign.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.PaySign.Controllers
{
    [Route("api/[controller]")]
    public class SignController : Controller
    {
        private readonly ISignService _signService;
        private readonly ILog _log;

        public SignController(
            ISignService signService,
            ILog log)
        {
            _signService = signService ?? throw new ArgumentNullException(nameof(signService));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        /// <summary>
        /// Sign content
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost("{keyName}")]
        [SwaggerOperation("Sign")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Sign(string keyName, [FromBody] string content)
        {
            try
            {
                var result = _signService.Sign(content, keyName);

                return Ok(result);
            }
            catch (Exception e)
            {
                await _log.WriteErrorAsync(nameof(SignController), nameof(Sign), e);
            }

            return StatusCode((int) HttpStatusCode.InternalServerError);
        }
    }
}
