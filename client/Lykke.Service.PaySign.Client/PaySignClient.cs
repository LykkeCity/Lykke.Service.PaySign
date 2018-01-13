using System;
using Common.Log;

namespace Lykke.Service.PaySign.Client
{
    public class PaySignClient : IPaySignClient, IDisposable
    {
        private readonly ILog _log;

        public PaySignClient(string serviceUrl, ILog log)
        {
            _log = log;
        }

        public void Dispose()
        {
            //if (_service == null)
            //    return;
            //_service.Dispose();
            //_service = null;
        }
    }
}
