using System.Collections.Concurrent;
using System.Collections.Generic;
using Lykke.Service.PaySign.Core.Domain.KeyInfo;
using Lykke.Service.PaySign.Core.Services;

namespace Lykke.Service.PaySign.Services
{
    public class KeyStoreService : IKeysStoreService
    {
        private readonly ConcurrentDictionary<string, KeyInfo> _keyInfos;

        public KeyStoreService()
        {
            _keyInfos = new ConcurrentDictionary<string, KeyInfo>();
        }

        public bool Add(string name, KeyInfo keyInfo)
        {
            return _keyInfos.TryAdd(name, keyInfo);
        }

        public KeyInfo Get(string name)
        {
            if (_keyInfos.TryGetValue(name, out var keyInfo))
            {
                return keyInfo;
            }

            return null;
        }

        public IEnumerable<string> GetKeys()
        {
            return _keyInfos.Keys;
        }
    }
}
