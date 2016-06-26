using System;

namespace Basics.P2P
{
    public class TorrentInfo
    {
        public string Hash{ get; set; }
        public int StateCode{ get; set; }
        public string Name{ get; set; }
        public long Size{ get; set; }
        public double Progress{ get; set; }
        public long Downloaded{ get; set; }
        public long Uploaded{ get; set; }
        public int Ratio{ get; set; }
        public double UploadRate{ get; set; }
        public double DownloadRate { get; set; }
        public int Eta{ get; set; }
        public string Label{ get; set; }
        public int PeersConnected{ get; set; }
        public int Peers{ get; set; }
        public int SeedsConnected{ get; set; }
        public int Seeds{ get; set; }
        public int AvailFactor{ get; set; }
        public int Order{ get; set; }
        public long Remaining{ get; set; }
        public string DownloadUrl{ get; set; }
        public string FeedUrl{ get; set; }
        public string Sid{ get; set; }
        public DateTime DateAdded{ get; set; }
        public DateTime DateCompleted{ get; set; }
        public string Folder{ get; set; }
        public string State{ get; set; }
    }
}
