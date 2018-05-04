using System;

namespace Lykke.Service.PaySign.Core.Domain
{
    public class CallbackData : ICallbackData
    {
        public string MerchantId { get; set; }
        public string PaymentRequestId { get; set; }
        public string Info { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Headers { get; set; }
    }
}
