using System;
using System.Security.Cryptography;
using System.Text;
using Lykke.Service.PaySign.Core;
using Lykke.Service.PaySign.Core.Exceptions;
using Lykke.Service.PaySign.Core.Services;

namespace Lykke.Service.PaySign.Services
{
    public class SignService : ISignService
    {
        private readonly IKeysStoreService _keysStoreService;

        public SignService(
            IKeysStoreService keysStoreService)
        {
            _keysStoreService = keysStoreService ?? throw new Exception(nameof(keysStoreService));
        }

        public string Sign(string body, string keyName)
        {
            var keyInfo = _keysStoreService.Get(keyName);

            if (keyInfo == null)
                throw new KeyNotFoundException(keyName);

            var rsa = keyInfo.PrivateKey.CreateRsa();

            var bodyToSign = string.Format("{0}{1}", keyInfo.ApiKey, body);

            return Convert.ToBase64String(
                rsa.SignData(
                    Encoding.UTF8.GetBytes(bodyToSign),
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1));
        }
    }
}
