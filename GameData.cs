using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;

sealed class Data
{
    // This class is meant to be accessed by other C# files without duplicating the data
    // This was the resource I read from to create this class: https://csharpindepth.com/articles/singleton

    // Make unset variables first
    private static Lazy<bool> _isInitalised = new Lazy<bool>(() => false);

    private static Lazy<Dictionary<byte, TileDisplay>> _mapTileIDData = new Lazy<Dictionary<byte, TileDisplay>>();
    private static Lazy<Dictionary<Difficulty, DifficultyInfo>> _difficultyData = new Lazy<Dictionary<Difficulty, DifficultyInfo>>();
    private static Lazy<List<MapData>> _allMapData = new Lazy<List<MapData>>();
    private static Lazy<List<TowerData>> _allTowerData = new Lazy<List<TowerData>>();
    private static Lazy<List<EnemyData>> _allEnemyData = new Lazy<List<EnemyData>>();
    private static Lazy<List<RoundData>> _allRoundData = new Lazy<List<RoundData>>();

    // Add lines for getting values from variables
    public Dictionary<byte, TileDisplay> MapTileID => _mapTileIDData.Value;
    public Dictionary<Difficulty, DifficultyInfo> DifficultyData => _difficultyData.Value;
    public List<MapData> AllMapData => _allMapData.Value;
    public List<TowerData> AllTowerData => _allTowerData.Value;
    public List<EnemyData> AllEnemyData => _allEnemyData.Value;
    public List<RoundData> AllRoundData => _allRoundData.Value;

    public Data()
    {
        if (_isInitalised.Value == true) return;

        lock (_isInitalised)
        {
            // --== GET DATA FROM JSON AND SET ALL DATA ==-- //
            // Set map tile data
            Dictionary<byte, TileDisplay> setMapTileIDData = new Dictionary<byte, TileDisplay>();

            string tilesIDJSON = File.ReadAllText("JSONDATA/TilesData.json");
            var tilesIDDeserialise = JsonSerializer.Deserialize<Dictionary<byte, TileDisplay>>(tilesIDJSON);
            foreach (var data in tilesIDDeserialise) setMapTileIDData[data.Key] = data.Value;

            // Set difficulty data
            Dictionary<Difficulty, DifficultyInfo> setDifficultyData = new Dictionary<Difficulty, DifficultyInfo>();
            
            string difficultyJSON = File.ReadAllText("JSONDATA/DifficultyInfo.json");
            var difficultyDeserialise = JsonSerializer.Deserialize<Dictionary<Difficulty, DifficultyInfo>>(difficultyJSON);
            foreach (var data in difficultyDeserialise) setDifficultyData[data.Key] = data.Value;

            // Set map data
            List<MapData> setAllMapData = new List<MapData>();

            string mapJSON = File.ReadAllText("JSONDATA/MapData.json");
            var mapDeserialise = JsonSerializer.Deserialize<List<MapData>>(mapJSON);
            foreach (var data in mapDeserialise) setAllMapData.Add(data);

            // Set tower data
            List<TowerData> setAllTowerData = new List<TowerData>();

            string towerJSON = File.ReadAllText("JSONDATA/TowerData.json");
            var towerDeserialise = JsonSerializer.Deserialize<List<TowerData>>(towerJSON);
            foreach (var data in towerDeserialise) setAllTowerData.Add(data);

            // Set enemy data
            List<EnemyData> setAllEnemyData = new List<EnemyData>();

            string enemyJSON = File.ReadAllText("JSONDATA/EnemyData.json");
            var enemyDeserialise = JsonSerializer.Deserialize<List<EnemyData>>(enemyJSON);
            foreach (var data in enemyDeserialise) setAllEnemyData.Add(data);

            // Set round data
            List<RoundData> setAllRoundData = new List<RoundData>();

            string roundJSON = File.ReadAllText("JSONDATA/RoundData.json");
            var roundDeserialise = JsonSerializer.Deserialize<List<RoundData>>(roundJSON);
            foreach (var data in roundDeserialise) setAllRoundData.Add(data);

            // Assign data
            _mapTileIDData = new Lazy<Dictionary<byte, TileDisplay>>(() => setMapTileIDData);
            _difficultyData = new Lazy<Dictionary<Difficulty, DifficultyInfo>>(() => setDifficultyData);
            _allMapData = new Lazy<List<MapData>>(() => setAllMapData);
            _allTowerData = new Lazy<List<TowerData>>(() => setAllTowerData);
            _allEnemyData = new Lazy<List<EnemyData>>(() => setAllEnemyData);
            _allRoundData = new Lazy<List<RoundData>>(() => setAllRoundData);

            // Set initialisation
            _isInitalised = new Lazy<bool>(() => true);
        }
    }
}