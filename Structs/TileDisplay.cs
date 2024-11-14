struct TileDisplay
{
    public string Display { get; }
    public ConsoleColor Color { get; }

    public TileDisplay(string setDisplay, ConsoleColor setColor = ConsoleColor.White)
    {
        Display = setDisplay;
        Color = setColor;
    }

    public TileDisplay()
    {
        Display = "  ";
        Color = ConsoleColor.White;
    }
}