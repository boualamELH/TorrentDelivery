namespace Basics.P2PCLIENT
{
    public static class TorrentStatus
    {
        public const int TFS_STARTED = 1;
        // the torrent is checking its file, to figure out which
        // parts of the files we already have
        public const int TFS_CHECKING = 2;
        // start the torrent when the file-check completes
        public const int TFS_START_AFTER_CHECK = 4;
        // The files in this torrent have been checked. No need
        // to check them again
        public const int TFS_CHECKED = 8;
        // An error ocurred
        public const int TFS_ERROR = 16;
        // The torrent is paused. i.e. all transfers are suspended
        public const int TFS_PAUSED = 32;
        // Auto managed. uTorrent will automatically start and stop
        // the torrent based on the number of active torrents etc.
        public const int TFS_AUTO = 64;
        // The .torrent file has been loaded
        public const int TFS_LOADED = 128;
        // the torrent is transforming (usually copying data from
        // another torrent in order to download a similar torrent)
        public const int TFS_TRANSFORMING = 256;
        // start the torrent when the transformation completes
        public const int TFS_START_AFTER_TRANSFORM = 512;
    }
}