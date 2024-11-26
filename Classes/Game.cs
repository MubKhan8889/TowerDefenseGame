using System;
using System.Drawing;
using System.Security;

class Game
{
    // Misc
    private Data GameData = new Data();
    private Random random = new Random();
    private ConsoleDisplay consoleDisplay = new ConsoleDisplay();
    private TowerFactory towerFactory = new TowerFactory();

    // Map Display
    private byte[] MapDisplayData;
    private byte[] MapDisplayOverlayData;
    private bool[] MapObstructionData;
    private ConsoleColor?[] MapColourOverrideData;
    private Point MapSize;

    // Track
    private Dictionary<int, int> TrackProgressToIndex = new Dictionary<int, int>();
    private Dictionary<int, Point> TrackProgressToPosition = new Dictionary<int, Point>();
    private int TrackLength;

    // Game Data
    private int Money = 0;
    private int Lives = 0;
    private int CurrentRound = 0;
    private int FinalRound = 0;
    private bool GameEnd = false;

    // Placing Data
    private bool IsPlacing = false;
    private string TowerSelectName = "none";
    private int placingPosX = 0;
    private int placingPosY = 0;

    // Selling Data
    private bool IsSelling = false;
    private int TowerIndex = 0;

    // Game Data
    List<TowerBase> Towers = new List<TowerBase>();
    List<Enemy> Enemies = new List<Enemy>();

