struct TowerData
{
    public string Name { get; }
    public TowerType Type { get; }
    public byte DisplayID { get; }
    public int Cost { get; }
    public int Damage { get; }
    public int Range { get; }
    public float AttackCooldown { get; }

    public TowerData(string setName, byte setDisplayID, TowerType setType, int setCost, int setDamage, int setRange, float setAttackCooldown)
    {
        Name = setName;
        DisplayID = setDisplayID;
        Type = setType;
        Cost = setCost;
        Damage = setDamage;
        Range = setRange;
        AttackCooldown = setAttackCooldown;
    }
}