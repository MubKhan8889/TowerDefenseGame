class Validator
{
    public bool ValidStringOption(string input, List<string> options)
    {
        foreach (string option in options)
        {
            if (option.ToLower() == input.ToLower()) return true;
        }

        return false;
    }
}