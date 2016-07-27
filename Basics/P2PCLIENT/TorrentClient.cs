using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace Basics.P2PCLIENT
{
    public class TorrentClient
    {
        private readonly string host;
        private readonly string username;
        private readonly string password;

        public TorrentClient(string host, int port, string username, string password)
        {
            this.host = $"{host}:{port}";
            this.username = username;
            this.password = password;
        }

        public string GetToken(out CookieCollection outCookies)
        {
            try
            {
                var uriString = $"http://{host}/gui/token.html";
                var response = HttpGet(uriString, out outCookies);

                var doc = new HtmlDocument();
                doc.LoadHtml(response);
                var x = doc.DocumentNode.SelectNodes("//html/div");
                if (x != null && x.Count != 0)
                {
                    return x.First().InnerText;
                }
            }
            catch (Exception e)
            {
                throw new TorrentClientException(e);
            }
            throw new TorrentClientException();
        }

        public string ListTorrents()
        {
            return SendCommand("list=1");
        }

        public bool AddTorrentFromFile(string torrentFilePath)
        {
            throw new NotImplementedException();
        }

        public bool AddTorrentFromUrl(string url)
        {
            try
            {
                var x = SendCommand($"action=add-url&s={url}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool RemoveTorrent(string hash, bool keepData = true, bool keepTorrent = false)
        {
            try
            {
                string args;
                if (keepData)
                {
                    args = keepTorrent ? "action=remove&hash=" : "action=removetorrent&hash=";
                }
                else
                {
                    args = keepTorrent ? "action=removedata&hash=" : "action=removedatatorrent&hash=";
                }
                SendCommand($"{args}{hash}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool StopTorrent(string hash)
        {
            try
            {
                SendCommand($"action=stop&hash={hash}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool StartTorrent(string hash)
        {
            try
            {
                SendCommand($"action=start&hash={hash}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RecheckTorrent(string hash)
        {
            try
            {
                SendCommand($"action=recheck&hash={hash}");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GetState(int status, long remaining)
        {
            if ((status & TorrentStatus.TFS_ERROR) != 0)
                return "ERROR";
            if ((status & TorrentStatus.TFS_CHECKING) != 0)
                return "CHECKED";
            if ((status & TorrentStatus.TFS_TRANSFORMING) != 0)
                return "TRANSFORMING";
            if ((status & TorrentStatus.TFS_STARTED) != 0)
            {
                if ((status & TorrentStatus.TFS_PAUSED) != 0)
                    return "PAUSED";
                if ((status & TorrentStatus.TFS_AUTO) != 0)
                    return remaining == 0 ? "SEEDING" : "DOWNLOADING";
                return remaining == 0 ? "SEEDING_FORCED" : "DOWNLOADING_FORCED";
            }
            if ((status & TorrentStatus.TFS_PAUSED) != 0)
                return "PAUSED";
            if (remaining == 0)
                return (status & TorrentStatus.TFS_AUTO) != 0 ? "QUEUED_SEED" : "FINISHED";
            return (status & TorrentStatus.TFS_AUTO) != 0 ? "QUEUED" : "STOPPED";
        }

        public List<TorrentInfo> GetTorrentList()
        {
            var torrentsList = new List<TorrentInfo>();
            var json = ListTorrents();
            var obj = JObject.Parse(json);
            var torrents = (JArray) obj["torrents"];
            foreach (var torrent in torrents)
            {
                var torrentJArray = (JArray) torrent;
                torrentsList.Add(
                    new TorrentInfo
                    {
                        Hash = torrentJArray[0].Value<string>(),
                        StateCode = torrentJArray[1].Value<int>(),
                        Name = torrentJArray[2].Value<string>(),
                        Size = torrentJArray[3].Value<long>(),
                        Progress = Math.Round(torrentJArray[4].Value<int>()/10.0, 2),
                        Downloaded = torrentJArray[5].Value<long>(),
                        Uploaded = torrentJArray[6].Value<long>(),
                        Ratio = torrentJArray[7].Value<int>(),
                        UploadRate = torrentJArray[8].Value<double>(),
                        DownloadRate = torrentJArray[9].Value<double>(),
                        Eta = torrentJArray[10].Value<int>(),
                        Label = torrentJArray[11].Value<string>(),
                        PeersConnected = torrentJArray[12].Value<int>(),
                        Peers = torrentJArray[13].Value<int>(),
                        SeedsConnected = torrentJArray[14].Value<int>(),
                        Seeds = torrentJArray[15].Value<int>(),
                        AvailFactor = torrentJArray[16].Value<int>(),
                        Order = torrentJArray[17].Value<int>(),
                        Remaining = torrentJArray[18].Value<long>(),
                        DownloadUrl = torrentJArray[19].Value<string>(),
                        FeedUrl = torrentJArray[20].Value<string>(),
                        Sid = torrentJArray[22].Value<string>(),
                        DateAdded = UnixTimeStampToDateTime(torrentJArray[23].Value<long>()),
                        DateCompleted = UnixTimeStampToDateTime(torrentJArray[24].Value<long>()),
                        Folder = torrentJArray[26].ToString(),
                        State = GetState(torrentJArray[1].Value<int>(), torrentJArray[18].Value<long>())
                    }
                    );
            }
            return torrentsList;
        }

        /*
        public bool TorrentFiles(string hash)
        {
            throw new NotImplementedException();
        }
        public bool TorrentDownloadFile(string hash)
        {
            throw new NotImplementedException();
        }
        public bool TorrentStreamUrl(string hash)
        {
            throw new NotImplementedException();
        }
        */

        private string HttpGet(string uriString, out CookieCollection outCookies, CookieCollection inCookies = null)
        {
            var webRequest = (HttpWebRequest) WebRequest.Create(uriString);
            webRequest.Credentials = new NetworkCredential(username, password);
            webRequest.CookieContainer = new CookieContainer();
            if (inCookies != null)
            {
                webRequest.CookieContainer.Add(inCookies);
            }
            var webResponse = (HttpWebResponse) webRequest.GetResponse();
            outCookies = webResponse.Cookies;
            var stream = webResponse.GetResponseStream();
            var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        private string SendCommand(string args = "", string root = "/gui/", bool tokenRequired = true)
        {
            CookieCollection cookies = null;
            if (tokenRequired)
            {
                var token = GetToken(out cookies);
                args = $"token={token}&{args}";
            }
            var url = !string.IsNullOrEmpty(args) ? $"{root}?{args}" : root;
            return HttpGet($"http://{host}{url}", out cookies, cookies);
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}