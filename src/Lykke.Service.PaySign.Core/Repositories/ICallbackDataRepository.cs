using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.PaySign.Core.Domain;

namespace Lykke.Service.PaySign.Core.Repositories
{
    public interface ICallbackDataRepository
    {
        Task<IReadOnlyList<ICallbackData>> GetLatestAsync();

        Task<ICallbackData> InsertAsync(ICallbackData src);
    }
}
