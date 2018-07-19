using System;
using Autofac;
using Common.Log;

namespace Lykke.Service.PaySign.Client
{
    public static class AutofacExtension
    {
        public static void RegisterPaySignClient(this ContainerBuilder builder, string serviceUrl)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (serviceUrl == null) throw new ArgumentNullException(nameof(serviceUrl));
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterType<PaySignClient>()
                .WithParameter("serviceUrl", serviceUrl)
                .As<IPaySignClient>()
                .SingleInstance();
        }

        public static void RegisterPaySignClient(this ContainerBuilder builder, PaySignServiceClientSettings settings)
        {
            builder.RegisterPaySignClient(settings?.ServiceUrl);
        }
    }
}
