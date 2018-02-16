namespace Lykke.Service.PaySign.Core.Domain
{
    public class CallbackData : ICallbackData
    {
        public string MerchantId { get; set; }
        public string PaymentRequestId { get; set; }
        public string Info { get; set; }
    }
}
