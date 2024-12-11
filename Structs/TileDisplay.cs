struct TileDisplay
{
    public string Display { get; set; }
    public ConsoleColor Color { get; set; }

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