using Lykke.Service.PaySign.Core.Settings.ServiceSettings;
using Lykke.Service.PaySign.Core.Settings.SlackNotifications;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.PaySign.Core.Settings
{
    public class AppSettings
    {
        public PaySignSettings PaySignService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
        public MonitoringServiceClientSettings MonitoringServiceClient { get; set; }
    }

    public class MonitoringServiceClientSettings
    {
        [HttpCheck("api/isalive", false)]
        public string MonitoringServiceUrl { get; set; }
    }
}
