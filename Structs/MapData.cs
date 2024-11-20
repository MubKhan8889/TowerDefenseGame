using System.Drawing;

struct MapData
{
    public string Name { get; }
    public Difficulty Difficulty { get; }
    public Point MapSize { get; }
    public List<TileChance> MapTiles { get; }
    public List<Point> TrackPath { get; }

    public MapData(string setName, Difficulty setDifficulty, Point setMapSize, List<TileChance> setTiles, List<Point> setTrack)
    {
        Name = setName;
        Difficulty = setDifficulty;
        MapSize = setMapSize;
        MapTiles = setTiles;
        TrackPath = setTrack;
    }
}