struct RoundData
{
    public int MoneyBonus { get; set; }
    public int TotalEnemies { get; set; }
    public List<EnemyRoundInfo> Enemies { get; set; }

    public RoundData(int setMoneyBonus, List<EnemyRoundInfo> setEnemies)
    {
        MoneyBonus = setMoneyBonus;
        Enemies = setEnemies;

        foreach (EnemyRoundInfo info in Enemies) TotalEnemies += info.Amount;
    }
}