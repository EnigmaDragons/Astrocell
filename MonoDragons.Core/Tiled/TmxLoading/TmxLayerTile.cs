namespace MonoDragons.Core.Tiled.TmxLoading
{
    public struct TmxLayerTile
    {
        public int Column;
        public int Row;
        public int TileId;

        public static TmxLayerTile Create(int column, int row, int tileId)
        {
            return new TmxLayerTile
            {
                Column = column,
                Row = row,
                TileId = tileId,
            };
        }
    }
}
