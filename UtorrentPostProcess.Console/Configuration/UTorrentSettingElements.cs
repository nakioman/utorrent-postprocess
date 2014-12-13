using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace UtorrentPostProcess.Console.Configuration
{
    public class UTorrentSettingElements : ConfigurationElement
    {
        [ConfigurationProperty("ip", IsRequired = true)]
        public string Ip
        {
            get { return (string)this["ip"]; }
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

        [ConfigurationProperty("torrentRatio", IsRequired = true)]
        public int TorrentRatio
        {
            get { return (int)this["torrentRatio"]; }
        }

        [ConfigurationProperty("tvfolder", IsRequired = true)]
        public string TvDownloadFolder
        {
            get { return (string)this["tvfolder"]; }
        }

        [ConfigurationProperty("moviesFolder", IsRequired = true)]
        public string MoviesDownloadFolder
        {
            get { return (string)this["moviesFolder"]; }
        }
    }
}
