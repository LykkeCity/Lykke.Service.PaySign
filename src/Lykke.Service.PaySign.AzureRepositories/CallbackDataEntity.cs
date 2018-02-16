using System;
using Common;
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
                return DateTime.UtcNow.ToIsoDateTime();
            }

            public static CallbackDataEntity Create(ICallbackData src)
            {
                return new CallbackDataEntity
                {
                    PartitionKey = GeneratePartitionKey(),
                    RowKey = GenerateRowKey(),
                    MerchantId = src.MerchantId,
                    PaymentRequestId = src.PaymentRequestId,
                    Info = src.Info
                };
            }
        }

        public string MerchantId { get; set; }

        public string PaymentRequestId { get; set; }

        public string Info { get; set; }
    }
}
