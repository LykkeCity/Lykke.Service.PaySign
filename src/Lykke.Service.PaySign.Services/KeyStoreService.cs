using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.PaySign.Core;
using Lykke.Service.PaySign.Core.Domain.KeyInfo;
using Lykke.Service.PaySign.Core.Services;

namespace Lykke.Service.PaySign.Services
{
    public class KeyStoreService : IKeysStoreService
    {
        private readonly ConcurrentDictionary<string, KeyInfo> _keyInfos;
        private readonly ILog _log;

        public KeyStoreService(ILogFactory logFactory)
        {
            _keyInfos = new ConcurrentDictionary<string, KeyInfo>();
            _log = logFactory.CreateLog(this);
        }

        public bool Add(string name, KeyInfo keyInfo)
        {
            if (!ValidateKey(keyInfo.PrivateKey))
                return false;

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

        private bool ValidateKey(string privateKey)
        {
            try
            {
                var rsa = privateKey.CreateRsa();
                return true;
            }
            catch (FormatException ex)
            {
                _log.Warning("Try to upload key in invalid format.", ex);
                throw;
            }
            catch (Exception ex)
            {
                _log.Warning("Try to upload an invalid key.", ex);
                return false;
            }
        }
    }
}
