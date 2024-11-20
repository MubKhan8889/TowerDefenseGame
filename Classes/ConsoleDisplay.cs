using System.Diagnostics;
using System.Drawing;


class ConsoleDisplay
{
    // Info
    Dictionary<byte, TileDisplay> mapTileIDs = new Dictionary<byte, TileDisplay>
    {
        { 0, new TileDisplay() },
        { 11, new TileDisplay(",,", ConsoleColor.Green) },
        { 31, new TileDisplay(",^", ConsoleColor.DarkGreen) },
        { 61, new TileDisplay("(|") },
        { 62, new TileDisplay("U)") },
        { 254, new TileDisplay("##") },
        { 255, new TileDisplay("[]", ConsoleColor.Gray) },
    };

    // Functions
    public void Title(string text)
    {
        for (int i = 0; i < text.Length + 10; i++) Console.Write("-");
        Console.Write("\n");
        Console.WriteLine($"--== {text.ToUpper()} ==--");
        for (int i = 0; i < text.Length + 10; i++) Console.Write("-");
        Console.Write("\n");
    }

    public void SubTitle(string text)
    {
        Console.WriteLine($"-- {text.ToUpper()} --");
    }

    public void List(List<string> textList)
    {
        foreach (string text in textList) Console.WriteLine(text);
    }

    public void Tile(byte tileID, ConsoleColor? overrideColour = null)
    {
        Console.ForegroundColor = (overrideColour != null) ? (ConsoleColor)overrideColour : mapTileIDs[tileID].Color;
        Console.Write(mapTileIDs[tileID].Display);
    }

    public void Value(string valueName, string value, ConsoleColor displayColor = ConsoleColor.White)
    {
        Console.ForegroundColor = displayColor;
        Console.Write(valueName);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(": " + value);
    }

    public void TowerOption(TowerData useTowerData)
    {
        Console.Write(useTowerData.Name + " - ");
        Tile(useTowerData.DisplayID);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($" | Cost: {useTowerData.Cost}");
    }
}