struct EnemyRoundInfo
{
    public int EnemyID { get; }
    public int Amount { get; }
    public float Interval { get; }

    public EnemyRoundInfo(int setEnemyID, int setAmount, float setInterval)
    {
        EnemyID = setEnemyID;
        Amount = setAmount;
        Interval = setInterval;
    }
}