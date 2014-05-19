using System.Configuration;

namespace UtorrentPostProcess.Console.Configuration
{
    public class CouchPotatoConfigSection : ConfigurationSection
    {
        private const string CouchPotatoConfigConst = "couchPotato";

        public static CouchPotatoConfigSection GetConfig()
        {
            return (CouchPotatoConfigSection)ConfigurationManager.GetSection(CouchPotatoConfigConst) ??
                new CouchPotatoConfigSection();
        }

        [ConfigurationProperty("label", IsRequired = true)]
        public string Label
        {
            get { return (string)this["label"]; }
        }

        [ConfigurationProperty("host", IsRequired = true)]
        public string Host
        {
            get { return (string)this["host"]; }
        }

        [ConfigurationProperty("baseUrl", IsRequired = true)]
        public string BaseUrl
        {
            get { return (string)this["baseUrl"]; }
        }

        [ConfigurationProperty("protocol", IsRequired = true)]
        public string Protocol
        {
            get { return (string)this["protocol"]; }
        }

        [ConfigurationProperty("port", IsRequired = true)]
        public int Port
        {
            get { return (int)this["port"]; }
        }

        [ConfigurationProperty("apiKey", IsRequired = true)]
        public string ApiKey
        {
            get { return (string)this["apiKey"]; }
        }
    }
}