    // Functions
    public void SetMapData(int mapDataIndex)
    {
        MapData useMapData = GameData.AllMapData[mapDataIndex];

        // Delete Data
        Towers.Clear();
        Enemies.Clear();
        TrackProgressToIndex.Clear();
        TrackProgressToPosition.Clear();

        // Set up map data
        MapSize = useMapData.MapSize;
        MapDisplayData = new byte[MapSize.X * MapSize.Y];
        MapDisplayOverlayData = new byte[MapSize.X * MapSize.Y];
        MapObstructionData = new bool[MapSize.X * MapSize.Y];
        MapColourOverrideData = new ConsoleColor?[MapSize.X * MapSize.Y];
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
                TrackProgressToPosition.Add(pathProgress, new Point(posX, posY));
                MapDisplayData[posX + (MapSize.X * posY)] = 254;
                MapObstructionData[posX + (MapSize.X * posY)] = true;

                pathProgress++;
                if (direction == "Horizontal") posX += directionMult;
                else if (direction == "Vertical") posY += directionMult;
            }
        }

        TrackProgressToIndex.Add(pathProgress, posX + (posY * MapSize.X));
        TrackProgressToPosition.Add(pathProgress, new Point(posX, posY));
        MapDisplayData[posX + (MapSize.X * posY)] = 255;
        TrackLength = pathProgress;

        // Set up game info
        Money = 500;
        Lives = 100;
        FinalRound = 10;
        CurrentRound = 1;
        GameEnd = false;
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
            ConsoleColor? choseColour = (MapColourOverrideData[i] != null) ? MapColourOverrideData[i] : null;
            consoleDisplay.Tile(choseTile, choseColour);
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("|");
        for (int i = 0; i < MapSize.X; i++) Console.Write("--");
        Console.WriteLine("+");
    }

    public void SetUpMapOverlay()
    {
        for (int i = 0; i < MapSize.X * MapSize.Y; i++) { MapDisplayOverlayData[i] = 0; MapColourOverrideData[i] = null; }

        foreach (TowerBase tower in Towers)
        {
            Point towerPos = tower.GetPosition();
            int arrayIndex = towerPos.X + (towerPos.Y * MapSize.X);

            MapDisplayOverlayData[arrayIndex] = tower.GetDisplayID();
            if (IsSelling == true && Towers[TowerIndex].GetPosition() == towerPos) MapColourOverrideData[arrayIndex] = ConsoleColor.Red;
        }
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

    public void DisplayGameEnd()
    {

    }

    // Game Options
    public void SetGamePlacing()
    {
        IsPlacing = false;
        TowerSelectName = "None";
        placingPosX = 0;
        placingPosY = 0;
    }

    public void SetGameSelling()
    {
        IsSelling = true;
        TowerIndex = 0;
    }

    public void MoveTowerPlace(Point movePoint)
    {
        placingPosX = Math.Clamp(placingPosX + movePoint.X, 0, MapSize.X - 1);
        placingPosY = Math.Clamp(placingPosY + movePoint.Y, 0, MapSize.Y - 1);
    }

    public void PlaceTower()
    {
        IEnumerable<TowerData> getTowerData = GameData.AllTowerData.Select(n => n).Where(n => n.Name == GetTowerSelectName());
        if (getTowerData.Count() != 1) return;

        TowerData useTowerData = getTowerData.First();

        if (Money < useTowerData.Cost) return;
        Towers.Add(towerFactory.CreateTower(useTowerData, new Point(placingPosX, placingPosY)));
        Money -= useTowerData.Cost;

        SetGamePlacing();
    }

    public void SellTower()
    {
        Money += (int)(Towers[TowerIndex].GetCost() * 0.75f);
        Towers.RemoveAt(TowerIndex);

        IsSelling = false;
    }

    public void NextTowerIndex()
    {
        TowerIndex++;
        if (TowerIndex > Towers.Count - 1) TowerIndex = 0;
    }

    public void PreviousTowerIndex()
    {
        TowerIndex--;
        if (TowerIndex < 0) TowerIndex = Towers.Count - 1;
    }

    public void StartRound()
    {
        RoundData roundInfoShortcut = GameData.AllRoundData[CurrentRound - 1];

        int currentEnemies = roundInfoShortcut.TotalEnemies;
        float enemySpawnCooldown = 0.5f;

        int enemyInfoIndex = 0;
        float enemyInterval = roundInfoShortcut.Enemies[0].Interval;
        int enemyID = roundInfoShortcut.Enemies[0].EnemyID;
        int enemyAmount = roundInfoShortcut.Enemies[0].Amount;

        while (currentEnemies > 0 && Lives > 0)
        {
            Console.Clear();
            SetUpMapOverlay();

            // Update Enemies
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Update(0.5f, TrackLength);
                if (Enemies[i].GetIsDead() == false) Enemies[i].SetPosition(TrackProgressToPosition[Enemies[i].GetCastedTrackProgress()]);

                if (Enemies[i].GetIsDead() == true)
                {
                    if (Enemies[i].GetTrackProgress() >= TrackLength) Lives -= Enemies[i].GetDamage();
                    else                                              Money += Enemies[i].GetMoneyReward();

                    if (Lives <= 0) continue;

                    Enemies.RemoveAt(i);
                    currentEnemies--;
                    i--;
                }
                else
                {
                    MapDisplayOverlayData[TrackProgressToIndex[Enemies[i].GetCastedTrackProgress()]] = Enemies[i].GetDisplayID();
                }
            }

            // Update towers
            for (int i = 0; i < Towers.Count; i++)
            {
                Towers[i].ReduceCooldown(0.5f);
                bool canAttack = Towers[i].CanAttack();

                if (canAttack)
                {
                    Enemy? findEnemy = Towers[i].FindFurthestEnemy(ref Enemies);
                    if (findEnemy == null) continue;

                    Towers[i].Attack(ref findEnemy);
                    Towers[i].AttackDisplay(ref MapDisplayOverlayData, MapSize, TrackProgressToPosition[findEnemy.GetCastedTrackProgress()]);
                    Towers[i].ResetCooldown();
                }
            }

            // Spawn enemies
            enemySpawnCooldown -= 0.5f;
            if (enemySpawnCooldown <= 0 && enemyAmount != 0)
            {
                float extraBoost = -enemySpawnCooldown;

                Enemy addEnemy = new Enemy(GameData.AllEnemyData[enemyID]);
                if (extraBoost != 0f) addEnemy.Update(extraBoost, TrackLength);
                Enemies.Add(addEnemy);

                enemyAmount--;
                if (enemyAmount == 0 && enemyInfoIndex != roundInfoShortcut.Enemies.Count - 1)
                {
                    enemyInfoIndex++;

                    enemyInterval = roundInfoShortcut.Enemies[enemyInfoIndex].Interval;
                    enemyID = roundInfoShortcut.Enemies[enemyInfoIndex].EnemyID;
                    enemyAmount = roundInfoShortcut.Enemies[enemyInfoIndex].Amount;
                }

                enemySpawnCooldown += enemyInterval;
            }

            if (currentEnemies == 0) continue;

            DisplayMap();
            DisplayGameInfo();

            Thread.Sleep(500);
        }

        if (CurrentRound == FinalRound || Lives <= 0)
        {
            GameEnd = true;
            return;
        }

        Money += GameData.AllRoundData[CurrentRound - 1].MoneyBonus;
        if (CurrentRound < FinalRound) CurrentRound++;
    }

    // Get and Set
    public bool GetIsPlacing() { return IsPlacing; }
    public string GetTowerSelectName() { return TowerSelectName; }
    public bool GetIsSelling() { return IsSelling; }
    public int GetTowerIndex() { return TowerIndex; }
    public int GetLives() { return Lives; }
    public bool GetGameEnd() { return GameEnd; }
    public int GetCurrentRound() { return CurrentRound; }

    public void SetIsPlacing(bool value) { IsPlacing = value; }
    public void SetTowerSelectName(string name) { TowerSelectName = name; }
    public void SetIsSelling(bool value) { IsSelling = value; }
}