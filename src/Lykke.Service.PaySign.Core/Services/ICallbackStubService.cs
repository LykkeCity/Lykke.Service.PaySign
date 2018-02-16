using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.PayInternal.Contract.PaymentRequest;

namespace Lykke.Service.PaySign.Core.Services
{
    public interface ICallbackStubService
    {
        Task RegisterCall(PaymentRequestDetailsMessage data);
        Task<IReadOnlyList<string>> GetLatestCalls();
    }
}
