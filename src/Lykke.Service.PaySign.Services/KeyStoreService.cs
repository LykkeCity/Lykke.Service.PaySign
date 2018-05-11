﻿using System.Collections.Generic;
using Lykke.Service.PaySign.Core.Domain.KeyInfo;
using Lykke.Service.PaySign.Core.Exceptions;
using Lykke.Service.PaySign.Core.Services;

namespace Lykke.Service.PaySign.Services
{
    public class KeyStoreService : IKeysStoreService
    {
        private readonly Dictionary<string, KeyInfo> _keyInfos;

        public KeyStoreService()
        {
            _keyInfos = new Dictionary<string, KeyInfo>();
        }

        public void Add(string name, KeyInfo keyInfo)
        {
            lock (_keyInfos)
            {
                if (!_keyInfos.TryAdd(name, keyInfo))
                    throw new KeyAlreadyExistsException(name);
            }
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
