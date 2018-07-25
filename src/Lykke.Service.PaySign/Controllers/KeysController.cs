using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.Log;
using Lykke.Service.PaySign.Core.Domain.KeyInfo;
using Lykke.Service.PaySign.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.PaySign.Controllers
{
    [Route("api/[controller]")]
    public class KeysController : Controller
    {
        private readonly IKeysStoreService _keysStoreService;
        private readonly ILog _log;

        public KeysController(
            IKeysStoreService keysStoreService,
            ILogFactory logFactory)
        {
            _keysStoreService = keysStoreService ?? throw new ArgumentNullException(nameof(keysStoreService));
            _log = logFactory?.CreateLog(this) ?? throw new ArgumentNullException(nameof(logFactory));
        }

        /// <summary>
        /// Getting all registered key names
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [SwaggerOperation("GetKeys")]
        [ProducesResponseType(typeof(IEnumerable<string>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public IActionResult GetKeys()
        {
            try
            {
                return Ok(_keysStoreService.GetKeys());
            }
            catch (Exception e)
            {
                _log.Error(e);
            }

            return StatusCode((int) HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Register private key
        /// </summary>
        /// <param name="file"></param>
        /// <param name="keyName"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpPost("upload/{keyName}/{apiKey}")]
        [SwaggerOperation("UploadKey")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UploadKey(IFormFile file, string keyName, string apiKey)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(ErrorResponse.Create("Empty file"));
            }

            if (_keysStoreService.Get(keyName) != null)
                return BadRequest(ErrorResponse.Create($"{keyName} already exists"));

            var fileContent = await file.OpenReadStream().ToBytesAsync();

            try
            {
                bool added = _keysStoreService.Add(keyName, new KeyInfo
                {
                    ApiKey = apiKey,
                    PrivateKey = Encoding.UTF8.GetString(fileContent, 0, fileContent.Length)
                });

                if (added)
                    return NoContent();

                return BadRequest(ErrorResponse.Create($"Couldn't add {keyName}"));
            }
            catch (FormatException ex)
            {
                return BadRequest(ErrorResponse.Create($"Private key file has an invalid format: {ex.Message}"));
            }
            catch (Exception e)
            {
                _log.Error(e);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
