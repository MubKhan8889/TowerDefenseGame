struct EnemyRoundInfo
{
    public int EnemyID { get; set; }
    public int Amount { get; set; }
    public float Interval { get; set; }

    public EnemyRoundInfo(int setEnemyID, int setAmount, float setInterval)
    {
        EnemyID = setEnemyID;
        Amount = setAmount;
        Interval = setInterval;
    }
}