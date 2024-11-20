using System.Drawing;

// States
AppState appState = AppState.Menu;
GameState gameState = GameState.Preparation;

// Data
List<MapData> allMapData = new List<MapData>
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
            new Point(19, 3),
            new Point(19, 9),
            new Point(2, 9),
            new Point(2, 15),
            new Point(19, 15)
        }
    )
};

List<TowerData> allTowerData = new List<TowerData>
{
    new TowerData("Basic",
        61,
        TowerType.Basic,
        200,
        10,
        6,
        4)
};

// Classes
ConsoleDisplay consoleDisplay = new ConsoleDisplay();
Validator validator = new Validator();
Game game = new Game();

// Misc
// For the mapTileDisplay
//      0 = Nothing
//      1X - 5X = Ground Display
//      6X - 10X = Tower Display
//      11X - 15X = Enemy Display
//      25X = Misc

List<string> acceptableAppStates = new List<string> { "Play", "Exit" };
List<string> acceptableGameStates = new List<string> { "Place", "Sell", "Play" };
List<string> acceptableMoveChoices = new List<string> { "W", "A", "S", "D" };
List<string> acceptableTowerChoices = new List<string> { "None" };
foreach (TowerData getTowerData in allTowerData) acceptableTowerChoices.Add(getTowerData.Name.ToLower());

Dictionary<string, AppState> inputToAppState = new Dictionary<string, AppState>
{
    { "play", AppState.Game },
    { "exit", AppState.Exit }
};

Dictionary<string, GameState> inputToGameState = new Dictionary<string, GameState>
{
    { "place", GameState.Placing },
    { "sell", GameState.Selling },
    { "start", GameState.Round }
};

// --== THE APPLICATION ==-- //
while (appState != AppState.Exit)
{
    Console.Clear();

    if (appState == AppState.Menu) MainMenuStateFunc();
    else if (appState == AppState.Game) GameStateFunc();
}

// Game Function
void MainMenuStateFunc()
{
    consoleDisplay.Title("Tower Defence Game");

    consoleDisplay.List(new List<string>()
    {
        "Play - Start a game",
        "Exit - Exit application"
    });

    string input = StringInput("Option: ").ToLower();

    if (input == "")
    {
        Console.WriteLine("Input is null, please enter something.");
        Console.ReadLine();

        return;
    }

    if (validator.ValidStringOption(input, acceptableAppStates))
    {
        appState = inputToAppState[input];

        if (appState == AppState.Game) game.SetMapData(allMapData[0]);
    }
    else
    {
        Console.WriteLine("Input is incorrect, please enter one of the options above.");
        Console.ReadLine();

        return;
    }
}

// Game States
void GameStateFunc()
{
    switch(gameState)
    {
        case GameState.Preparation: GameStatePreparation(); break;
        case GameState.Placing:     GameStatePlacing();     break;
        case GameState.Selling:     GameStateSelling();     break;
        case GameState.Round:       GameStateRound();       break;
    }
}

// Preparation
void GameStatePreparation()
{
    game.DisplayMap();
    game.DisplayGameInfo();

    Console.Write("\n");
    consoleDisplay.SubTitle("Option");
    consoleDisplay.List(new List<string>()
    {
        "Place - Place a tower",
        "Sell - Sell a tower",
        "Start - Start the round"
    });

    string input = StringInput("Option: ").ToLower();

    if (validator.ValidStringOption(input, acceptableGameStates))
    {
        gameState = inputToGameState[input];

        if (gameState == GameState.Placing) game.SetGamePlacing();
    }
    else
    {
        Console.WriteLine("Input is incorrect, please enter one of the options above.");
        Console.ReadLine();

        return;
    }
}

// Placing
void GameStatePlacing()
{
    game.DisplayMap();
    game.DisplayGameInfo();

    if (game.GetIsPlacing() == false) GameStatePlacingTowerChoose();
    else if (game.GetIsPlacing() == true) GameStatePlacingTowerPlace();
}

void GameStatePlacingTowerChoose()
{
    Console.Write("\n");
    consoleDisplay.SubTitle("Towers");
    foreach (TowerData getTowerData in allTowerData) consoleDisplay.TowerOption(getTowerData);
    Console.WriteLine("None - Cancel Placement");

    string input = StringInput("Tower: ").ToLower();

    if (validator.ValidStringOption(input, acceptableTowerChoices))
    {
        if (input == "none")
        {
            gameState = GameState.Preparation;
            return;
        }

        IEnumerable<string> findName = allTowerData.Select(s => s.Name);
        if (findName.Count() == 1)
        {
            game.SetIsPlacing(true);
            return;
        }
    }
    else
    {
        Console.WriteLine("Input is incorrect, please enter one of the options above.");
        Console.ReadLine();

        return;
    }

    Console.ReadLine();
}

void GameStatePlacingTowerPlace()
{
    Console.Write("\n");
    consoleDisplay.SubTitle("Placing");
    Console.WriteLine("Type any number, and then one of the letters of the following: W, A, S or D");
    Console.WriteLine("W moves up\nA moves to the left\nS moves down\nD moves to the right");
    
    string input = StringInput("Move: ");

    if (validator.ValidStringOption(Convert.ToString(input[input.Length - 1]), acceptableMoveChoices))
    {
        if (input == "none")
        {
            gameState = GameState.Preparation;
            return;
        }

        IEnumerable<string> findName = allTowerData.Select(s => s.Name);
        if (findName.Count() == 1)
        {
            game.SetIsPlacing(true);
            return;
        }
    }
    else
    {
        Console.WriteLine("Input is incorrect, please enter one of the options above.");
        Console.ReadLine();

        return;
    }

    Console.ReadLine();
}

// Selling
void GameStateSelling()
{

}

// Round Playout
void GameStateRound()
{

}

// Other Functions
static string StringInput(string prompt)
{
    Console.Write(prompt);
    string input = Console.ReadLine();
    return input;
}