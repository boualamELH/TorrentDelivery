namespace Basics.COM.KAT
{
    public class KatSearchResult : SearchResult
    {
        public string Name { get; set; }
        public string Uploader { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        public bool IsVerified { get; set; }
        public string FilesCount { get; set; }
        public string Age { get; set; }
        public int Seed { get; set; }
        public int Leech { get; set; }
        public string DownloadLink { get; set; }
        public string MagnetLink { get; set; }
    }
}