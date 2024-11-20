struct EnemyData
{
    public byte DisplayID { get; }
    public int Health { get; }
    public float Speed { get; }
    public int Damage { get; }

    public EnemyData(byte setDisplay, int setHealth, float setSpeed, int setDamage)
    {
        DisplayID = setDisplay;
        Health = setHealth;
        Speed = setSpeed;
        Damage = setDamage;
    }
}