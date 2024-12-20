﻿using System.Diagnostics;
using System.Drawing;


class ConsoleDisplay
{
    // Misc
    Data GameData = new Data();

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

    public void GameEndText(string text)
    {
        Console.WriteLine($"!!! {text.ToUpper()} !!!");
        for (int i = 0; i < text.Length + 8; i++) Console.Write("-");
        Console.Write("\n");
    }

    public void List(List<string> textList)
    {
        foreach (string text in textList) Console.WriteLine(text);
    }

    public void Tile(byte tileID, ConsoleColor? overrideColour = null)
    {
        Console.ForegroundColor = (overrideColour != null) ? (ConsoleColor)overrideColour : GameData.MapTileID[tileID].Color;
        Console.Write(GameData.MapTileID[tileID].Display);
    }

    public void Value(string valueName, string value, ConsoleColor displayColor = ConsoleColor.White)
    {
        Console.ForegroundColor = displayColor;
        Console.Write(valueName);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(": " + value);
    }

    public void TowerOption(TowerData useTowerData, int index)
    {
        Console.Write($"{index} - {useTowerData.Name} | ");
        Tile(useTowerData.DisplayID);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($" | Cost: {useTowerData.Cost}");
    }

    public void MapWin(MapWonData getMapWonData)
    {
        Console.Write($"{getMapWonData.MapName} | ");
        Console.ForegroundColor = (getMapWonData.EasyWin == true) ? ConsoleColor.DarkYellow : ConsoleColor.DarkGray;
        Console.Write("X");
        Console.ForegroundColor = (getMapWonData.NormalWin == true) ? ConsoleColor.Gray : ConsoleColor.DarkGray;
        Console.Write("X");
        Console.ForegroundColor = (getMapWonData.HardWin == true) ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
        Console.WriteLine("X");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("ERROR: " + message);
        Console.ForegroundColor = ConsoleColor.White;
    }
}