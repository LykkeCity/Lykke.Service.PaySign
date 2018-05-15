using System.Collections.Generic;
using Lykke.Service.PaySign.Core.Domain.KeyInfo;

namespace Lykke.Service.PaySign.Core.Services
{
    public interface IKeysStoreService
    {
        bool Add(string name, KeyInfo keyInfo);
        KeyInfo Get(string name);
        IEnumerable<string> GetKeys();
    }
}
