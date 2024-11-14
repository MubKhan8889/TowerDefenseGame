struct TileChance
{
    public TileDisplay Tile { get; }
    public int Weight { get; }

    public TileChance(TileDisplay setTile, int setWeight = 1)
    {
        Tile = setTile;
        Weight = setWeight;
    }
}