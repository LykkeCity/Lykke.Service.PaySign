using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureStorage;
using Lykke.Service.PaySign.Core.Domain;
using Lykke.Service.PaySign.Core.Repositories;

namespace Lykke.Service.PaySign.AzureRepositories
{
    public class CallbackDataRepository : ICallbackDataRepository
    {
        private readonly INoSQLTableStorage<CallbackDataEntity> _tableStorage;

        private const int LatestRecordsCount = 10;

        public CallbackDataRepository(INoSQLTableStorage<CallbackDataEntity> tableStorage)
        {
            _tableStorage = tableStorage ?? throw new ArgumentNullException(nameof(tableStorage));
        }

        public async Task<IReadOnlyList<ICallbackData>> GetLatestAsync()
        {
            IEnumerable<CallbackDataEntity> records =
                await _tableStorage.GetDataAsync(CallbackDataEntity.ByDefault.GeneratePartitionKey());

            return records
                .OrderByDescending(x => x.RowKey)
                .Take(LatestRecordsCount)
                .ToList();
        }

        public async Task<ICallbackData> InsertAsync(ICallbackData src)
        {
            CallbackDataEntity newItem = CallbackDataEntity.ByDefault.Create(src);

            await _tableStorage.InsertAsync(newItem);

            return newItem;
        }
    }
}
