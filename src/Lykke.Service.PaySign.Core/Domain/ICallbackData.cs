using JetBrains.Annotations;

namespace Lykke.Service.PaySign.Core.Domain
{
    public interface ICallbackData
    {
        [CanBeNull] string MerchantId { get; set; }
        [CanBeNull] string PaymentRequestId { get; set; }
        string Info { get; set; }
    }
}
