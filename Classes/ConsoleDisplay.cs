class ConsoleDisplay
{
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
}