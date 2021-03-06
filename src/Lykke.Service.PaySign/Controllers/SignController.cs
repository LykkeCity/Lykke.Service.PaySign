﻿using System;
using System.Net;
using Common;
using Common.Log;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Common.Log;
using Lykke.Service.PaySign.Core.Exceptions;
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
            ILogFactory logFactory)
        {
            _signService = signService ?? throw new ArgumentNullException(nameof(signService));
            _log = logFactory?.CreateLog(this) ?? throw new ArgumentNullException(nameof(logFactory));
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
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.InternalServerError)]
        public IActionResult Sign(string keyName, [FromBody] string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return BadRequest(ErrorResponse.Create("Content to sign can't be empty"));

            try
            {
                var result = _signService.Sign(content, keyName);

                return Ok(result);
            }
            catch (KeyNotFoundException keyEx)
            {
                _log.Error(keyEx, null, new {keyEx.KeyName}.ToJson());

                return BadRequest(ErrorResponse.Create(keyEx.Message));
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }

            return StatusCode((int) HttpStatusCode.InternalServerError);
        }
    }
}
