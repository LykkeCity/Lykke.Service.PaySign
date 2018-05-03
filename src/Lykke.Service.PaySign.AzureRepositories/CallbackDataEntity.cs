using System;
using Lykke.AzureStorage.Tables;
using Lykke.Service.PaySign.Core.Domain;

namespace Lykke.Service.PaySign.AzureRepositories
{
    public class CallbackDataEntity : AzureTableEntity, ICallbackData
    {
        public static class ByDefault
        {
            public static string GeneratePartitionKey()
            {
                return "Default";
            }

            public static string GenerateRowKey()
            {
                return Guid.NewGuid().ToString();
            }

            public static CallbackDataEntity Create(ICallbackData src)
            {
                return new CallbackDataEntity
                {
                    PartitionKey = GeneratePartitionKey(),
                    RowKey = GenerateRowKey(),
                    MerchantId = src.MerchantId,
                    PaymentRequestId = src.PaymentRequestId,
                    CreatedOn = src.CreatedOn,
                    Info = src.Info,
                    Headers = src.Headers
                };
            }
        }

        public string MerchantId { get; set; }

        public string PaymentRequestId { get; set; }

        public string Info { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Headers { get; set; }
    }
}
