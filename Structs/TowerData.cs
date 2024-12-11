struct TowerData
{
    public string Name { get; set; }
    public TowerType Type { get; set; }
    public byte DisplayID { get; set; }
    public int Cost { get; set; }
    public int Damage { get; set; }
    public int Range { get; set; }
    public float AttackCooldown { get; set; }

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