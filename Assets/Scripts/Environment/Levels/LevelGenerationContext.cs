namespace GP.Environment.Levels
{
    public class LevelGenerationContext
    {
        public int MinTiles { get; }
        public int MaxTiles { get; }
        public int TreasurePerTile { get; }

        public LevelGenerationContext(int minTiles, int maxTiles, int treasurePerTile)
        {
            MinTiles = minTiles;
            MaxTiles = maxTiles;
            TreasurePerTile = treasurePerTile;
        }
    }
}
