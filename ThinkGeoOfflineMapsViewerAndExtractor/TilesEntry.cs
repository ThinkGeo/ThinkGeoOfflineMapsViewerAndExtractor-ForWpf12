namespace MBTilesExtractor
{
    public class TilesEntry
    {
        public long ZoomLevel { get; set; }
        public long TileColumn { get; set; }
        public long TileRow { get; set; }
        public long TileId { get; set; }
        public byte[] TileData { get; set; }

        public TilesEntry()
        { }
    }
}