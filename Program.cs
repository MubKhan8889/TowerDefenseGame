using System.Drawing;

// Misc
Data GameData = new Data();

// States
AppState appState = AppState.Menu;
GameState gameState = GameState.Preparation;

// Classes
ConsoleDisplay consoleDisplay = new ConsoleDisplay();
Validator validator = new Validator();
Game game = new Game();

List<string> acceptableAppStates = new List<string> { "Play", "Exit" };
List<string> acceptableGameStates = new List<string> { "Place", "Sell", "Start" };
List<string> acceptableMoveChoices = new List<string> { "W", "A", "S", "D" };
List<string> acceptablePlaceChoices = new List<string> { "Cancel", "Confirm" };
List<string> acceptableTowerChoices = new List<string> { "None" };
List<string> acceptableSellChoices = new List<string> { "Next", "Previous", "Confirm", "Cancel" };
foreach (TowerData getTowerData in GameData.AllTowerData) acceptableTowerChoices.Add(getTowerData.Name.ToLower());

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
    else if (appState == AppState.GameEnd) GameEndFunc();
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

    Console.Write("\n");
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

        if (appState == AppState.Game) game.SetMapData(0);
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
    game.SetUpMapOverlay();
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

    Console.Write("\n");
    string input = StringInput("Option: ").ToLower();

    if (validator.ValidStringOption(input, acceptableGameStates))
    {
        gameState = inputToGameState[input];

        if (gameState == GameState.Placing) game.SetGamePlacing();
        if (gameState == GameState.Selling) game.SetGameSelling();
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
    game.SetUpMapOverlay();
    game.DisplayMap();
    game.DisplayGameInfo();

    if (game.GetIsPlacing() == false) GameStatePlacingTowerChoose();
    else if (game.GetIsPlacing() == true) GameStatePlacingTowerPlace();
}

void GameStatePlacingTowerChoose()
{
    Console.Write("\n");
    consoleDisplay.SubTitle("Towers");
    foreach (TowerData getTowerData in GameData.AllTowerData) consoleDisplay.TowerOption(getTowerData);
    consoleDisplay.List(new List<string>()
    {
        "None - Cancel Placement",
        "Confirm - Place Tower"
    });

    string input = StringInput("Tower: ").ToLower();

    if (validator.ValidStringOption(input, acceptableTowerChoices))
    {
        if (input == "none")
        {
            gameState = GameState.Preparation;
            return;
        }

        IEnumerable<TowerData> findTower = GameData.AllTowerData.Select(n => n).Where(n => n.Name.ToLower() == input);
        if (findTower.Count() == 1)
        {
            game.SetIsPlacing(true);
            game.SetTowerSelectName(findTower.First().Name);
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
    consoleDisplay.List(new List<string>()
    {
        "W moves up",
        "A moves to the left",
        "S moves down",
        "D moves to the right",
        "Confirm - Place tower",
        "Cancel - Cancel tower placement"
    });
    Console.Write("\n");
    
    string input = StringInput("Move: ").ToLower();
    string inputDirection = Convert.ToString(input[input.Length - 1]);
    if (validator.ValidStringOption(inputDirection, acceptableMoveChoices))
    {
        string inputMovement = input.Substring(0, input.Length - 1);
        Point movement = new Point(0, 0);

        switch (inputDirection)
        {
            case "w": movement = new Point(0, -1 * Convert.ToInt32(inputMovement)); break;
            case "a": movement = new Point(-1 * Convert.ToInt32(inputMovement), 0); break;
            case "s": movement = new Point(0, 1 * Convert.ToInt32(inputMovement)); break;
            case "d": movement = new Point(1 * Convert.ToInt32(inputMovement), 0); break;
        }

        if (movement == new Point(0, 0))
        {
            Console.WriteLine("Letter is invalid, please enter one of the letters last.");
            Console.ReadLine();

            return;
        }

        game.MoveTowerPlace(movement);
    }
    else if (validator.ValidStringOption(input, acceptablePlaceChoices))
    {
        if (input == "cancel")
        {
            gameState = GameState.Preparation;

            game.SetIsPlacing(false);
            return;
        }
        else if (input == "confirm")
        {
            gameState = GameState.Preparation;

            game.PlaceTower();
            return;
        }
    }
    else
    {
        Console.WriteLine("Input is incorrect, please enter one of the options above.");
        Console.ReadLine();

        return;
    }
}

// Selling
void GameStateSelling()
{
    game.SetUpMapOverlay();
    game.DisplayMap();
    game.DisplayGameInfo();

    Console.Write("\n");
    consoleDisplay.SubTitle("Selling");
    consoleDisplay.Value("Tower Index", Convert.ToString(game.GetTowerIndex()));
    consoleDisplay.List(new List<string>()
    {
        "Next - Next Index",
        "Previous - Previous Index",
        "Confirm - Sell Tower",
        "Cancel - Cancel tower selling"
    });

    Console.Write("\n");
    string input = StringInput("Option: ").ToLower();

    if (validator.ValidStringOption(input, acceptableSellChoices))
    {
        if (input == "next")
        {
            game.NextTowerIndex();
        }
        else if (input == "previous")
        {
            game.PreviousTowerIndex();
        }
        else if (input == "confirm")
        {
            gameState = GameState.Preparation;
            game.SellTower();
            return;
        }
        else if (input == "cancel")
        {
            gameState = GameState.Preparation;
            game.SetIsSelling(false);
            return;
        }
    }
    else
    {
        Console.WriteLine("Input is incorrect, please enter one of the options above.");
        Console.ReadLine();

        return;
    }
}

// Round Playout
void GameStateRound()
{
    game.StartRound();

    if (game.GetGameEnd()) appState = AppState.GameEnd;
    else                   gameState = GameState.Preparation;
}

// Game End
void GameEndFunc()
{
    if (game.GetLives() <= 0)
    {
        consoleDisplay.GameEndText("Game Over");
        Console.WriteLine("You ran out of all lives, and therefore your base was destroyed.");
        Console.Write("\n");
        Console.WriteLine($"You only made it to round {game.GetCurrentRound()}.");
    }
    else
    {
        consoleDisplay.GameEndText("Game Won");
        Console.WriteLine("You survived all the enemies that were after you, and therefore your base is still intact");
    }

    Console.ReadLine();
    appState = AppState.Menu;
}

// Other Functions
static string StringInput(string prompt)
{
    Console.Write(prompt);
    string input = Console.ReadLine();
    return input;
}