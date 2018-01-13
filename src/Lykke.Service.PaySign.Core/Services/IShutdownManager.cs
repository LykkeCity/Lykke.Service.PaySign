using System.Threading.Tasks;

namespace Lykke.Service.PaySign.Core.Services
{
    public interface IShutdownManager
    {
        Task StopAsync();
    }
}