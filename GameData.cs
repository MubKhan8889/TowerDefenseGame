using System.Drawing;

sealed class Data
{
    // Make unset variables first
    private static Lazy<bool> _isInitalised = new Lazy<bool>(() => false);

    private static Lazy<Dictionary<byte, TileDisplay>> _mapTileIDData = new Lazy<Dictionary<byte, TileDisplay>>();
    private static Lazy<List<MapData>> _allMapData = new Lazy<List<MapData>>();
    private static Lazy<List<TowerData>> _allTowerData = new Lazy<List<TowerData>>();
    private static Lazy<List<EnemyData>> _allEnemyData = new Lazy<List<EnemyData>>();
    private static Lazy<List<RoundData>> _allRoundData = new Lazy<List<RoundData>>();

    // Add lines for getting values from variables
    public Dictionary<byte, TileDisplay> MapTileID => _mapTileIDData.Value;
    public List<MapData> AllMapData => _allMapData.Value;
    public List<TowerData> AllTowerData => _allTowerData.Value;
    public List<EnemyData> AllEnemyData => _allEnemyData.Value;
    public List<RoundData> AllRoundData => _allRoundData.Value;

    public Data()
    {
        if (_isInitalised.Value == true) return;

        lock (_isInitalised)
        {
            // Set All Data //
            // Set map tile data
            Dictionary<byte, TileDisplay> setMapTileIDData = new Dictionary<byte, TileDisplay>
            {
                { 0, new TileDisplay() },
                { 11, new TileDisplay(",,", ConsoleColor.Green) },
                { 31, new TileDisplay(",^", ConsoleColor.DarkGreen) },
                { 61, new TileDisplay("(|") },
                { 62, new TileDisplay("U)") },
                { 63, new TileDisplay("H^") },
                { 101, new TileDisplay("()", ConsoleColor.DarkGreen) },
                { 253, new TileDisplay("XX", ConsoleColor.Yellow) },
                { 254, new TileDisplay("##") },
                { 255, new TileDisplay("[]", ConsoleColor.Gray) },
            };

            // Set map data
            List<MapData> setAllMapData = new List<MapData>
            {
                new MapData(
                    "TestMap",
                    Difficulty.Easy,
                    new Point(24, 18),

                    new List<TileChance>
                    {
                        new TileChance(0, 7),
                        new TileChance(11, 5),
                        new TileChance(31, 1),
                    },

                    new List<Point>
                    {
                        new Point(0, 3),
                        new Point(21, 3),
                        new Point(21, 9),
                        new Point(2, 9),
                        new Point(2, 15),
                        new Point(21, 15)
                    }
                )
            };

            // Set tower data
            List<TowerData> setAllTowerData = new List<TowerData>
            {
                new TowerData("Basic",
                    61,
                    TowerType.Basic,
                    200,
                    10,
                    6,
                    2f),

                new TowerData("Advanced",
                    63,
                    TowerType.Basic,
                    500,
                    5,
                    10,
                    0.5f)
            };
                
            // Set enemy data
            List<EnemyData> setAllEnemyData = new List<EnemyData>
            {
                new EnemyData(101, 20, 5f, 25, 8) // Set damage to 5
            };

            // Set round data
            List<RoundData> setAllRoundData = new List<RoundData>
            {
                new RoundData(50,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 3, 1.5f)
                    }),

               new RoundData(55,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 4, 1.1f)
                    }),

                new RoundData(65,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 3, 0.8f),
                        new EnemyRoundInfo(0, 3, 1.5f)
                    }),

                new RoundData(80,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 3, 0.6f),
                        new EnemyRoundInfo(0, 4, 1.25f)
                    }),

                new RoundData(100,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 4, 0.5f),
                        new EnemyRoundInfo(0, 4, 1f)
                    }),

                new RoundData(125,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 5, 0.5f),
                        new EnemyRoundInfo(0, 5, 1f)
                    }),

                new RoundData(150,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 6, 0.45f),
                        new EnemyRoundInfo(0, 6, 0.9f)
                    }),

                new RoundData(175,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 7, 0.4f),
                        new EnemyRoundInfo(0, 7, 0.9f)
                    }),

                new RoundData(200,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 8, 0.45f),
                        new EnemyRoundInfo(0, 8, 0.9f)
                    }),

                new RoundData(250,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 9, 0.4f),
                        new EnemyRoundInfo(0, 9, 0.8f)
                    })
            };

            // Assign data
            _mapTileIDData = new Lazy<Dictionary<byte, TileDisplay>>(() => setMapTileIDData);
            _allMapData = new Lazy<List<MapData>>(() => setAllMapData);
            _allTowerData = new Lazy<List<TowerData>>(() => setAllTowerData);
            _allEnemyData = new Lazy<List<EnemyData>>(() => setAllEnemyData);
            _allRoundData = new Lazy<List<RoundData>>(() => setAllRoundData);

            // Set initialisation
            _isInitalised = new Lazy<bool>(() => true);
        }
    }
}