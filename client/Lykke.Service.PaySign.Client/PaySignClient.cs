using System;
using Common.Log;
using Lykke.Common.Log;

namespace Lykke.Service.PaySign.Client
{
    public class PaySignClient : IPaySignClient, IDisposable
    {
        private readonly ILog _log;

        public PaySignClient(string serviceUrl, ILogFactory logFactory)
        {
            _log = logFactory.CreateLog(this);
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
