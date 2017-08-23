namespace MonoDragons.Core.Tiled.TmxLoading
{
    public struct TmxTile
    {
        public int Column;
        public int Row;
        public int TextureId;

        public static TmxTile Create(int column, int row, int textureId)
        {
            return new TmxTile
            {
                Column = column,
                Row = row,
                TextureId = textureId,
            };
        }
    }
}
