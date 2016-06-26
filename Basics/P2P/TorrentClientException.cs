using System;

namespace Basics.P2P
{
    public class TorrentClientException : Exception
    {
        public TorrentClientException() : base("Unreachable uTorrent WebUI server.")
        {
        }

        public TorrentClientException(string message) : base(message)
        {
        }

        public TorrentClientException(Exception innerException)
            : base("Unreachable uTorrent WebUI server.", innerException)
        {
        }
    }
}