using System.Drawing;

class TowerBase
{
    private byte DisplayID;
    private Point Position;

    private int Damage;
    private int Range;
    private float AttackCooldown;
    private float CurrentCooldown;

    public TowerBase(TowerData useTowerData, Point setPosition)
    {
        DisplayID = useTowerData.DisplayID;
        Position = setPosition;
        Damage = useTowerData.Damage;
        Range = useTowerData.Range;
        AttackCooldown = useTowerData.AttackCooldown;
        CurrentCooldown = useTowerData.AttackCooldown;
    }

    // Base Functions
    public bool CanAttack(float interval)
    {
        CurrentCooldown -= interval;

        if (CurrentCooldown <= 0)
        {
            CurrentCooldown += AttackCooldown;
            return true;
        }

        return false;
    }

    public virtual void Attack() { }

    // Get and Set
    public byte GetDisplayID() { return DisplayID; }
    public Point GetPosition() { return Position; }
    public int GetDamage() { return Damage; }
    public int GetRange() { return Range; }
    public float GetAttackCooldown() { return AttackCooldown; }
    public float GetCurrentCooldown() { return CurrentCooldown; }
}