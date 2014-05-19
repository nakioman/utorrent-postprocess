using UTorrent.Api.Data;

namespace UtorrentPostProcess.Console.Plugins
{
    public interface IPlugin
    {
        string Label { get; }
        void Run(string torrent);
    }
}