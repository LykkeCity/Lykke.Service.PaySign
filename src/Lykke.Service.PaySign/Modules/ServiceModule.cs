using Autofac;
using Autofac.Extensions.DependencyInjection;
using AzureStorage.Tables;
using Lykke.Common.Log;
using Lykke.Service.PaySign.AzureRepositories;
using Lykke.Service.PaySign.Core.Repositories;
using Lykke.Service.PaySign.Core.Services;
using Lykke.Service.PaySign.Core.Settings.ServiceSettings;
using Lykke.Service.PaySign.Services;
using Lykke.SettingsReader;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.PaySign.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<PaySignSettings> _settings;
        // NOTE: you can remove it if you don't need to use IServiceCollection extensions to register service specific dependencies
        private readonly IServiceCollection _services;

        public ServiceModule(IReloadingManager<PaySignSettings> settings)
        {
            _settings = settings;

            _services = new ServiceCollection();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();

            builder.RegisterType<KeyStoreService>()
                .As<IKeysStoreService>()
                .SingleInstance();

            builder.RegisterType<SignService>()
                .As<ISignService>()
                .SingleInstance();

            builder.RegisterType<CallbackStubService>()
                .As<ICallbackStubService>()
                .SingleInstance();

            builder.Register(c => new CallbackDataRepository(
                    AzureTableStorage<CallbackDataEntity>.Create(_settings.ConnectionString(x => x.Db.DataConnString),
                        "CallbackData", c.Resolve<ILogFactory>())))
                .As<ICallbackDataRepository>()
                .SingleInstance();

            builder.Populate(_services);
        }
    }
}
