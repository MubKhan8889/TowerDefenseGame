struct DifficultyInfo
{
    public int Lives { get; set; }
    public int StartMoney { get; set; }
    public float MoneyMult { get; set; }
    public int FinalRound { get; set; }

    public DifficultyInfo(int setLives, int setStartMoney, float setMoneyMult, int setFinalRound)
    {
        Lives = setLives;
        StartMoney = setStartMoney;
        MoneyMult = setMoneyMult;
        FinalRound = setFinalRound;
    }
}