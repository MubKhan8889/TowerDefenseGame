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
                { 12, new TileDisplay("/)", ConsoleColor.Gray) },
                { 13, new TileDisplay("(|", ConsoleColor.Gray) },
                { 14, new TileDisplay(",^", ConsoleColor.Yellow) },
                { 15, new TileDisplay("^,", ConsoleColor.Yellow) },
                { 31, new TileDisplay(",^", ConsoleColor.DarkGreen) },
                { 32, new TileDisplay("#]", ConsoleColor.DarkGray) },
                { 33, new TileDisplay("..", ConsoleColor.DarkGray) },
                { 61, new TileDisplay("(|") },
                { 62, new TileDisplay("(U") },
                { 63, new TileDisplay("(T") },
                { 71, new TileDisplay("[|") },
                { 72, new TileDisplay("[U") },
                { 73, new TileDisplay("[T") },
                { 101, new TileDisplay("()", ConsoleColor.DarkGreen) },
                { 102, new TileDisplay("II", ConsoleColor.Gray) },
                { 103, new TileDisplay(">>", ConsoleColor.Yellow) },
                { 104, new TileDisplay("qp", ConsoleColor.Red) },
                { 105, new TileDisplay("[]", ConsoleColor.DarkGray) },
                { 111, new TileDisplay("[1", ConsoleColor.DarkYellow) },
                { 112, new TileDisplay("[2", ConsoleColor.DarkYellow) },
                { 113, new TileDisplay("[3", ConsoleColor.DarkYellow) },
                { 253, new TileDisplay("XX", ConsoleColor.Yellow) },
                { 254, new TileDisplay("##") },
                { 255, new TileDisplay("[]", ConsoleColor.Gray) },
            };

            // Set map data
            List<MapData> setAllMapData = new List<MapData>
            {
                new MapData(
                    "Plains",
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
                ),

                new MapData(
                    "Caves",
                    Difficulty.Normal,
                    new Point(14, 14),

                    new List<TileChance>
                    {
                        new TileChance(0, 20),
                        new TileChance(12, 3),
                        new TileChance(13, 3),
                        new TileChance(32, 1),
                    },

                    new List<Point>
                    {
                        new Point(0, 11),
                        new Point(8, 11),
                        new Point(8, 3),
                        new Point(3, 3),
                        new Point(3, 7),
                        new Point(14, 7)
                    }
                ),

                new MapData(
                    "Field",
                    Difficulty.Hard,
                    new Point(9, 28),

                    new List<TileChance>
                    {
                        new TileChance(0, 5),
                        new TileChance(14, 7),
                        new TileChance(15, 7),
                        new TileChance(33, 1),
                    },

                    new List<Point>
                    {
                        new Point(4, 27),
                        new Point(4, 0)
                    }
                ),
            };

            // Set tower data
            List<TowerData> setAllTowerData = new List<TowerData>
            {
                new TowerData("Basic",
                    61,
                    TowerType.Basic,
                    150,
                    10,
                    6,
                    2f),

                new TowerData("Cannon",
                    62,
                    TowerType.Cannon,
                    225,
                    7,
                    4,
                    3f),

                new TowerData("Sniper",
                    63,
                    TowerType.Basic,
                    200,
                    10,
                    10,
                    5.5f),

                new TowerData("Advanced",
                    71,
                    TowerType.Basic,
                    500,
                    6,
                    7,
                    0.5f),

                new TowerData("Destroyer",
                    72,
                    TowerType.Cannon,
                    750,
                    10,
                    6,
                    2f),

                new TowerData("Overseer",
                    73,
                    TowerType.Basic,
                    650,
                    20,
                    14,
                    4f)
            };
                
            // Set enemy data
            List<EnemyData> setAllEnemyData = new List<EnemyData>
            {
                new EnemyData(101, 20, 5f, 5, 8),
                new EnemyData(102, 40, 3.5f, 8, 13),
                new EnemyData(103, 20, 12.5f, 4, 20),
                new EnemyData(104, 80, 7f, 20, 35),
                new EnemyData(105, 100, 6f, 12, 28),
                new EnemyData(111, 150, 5f, 50, 200),
                new EnemyData(112, 300, 6f, 75, 500),
                new EnemyData(113, 600, 7f, 100, 1000)
            };

            // Set round data
            List<RoundData> setAllRoundData = new List<RoundData>
            {
                // 1
                new RoundData(50,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 3, 1.5f)
                    }),

                // 2
                new RoundData(55,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 4, 1.1f)
                    }),

                // 3
                new RoundData(60,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 3, 0.8f),
                        new EnemyRoundInfo(0, 3, 1.5f)
                    }),

                // 4
                new RoundData(70,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(1, 2, 3f),
                        new EnemyRoundInfo(0, 4, 1.25f)
                    }),

                // 5
                new RoundData(80,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 3, 0.5f),
                        new EnemyRoundInfo(0, 2, 2.5f),
                        new EnemyRoundInfo(0, 3, 0.5f),
                    }),

                // 6
                new RoundData(90,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(0, 5, 2.25f)
                    }),

                // 7
                new RoundData(105,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(1, 1, 1f),
                        new EnemyRoundInfo(0, 5, 1.5f),
                        new EnemyRoundInfo(2, 2, 1f)
                    }),

                // 8
                new RoundData(120,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(2, 1, 1f),
                        new EnemyRoundInfo(0, 4, 1.25f),
                        new EnemyRoundInfo(2, 1, 3f),
                        new EnemyRoundInfo(1, 2, 2f),
                        new EnemyRoundInfo(2, 1, 1f),
                    }),

                // 9
                new RoundData(135,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(5, 1, 1f)
                    }),

                // 10
                new RoundData(155,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(1, 10, 1.25f),
                        new EnemyRoundInfo(2, 3, 0.75f),
                        new EnemyRoundInfo(3, 2, 5f)
                    }),

                // 11
                new RoundData(175,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(3, 2, 3f),
                        new EnemyRoundInfo(2, 10, 0.25f),
                    }),

                // 12
                new RoundData(195,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(6, 1, 1f)
                    }),

                // 13
                new RoundData(220,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(4, 2, 2f),
                        new EnemyRoundInfo(2, 1, 2f),
                        new EnemyRoundInfo(3, 8, 1f),
                        new EnemyRoundInfo(4, 2, 0.5f),
                    }),

                // 14
                new RoundData(245,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(1, 5, 0.45f),
                        new EnemyRoundInfo(0, 15, 0.25f),
                        new EnemyRoundInfo(4, 4, 1.25f),
                    }),

                // 15
                new RoundData(270,
                    new List<EnemyRoundInfo>
                    {
                        new EnemyRoundInfo(7, 1, 1f)
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