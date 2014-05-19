using System;
using System.Net;
using System.Threading;
using log4net;
using UTorrent.Api.Data;
using UtorrentPostProcess.Console.Configuration;

namespace UtorrentPostProcess.Console.Plugins
{
    public class CouchPotatoPlugin : IPlugin
    {
        private readonly ILog _log;
        private readonly CouchPotatoConfigSection _config;

        public CouchPotatoPlugin()
        {
            _config = CouchPotatoConfigSection.GetConfig();
            _log = LogManager.GetLogger(typeof(CouchPotatoPlugin));
        }
        public string Label { get { return _config.Label; } }
        public void Run(string torrent)
        {
            try
            {
                var couchPotatoUrl = String.Format("{0}://{1}:{2}/{3}/api/{4}/renamer.scan/?async=1&base_folder={5}",
                    _config.Protocol, _config.Host, _config.Port, _config.BaseUrl, _config.ApiKey, torrent);
                var request = WebRequest.Create(couchPotatoUrl);
                request.GetResponse();
            }
            catch (Exception ex)
            {
                _log.Error("There was an error in couchpotato processing the torrent in path " + torrent, ex);
            }
        }
    }
}