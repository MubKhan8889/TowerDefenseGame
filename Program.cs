// States
AppState appState = AppState.Menu;

// Misc
// For the mapTileDisplay
//      0 = nothing
//      
Dictionary<byte, TileDisplay> mapTileDisplay = new Dictionary<byte, TileDisplay>
{
    { 0, new TileDisplay() },
    { 254, new TileDisplay("##") },
    { 254, new TileDisplay("[]", ConsoleColor.Gray) },
};

List<string> acceptableGameStates = new List<string> { "Play", "Exit" };

Dictionary<string, AppState> inputToGameState = new Dictionary<string, AppState>
{
    { "play", AppState.Game },
    { "exit", AppState.Exit }
};

// Data


// Classes
ConsoleDisplay consoleDisplay = new ConsoleDisplay();
Validator validator = new Validator();

// --== THE APPLICATION ==-- //
while (appState != AppState.Exit)
{
    Console.Clear();

    if (appState == AppState.Menu) MainMenu();
}

// Game Function
void MainMenu()
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

    if (validator.ValidStringOption(input, acceptableGameStates))
    {
        appState = inputToGameState[input];
    }
    else
    {
        Console.WriteLine("Input is incorrect, please enter one of the options above.");
        Console.ReadLine();

        return;
    }
}

// Other Functions
static string StringInput(string prompt)
{
    Console.Write(prompt);
    string input = Console.ReadLine();
    return input;
}