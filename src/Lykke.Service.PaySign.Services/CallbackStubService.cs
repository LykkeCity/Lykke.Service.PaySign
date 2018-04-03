using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Service.PaySign.Core.Domain;
using Lykke.Service.PaySign.Core.Repositories;
using Lykke.Service.PaySign.Core.Services;

namespace Lykke.Service.PaySign.Services
{
    public class CallbackStubService : ICallbackStubService
    {
        private readonly ICallbackDataRepository _callbackDataRepository;

        public CallbackStubService(ICallbackDataRepository callbackDataRepository)
        {
            _callbackDataRepository =
                callbackDataRepository ?? throw new ArgumentNullException(nameof(callbackDataRepository));
        }

        public async Task RegisterCall(string data)
        {
            await _callbackDataRepository.InsertAsync(new CallbackData
            {
                Info = data,
                CreatedOn = DateTime.UtcNow
            });
        }

        public async Task<IReadOnlyList<string>> GetLatestCalls()
        {
            IReadOnlyList<ICallbackData> records = await _callbackDataRepository.GetLatestAsync();

            return records.Select(x => x.Info).ToList();
        }
    }
}
