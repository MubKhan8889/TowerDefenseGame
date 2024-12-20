﻿using System.Drawing;
using System.Text.Json;

// --== MISC ==-- //
Data GameData = new Data();

// --== DATA ==-- //
List<MapWonData> mapWonData = new List<MapWonData>();

// This is to create a file for data to display what difficulties have been beaten for each map
if (File.Exists("JSONDATA/MapWonData.json") == false) File.WriteAllText("JSONDATA/MapWonData.json", "");
string mapWonJSON = File.ReadAllText("JSONDATA/MapWonData.json");

if (mapWonJSON != "")
{
    JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

    var mapWonDeserialise = JsonSerializer.Deserialize<List<MapWonData>>(mapWonJSON, jsonOptions);
    foreach (var data in mapWonDeserialise) mapWonData.Add(data);
}
else
{
    foreach (MapData mapData in GameData.AllMapData) mapWonData.Add(new MapWonData(mapData.Name, false, false, false));

    JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
    jsonOptions.WriteIndented = true;

    var mapWonSerialise = JsonSerializer.Serialize(mapWonData, jsonOptions);
    File.WriteAllText("JSONDATA/MapWonData.json", mapWonSerialise);
}

// --== STATES ==-- //
AppState appState = AppState.Menu;
GameState gameState = GameState.Preparation;

bool choosingGame = false;
int selectedMapIndex = 0;
Difficulty selectedDifficulty = Difficulty.Normal;

// --== CLASSES ==-- //
ConsoleDisplay consoleDisplay = new ConsoleDisplay();
Validator validator = new Validator();
Game game = new Game();

// This is so the user input can be checked by lists to check if they typed in a valid input
List<string> acceptableAppStates = new List<string> { "p", "e" }; // Play, Exit
List<string> acceptableGameStates = new List<string> { "p", "s", "r" }; // Place, Sell, Start
List<string> acceptableMoveChoices = new List<string> { "w", "a", "s", "d" };
List<string> acceptablePlaceChoices = new List<string> { "n", "c" }; // Cancel, Confirm
List<string> acceptableTowerChoices = new List<string> { "n" };  // None
List<string> acceptableSellChoices = new List<string> { "n", "p", "c", "s" }; // Next, Previous, Confirm, Cancel
List<string> acceptableDifficultyChoices = new List<string> { "e", "n", "h" }; // Easy, Normal, Hard
List<string> acceptableMapChoices = new List<string> { };
for (int i = 0; i < GameData.AllTowerData.Count; i++) acceptableTowerChoices.Add(Convert.ToString(i + 1));
for (int i = 0; i < GameData.AllMapData.Count; i++) acceptableMapChoices.Add(Convert.ToString(i + 1));

// This is to convert the user input to these enums
Dictionary<string, AppState> inputToAppState = new Dictionary<string, AppState>
{
    { "p", AppState.Game },
    { "e", AppState.Exit }
};

Dictionary<string, GameState> inputToGameState = new Dictionary<string, GameState>
{
    { "p", GameState.Placing },
    { "s", GameState.Selling },
    { "r", GameState.Round }
};

Dictionary<string, Difficulty> inputToDifficulty = new Dictionary<string, Difficulty>
{
    { "e", Difficulty.Easy },
    { "n", Difficulty.Normal },
    { "h", Difficulty.Hard }
};

// --== THE APPLICATION ==-- //
while (appState != AppState.Exit)
{
    Console.Clear();

    if (appState == AppState.Menu) MainMenuStateFunc();
    else if (appState == AppState.Game) GameStateFunc();
    else if (appState == AppState.GameEnd) GameEndFunc();
}

// --== MAIN MENU FUNCTION ==-- //
void MainMenuStateFunc()
{
    if (choosingGame == false) MainMenuStateTitleScreen();
    else if (choosingGame == true) MainMenuStateChoosingGame();
}

