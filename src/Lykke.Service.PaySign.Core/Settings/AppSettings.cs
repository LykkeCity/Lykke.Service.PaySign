using Lykke.Service.PaySign.Core.Settings.ServiceSettings;
using Lykke.Service.PaySign.Core.Settings.SlackNotifications;

namespace Lykke.Service.PaySign.Core.Settings
{
    public class AppSettings
    {
        public PaySignSettings PaySignService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}
