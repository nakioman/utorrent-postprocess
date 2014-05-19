using System.Configuration;

namespace UtorrentPostProcess.Console.Configuration
{
    public class UtorrentPostProcessConfigSection : ConfigurationSection
    {
        private const string UtorrentPostProcessSectionConst = "uTorrentPostProcess";

        public static UtorrentPostProcessConfigSection GetConfig()
        {
            return (UtorrentPostProcessConfigSection)ConfigurationManager.GetSection(UtorrentPostProcessSectionConst) ??
                new UtorrentPostProcessConfigSection();
        }

        [ConfigurationProperty("uTorrent", IsRequired = true)]
        public UTorrentSettingElements UTorrent
        {
            get
            {
                return (UTorrentSettingElements)this["uTorrent"] ??
                    new UTorrentSettingElements();
            }
        }
    }
}