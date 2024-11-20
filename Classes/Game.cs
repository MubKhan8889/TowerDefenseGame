using System.Drawing;

class Game
{
    // Misc
    private Random random = new Random();
    private ConsoleDisplay consoleDisplay = new ConsoleDisplay();
    private TowerFactory towerFactory = new TowerFactory();

    // Map Display
    private byte[] MapDisplayData;
    private byte[] MapDisplayOverlayData;
    private bool[] MapObstructionData;
    private Point MapSize;

    // Track
    private Dictionary<int, int> TrackProgressToIndex = new Dictionary<int, int>();
    private int TrackLength;

    // Game Data
    private int Money = 0;
    private int Lives = 0;
    private int CurrentRound = 0;
    private int FinalRound = 0;

    // Placing Data
    private bool IsPlacing = false;
    private string TowerSelectName = "none";
    private int placingPosX = 0;
    private int placingPosY = 0;

    List<TowerBase> Towers = new List<TowerBase>();
    List<Enemy> Enemies = new List<Enemy>();

    // Functions
    public void SetMapData(MapData useMapData)
    {
        // Set up map data
        MapSize = useMapData.MapSize;
        MapDisplayData = new byte[MapSize.X * MapSize.Y];
        MapDisplayOverlayData = new byte[MapSize.X * MapSize.Y];
        MapObstructionData = new bool[MapSize.X * MapSize.Y];

        TrackLength = 0;

        Dictionary<int, byte> weights = new Dictionary<int, byte>();
        int addedWeight = 0;
        foreach(TileChance tile in useMapData.MapTiles)
        {
            for (int i = addedWeight; i < addedWeight + tile.Weight; i++) weights[i] = tile.TileID;
            addedWeight += tile.Weight;
        }

        for (int i = 0; i < MapDisplayData.Length; i++)
        {
            int chosenWeight = random.Next(0, addedWeight);
            MapDisplayData[i] = weights[chosenWeight];
        }

        int pathProgress = 0;
        int posX = useMapData.TrackPath[0].X;
        int posY = useMapData.TrackPath[0].Y;
        for (int i = 0; i < useMapData.TrackPath.Count - 1; i++)
        {
            int moveX = useMapData.TrackPath[i + 1].X - useMapData.TrackPath[i].X;
            int moveY = useMapData.TrackPath[i + 1].Y - useMapData.TrackPath[i].Y;

            string direction = "Horizontal";
            if (moveX == 0 && moveY != 0) direction = "Vertical";
            int moveTiles = Math.Abs((direction == "Horizontal") ? moveX : moveY);

            int directionMult = 1;
            if ((direction == "Horizontal" && moveX < 0) || (direction == "Vertical" && moveY < 0)) directionMult = -1;

            for (int j = 0; j < moveTiles; j++)
            {
                TrackProgressToIndex.Add(pathProgress, posX + (posY * MapSize.X));
                MapDisplayData[posX + (MapSize.X * posY)] = 254;
                MapObstructionData[posX + (MapSize.X * posY)] = true;

                pathProgress++;
                if (direction == "Horizontal") posX += directionMult;
                else if (direction == "Vertical") posY += directionMult;
            }
        }

        TrackProgressToIndex.Add(pathProgress, posX + (posY * MapSize.X));
        MapDisplayData[posX + (MapSize.X * posY)] = 255;
        TrackLength = pathProgress;

        // Set up game info
        Money = 500;
        Lives = 100;
        FinalRound = 10;
        CurrentRound = 1;
    }

    // Map Display
    public void DisplayMap()
    {
        for (int i = 0; i < MapSize.X * MapSize.Y; i++)
        {
            if (i != 0 && i % MapSize.X == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("|");
            }

            if (i == (placingPosX + (MapSize.X * placingPosY)) && IsPlacing == true)
            {
                ConsoleColor chosenColour = (MapObstructionData[placingPosX + (MapSize.X * placingPosY)] == true) ? ConsoleColor.Red : ConsoleColor.Cyan;
                consoleDisplay.Tile(254, chosenColour);

                continue;
            }

            byte choseTile = (MapDisplayOverlayData[i] != 0) ? MapDisplayOverlayData[i] : MapDisplayData[i];
            consoleDisplay.Tile(choseTile);
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("|");
        for (int i = 0; i < MapSize.X; i++) Console.Write("--");
        Console.WriteLine("+");
    }

    private void SetUpMapOverlay()
    {
        for (int i = 0; i < MapSize.X * MapSize.Y; i++) MapDisplayOverlayData[i] = 0;

        foreach (TowerBase tower in Towers)
        {
            Point towerPos = tower.GetPosition();
            MapDisplayOverlayData[towerPos.X + (towerPos.Y * MapSize.X)] = tower.GetDisplayID();
        }

        foreach (Enemy enemy in Enemies)
        {
            MapDisplayOverlayData[TrackProgressToIndex[enemy.GetCastedTrackProgress()]] = enemy.GetDisplayID();
        }

        MapDisplayOverlayData[placingPosX + (MapSize.X * placingPosY)] = 254;
    }

    // Game display
    public void DisplayGameInfo()
    {
        Console.Write("\n");
        consoleDisplay.SubTitle("Info");

        consoleDisplay.Value("Money", Convert.ToString(Money), ConsoleColor.Yellow);
        consoleDisplay.Value("Lives", Convert.ToString(Lives), ConsoleColor.Red);
        consoleDisplay.Value("Round", $"{CurrentRound}/{FinalRound}");
    }

    // Game Options
    public void SetGamePlacing()
    {
        IsPlacing = false;
        TowerSelectName = "None";
        placingPosX = 0;
        placingPosY = 0;
    }

    // Get and Set
    public bool GetIsPlacing() { return IsPlacing; }
    public void SetIsPlacing(bool value) { IsPlacing = value; }
}