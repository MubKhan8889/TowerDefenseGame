using System.Drawing;

struct MapData
{
    public string Name { get; set; }
    public Difficulty Difficulty { get; set; }
    public Point MapSize { get; set; }
    public List<TileChance> MapTiles { get; set; }
    public List<Point> TrackPath { get; set; }

    public MapData(string setName, Difficulty setDifficulty, Point setMapSize, List<TileChance> setTiles, List<Point> setTrack)
    {
        Name = setName;
        Difficulty = setDifficulty;
        MapSize = setMapSize;
        MapTiles = setTiles;
        TrackPath = setTrack;
    }
}