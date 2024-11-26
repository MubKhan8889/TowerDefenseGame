struct RoundData
{
    public int MoneyBonus { get; }
    public int TotalEnemies { get; }
    public List<EnemyRoundInfo> Enemies { get; }

    public RoundData(int setMoneyBonus, List<EnemyRoundInfo> setEnemies)
    {
        MoneyBonus = setMoneyBonus;
        Enemies = setEnemies;

        foreach (EnemyRoundInfo info in Enemies) TotalEnemies += info.Amount;
    }
}