void MainMenuStateTitleScreen()
{
    consoleDisplay.Title("Tower Defence Game");

    consoleDisplay.List(new List<string>()
    {
        "P - Start a game",
        "E - Exit application"
    });

    Console.Write("\n");
    string input = StringInput("Option: ").ToLower();

    if (validator.ValidStringOption(input, acceptableAppStates))
    {
        if (input == "e") appState = AppState.Exit;
        else if (input == "p") choosingGame = true;
    }
    else
    {
        if (input == "") consoleDisplay.Error("Input is null, please enter something.");
        else             consoleDisplay.Error("Input is incorrect, please enter one of the options above.");
        Console.ReadLine();

        return;
    }
}

void MainMenuStateChoosingGame()
{
    consoleDisplay.Title("Tower Defence Game");
    Console.WriteLine("Type in the number of the map you want to choose, and the letter of the difficulty you want to play the map on");

    Console.Write("\n");
    consoleDisplay.SubTitle("Maps");
    for (int i = 0; i < GameData.AllMapData.Count; i++) Console.WriteLine(acceptableMapChoices[i] + " - " + GameData.AllMapData[i].Name);

    Console.Write("\n");
    consoleDisplay.SubTitle("Difficulties");
    consoleDisplay.List(new List<string>()
    {
        "E - Easy",
        "N - Normal",
        "H - Hard"
    });

    Console.Write("\n");
    consoleDisplay.SubTitle("Wins");
    foreach(MapWonData getMapWonData in mapWonData) consoleDisplay.MapWin(getMapWonData);

    Console.Write("\n");
    string input = StringInput("Map & Difficulty: ").ToLower();

    if (input.Length != 2)
    {
        consoleDisplay.Error("Input has to be exactly 2 letters long.");
        Console.ReadLine();

        return;
    }

    string mapChoice = Convert.ToString(input[0]);
    string diffChoice = Convert.ToString(input[1]);

    if (validator.ValidStringOption(mapChoice, acceptableMapChoices) && validator.ValidStringOption(diffChoice, acceptableDifficultyChoices))
    {
        choosingGame = false;

        selectedMapIndex = Convert.ToInt32(mapChoice) - 1;
        selectedDifficulty = inputToDifficulty[diffChoice];

        game.SetMapData(selectedMapIndex, selectedDifficulty);
        appState = AppState.Game;
    }
    else
    {
        if (input == "") consoleDisplay.Error("Input is null, please enter something.");
        else             consoleDisplay.Error("Input is incorrect, the first letter is the map number and the second letter is the difficulty choice.");
        Console.ReadLine();

        return;
    }
}

// --== GAME STATES ==-- //
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

// --== PREPARATION ==-- //
void GameStatePreparation()
{
    game.SetUpMapOverlay();
    game.DisplayMap();
    game.DisplayGameInfo();

    Console.Write("\n");
    consoleDisplay.SubTitle("Option");
    consoleDisplay.List(new List<string>()
    {
        "P - Place a tower",
        "S - Sell a tower",
        "R - Start the round"
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
        if (input == "") consoleDisplay.Error("Input is null, please enter something.");
        else             consoleDisplay.Error("Input is incorrect, please enter one of the options above.");
        Console.ReadLine();

        return;
    }
}

// --== PLACING ==-- //
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
    for (int i = 0; i < GameData.AllTowerData.Count; i++) consoleDisplay.TowerOption(GameData.AllTowerData[i], i + 1);
    Console.WriteLine("N - Cancel Placement");
    Console.Write("\n");

    string input = StringInput("Tower: ").ToLower();

    if (validator.ValidStringOption(input, acceptableTowerChoices))
    {
        if (input == "n")
        {
            gameState = GameState.Preparation;
            return;
        }

        int towerIndex = Convert.ToInt32(input);
        if (game.CanAffordTower(towerIndex) == false)
        {
            consoleDisplay.Error("You don't have enough money to afford this tower.");
            Console.ReadLine();

            return;
        }

        game.SetIsPlacing(true);
        game.SetTowerSelectIndex(towerIndex);

        return;
    }
    else
    {
        if (input == "") consoleDisplay.Error("Input is null, please enter something.");
        else             consoleDisplay.Error("Input is incorrect, please enter one of the options above.");
        Console.ReadLine();

        return;
    }
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
        "C - Place tower",
        "N - Cancel tower placement"
    });
    Console.Write("\n");
    
    string input = StringInput("Move: ").ToLower();

    if (input == "")
    {
        consoleDisplay.Error("Input is null, please enter something.");
        Console.ReadLine();

        return;
    }

    string inputDirection = Convert.ToString(input[input.Length - 1]);
    if (validator.ValidStringOption(inputDirection, acceptableMoveChoices))
    {
        string inputMovement = input.Substring(0, input.Length - 1);
        Point movement = new Point(0, 0);

        try
        {
            switch (inputDirection)
            {
                case "w": movement = new Point(0, -1 * Convert.ToInt32(inputMovement)); break;
                case "a": movement = new Point(-1 * Convert.ToInt32(inputMovement), 0); break;
                case "s": movement = new Point(0, 1 * Convert.ToInt32(inputMovement)); break;
                case "d": movement = new Point(1 * Convert.ToInt32(inputMovement), 0); break;
            }
        }
        catch
        {
            if (inputMovement == "") consoleDisplay.Error("You need to have a number first before the letter.");
            else                     consoleDisplay.Error("You should only have 1 letter for the direction.");
            Console.ReadLine();

            return;
        }

        if (movement == new Point(0, 0))
        {
            consoleDisplay.Error("Letter is invalid, please enter one of the letters last.");
            Console.ReadLine();

            return;
        }

        game.MoveTowerPlace(movement);
    }
    else if (validator.ValidStringOption(input, acceptablePlaceChoices))
    {
        if (input == "n")
        {
            gameState = GameState.Preparation;

            game.SetIsPlacing(false);
            return;
        }
        else if (input == "c")
        {
            gameState = GameState.Preparation;

            game.PlaceTower();
            return;
        }
    }
    else
    {
        consoleDisplay.Error("Input is incorrect, please enter one of the options above.");
        Console.ReadLine();

        return;
    }
}

