using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Lykke.Service.PaySign.Core.Domain;
using Lykke.Service.PaySign.Core.Repositories;
using Lykke.Service.PaySign.Core.Services;
using Microsoft.AspNetCore.Http;

namespace Lykke.Service.PaySign.Services
{
    public class CallbackStubService : ICallbackStubService
    {
        private readonly ICallbackDataRepository _callbackDataRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CallbackStubService(
            ICallbackDataRepository callbackDataRepository, 
            IHttpContextAccessor httpContextAccessor)
        {
            _callbackDataRepository =
                callbackDataRepository ?? throw new ArgumentNullException(nameof(callbackDataRepository));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task RegisterCall(string data)
        {
            string headers = _httpContextAccessor.HttpContext?.Request?.Headers?.ToJson();

            await _callbackDataRepository.InsertAsync(new CallbackData
            {
                Info = data,
                CreatedOn = DateTime.UtcNow,
                Headers =  headers
            });
        }

        public async Task<IReadOnlyList<string>> GetLatestCalls()
        {
            IReadOnlyList<ICallbackData> records = await _callbackDataRepository.GetLatestAsync();

            return records.Select(x => x.Info).ToList();
        }
    }
}
