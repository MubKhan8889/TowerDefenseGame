using System.Drawing;

struct MapData
{
    public string Name { get; }
    public Difficulty Difficulty { get; }
    public List<TileChance> MapTiles { get; }
    public List<Point> TrackPath { get; }

    public MapData(string setName, Difficulty setDifficulty, List<TileChance> setTiles, List<Point> setTrack)
    {
        Name = setName;
        Difficulty = setDifficulty;
        MapTiles = setTiles;
        TrackPath = setTrack;
    }
}