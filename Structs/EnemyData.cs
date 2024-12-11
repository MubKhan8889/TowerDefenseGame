struct EnemyData
{
    public byte DisplayID { get; set; }
    public int Health { get; set; }
    public float Speed { get; set; }
    public int Damage { get; set; }
    public int MoneyReward { get; set; }

    public EnemyData(byte setDisplay, int setHealth, float setSpeed, int setDamage, int setMoneyReward)
    {
        DisplayID = setDisplay;
        Health = setHealth;
        Speed = setSpeed;
        Damage = setDamage;
        MoneyReward = setMoneyReward;
    }
}