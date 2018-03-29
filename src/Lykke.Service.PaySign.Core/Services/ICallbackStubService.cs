using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.PaySign.Core.Services
{
    public interface ICallbackStubService
    {
        Task RegisterCall(string data);
        Task<IReadOnlyList<string>> GetLatestCalls();
    }
}
