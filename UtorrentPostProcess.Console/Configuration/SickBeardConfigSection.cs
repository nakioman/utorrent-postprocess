using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtorrentPostProcess.Console.Configuration
{
    public class SickBeardConfigSection : ConfigurationSection
    {
        private const string SickBeardSectionConst = "sickBeard";

        public static SickBeardConfigSection GetConfig()
        {
            return (SickBeardConfigSection)ConfigurationManager.GetSection(SickBeardSectionConst) ??
                new SickBeardConfigSection();
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

        [ConfigurationProperty("username", IsRequired = true)]
        public string Username
        {
            get { return (string)this["username"]; }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["password"]; }
        }
    }
    
}
