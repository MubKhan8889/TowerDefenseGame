struct EnemyData
{
    public byte DisplayID { get; }
    public int Health { get; }
    public float Speed { get; }
    public int Damage { get; }
    public int MoneyReward { get; }

    public EnemyData(byte setDisplay, int setHealth, float setSpeed, int setDamage, int setMoneyReward)
    {
        DisplayID = setDisplay;
        Health = setHealth;
        Speed = setSpeed;
        Damage = setDamage;
        MoneyReward = setMoneyReward;
    }
}