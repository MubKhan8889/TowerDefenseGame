struct TileChance
{
    public byte TileID { get; set; }
    public int Weight { get; set; }

    public TileChance(byte setTileID, int setWeight = 1)
    {
        TileID = setTileID;
        Weight = setWeight;
    }
}