// --== SELLING ==-- //
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
        "N - Next Index",
        "P - Previous Index",
        "C - Sell Tower",
        "S - Cancel tower selling"
    });

    Console.Write("\n");
    string input = StringInput("Option: ").ToLower();

    if (validator.ValidStringOption(input, acceptableSellChoices))
    {
        if (input == "n")
        {
            game.NextTowerIndex();
        }
        else if (input == "p")
        {
            game.PreviousTowerIndex();
        }
        else if (input == "c")
        {
            gameState = GameState.Preparation;
            game.SellTower();
            return;
        }
        else if (input == "s")
        {
            gameState = GameState.Preparation;
            game.SetIsSelling(false);
            return;
        }
    }
    else
    {
        if (input == "") consoleDisplay.Error("Input is null, please enter something.");
        else             consoleDisplay.Error("Input is incorrect, please enter one of the options above.");
        Console.ReadLine();

        return;
    }
}

// --== ROUND PLAYOUT ==-- //
void GameStateRound()
{
    game.StartRound();

    if (game.GetGameEnd()) appState = AppState.GameEnd;
    else                   gameState = GameState.Preparation;
}

// --== GAME END ==-- //
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
        Console.WriteLine("You survived all the enemies that were after you, and therefore your base is still intact.");

        MapWonData getMapWonData = mapWonData[selectedMapIndex];
        
        switch (selectedDifficulty)
        {
            case Difficulty.Easy:
                mapWonData[selectedMapIndex] = new MapWonData(getMapWonData.MapName, true, getMapWonData.NormalWin, getMapWonData.HardWin);
                break;

            case Difficulty.Normal:
                mapWonData[selectedMapIndex] = new MapWonData(getMapWonData.MapName, getMapWonData.EasyWin, true, getMapWonData.HardWin);
                break;

            case Difficulty.Hard:
                mapWonData[selectedMapIndex] = new MapWonData(getMapWonData.MapName, getMapWonData.EasyWin, getMapWonData.NormalWin, true);
                break;
        }

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        jsonOptions.WriteIndented = true;

        string winDataJSON = JsonSerializer.Serialize(mapWonData, jsonOptions);
        File.WriteAllText("JSONDATA/MapWonData.json", winDataJSON);
    }

    Console.ReadLine();
    appState = AppState.Menu;
    gameState = GameState.Preparation;
}

// --== OTHER FUNCTIONS ==-- //
static string StringInput(string prompt)
{
    Console.Write(prompt);
    string input = Console.ReadLine();
    return input;
}