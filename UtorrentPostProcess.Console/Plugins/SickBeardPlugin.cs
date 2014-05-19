using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UTorrent.Api.Data;
using UtorrentPostProcess.Console.Configuration;

namespace UtorrentPostProcess.Console.Plugins
{
    public class SickBeardPlugin : IPlugin
    {
        private readonly SickBeardConfigSection _config;
        private readonly ILog _log;

        public SickBeardPlugin()
        {
            _log = LogManager.GetLogger(typeof(SickBeardPlugin));
            _config = SickBeardConfigSection.GetConfig();
        }

        public string Label
        {
            get { return _config.Label; }
        }

        public void Run(string torrent)
        {
            try
            {

                var sickBeardUrl = String.Format("{0}://{1}:{2}/{3}/home/postprocess/processEpisode?quiet=1&dir={4}",
                    _config.Protocol, _config.Host, _config.Port, _config.BaseUrl, torrent);
                var request = WebRequest.Create(sickBeardUrl);
                request.Credentials = new NetworkCredential(_config.Username, _config.Password);
                request.GetResponse();
            }
            catch (Exception ex)
            {
                _log.Error("There was an error in sickbeard processing the torrent in path " + torrent, ex);
            }
        }
    }
}
