using System;
using System.IO;
using System.Linq;
using log4net;
using UTorrent.Api;
using UtorrentPostProcess.Console.Configuration;

namespace UtorrentPostProcess.Console
{
    public class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
        private static readonly UtorrentPostProcessConfigSection Config = UtorrentPostProcessConfigSection.GetConfig();
        private static UTorrentClient _uTorrentClient;

        static void Main(string[] args)
        {

            _uTorrentClient = new UTorrentClient(Config.UTorrent.Ip, Config.UTorrent.Port,
                Config.UTorrent.Username, Config.UTorrent.Password);

            if (args.Length != 1)
            {
                Log.Error("Only Torrent hash id is expected as an argument");
                Environment.Exit(-1);
            }

            try
            {
                var torrents = _uTorrentClient.GetList();
                if (torrents.Error != null)
                {
                    Log.Error(torrents.Error.Message);
                    Environment.Exit(-1);
                }

                var torrent = torrents.Result.Torrents.SingleOrDefault(x => x.Hash == args[0]);
                if (torrent == null)
                {
                    Log.ErrorFormat("Torrent with hash {0}, was not found", args[0]);
                    Environment.Exit(-1);
                }

                switch (torrent.Label)
                {
                    case "tv":
                        CopyTorrentToFolder(torrent.Path, torrent.Hash, Config.UTorrent.TvDownloadFolder);
                        if (Config.UTorrent.TorrentRatio == 0 || torrent.Ratio >= Config.UTorrent.TorrentRatio * 100)
                        {
                            _uTorrentClient.DeleteTorrent(torrent.Hash);
                        }
                        break;
                    case "movies":
                        CopyTorrentToFolder(torrent.Path, torrent.Hash, Config.UTorrent.MoviesDownloadFolder);
                        if (Config.UTorrent.TorrentRatio == 0 || torrent.Ratio >= Config.UTorrent.TorrentRatio * 100)
                        {
                            _uTorrentClient.DeleteTorrent(torrent.Hash);
                        }
                        break;
                }

            }
            catch (ServerUnavailableException)
            {
                Log.Error("The utorrent server cannot be reach");
                Environment.Exit(-1);
            }

            Environment.Exit(0);
        }

        private static string CopyTorrentToFolder(string torrentPath, string hash, string destinationFolder)
        {
            var folder = String.Empty;
            try
            {
                var isDir = (File.GetAttributes(torrentPath) & FileAttributes.Directory) == FileAttributes.Directory;
                folder = isDir ?
                    Path.Combine(destinationFolder, torrentPath.Split(Path.DirectorySeparatorChar).Last())
                    : destinationFolder;
                CopyDir(torrentPath, folder, hash);
                return folder;
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error copying torrent from path {0}, to working dir {1}", torrentPath, folder), ex);
                Environment.Exit(-1);
            }
            return null;
        }

        public static void CopyDir(string source, string target, string hash)
        {
            if (!Directory.Exists(target)) Directory.CreateDirectory(target);
            var files = _uTorrentClient.GetFiles(hash).Result.Files[hash];

            foreach (var file in files)
            {
                var subPath = file.Name.Replace(file.NameWithoutPath, "");
                var subTarget = Path.Combine(target, subPath);
                if (!Directory.Exists(subTarget)) Directory.CreateDirectory(subTarget);
                File.Copy(Path.Combine(source, file.Name), Path.Combine(target, file.Name));
            }
        }
    }
}
