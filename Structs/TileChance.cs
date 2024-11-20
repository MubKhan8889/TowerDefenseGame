struct TileChance
{
    public byte TileID { get; }
    public int Weight { get; }

    public TileChance(byte setTileID, int setWeight = 1)
    {
        TileID = setTileID;
        Weight = setWeight;
    }
}