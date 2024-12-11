using System.Drawing;

class TowerBase
{
    protected byte DisplayID;
    protected Point Position;
    protected Point TargetPoint;
    protected int Cost;

    protected int Damage;
    protected int Range;
    protected float AttackCooldown;
    protected float CurrentCooldown;

    public TowerBase(TowerData useTowerData, Point setPosition)
    {
        DisplayID = useTowerData.DisplayID;
        Position = setPosition;
        Cost = useTowerData.Cost;
        Damage = useTowerData.Damage;
        Range = useTowerData.Range;
        AttackCooldown = useTowerData.AttackCooldown;
        CurrentCooldown = useTowerData.AttackCooldown;
    }

    // Base Functions
    public void ReduceCooldown(float interval)
    {
        if (CurrentCooldown == 0f) return;

        CurrentCooldown -= interval;
        if (CurrentCooldown < 0f) CurrentCooldown = 0f;
    }

    public void ResetCooldown()
    {
        CurrentCooldown += AttackCooldown;
    }

    public bool CanAttack()
    {
        return CurrentCooldown <= 0;
    }

    public Enemy? FindFurthestEnemy(ref List<Enemy> allEnemies)
    {
        IEnumerable<Enemy> enemiesByTrackProgress = allEnemies.Where(e => e.DistanceFromPoint(this.Position) <= this.Range).OrderBy(e => e.GetTrackProgress());
        return (enemiesByTrackProgress.Count() != 0) ? enemiesByTrackProgress.Last() : null;
    }

    public void DamageEnemy(ref Enemy refEnemy)
    {
        refEnemy.TakeDamage(Damage);
    }

    // Override
    public virtual bool DoAttack(ref List<Enemy> refEnemies) { return false; }
    public virtual void AttackDisplay(ref byte[] MapDisplayOverlayData, Point mapSize) { }

    // Get and Set
    public byte GetDisplayID() { return DisplayID; }
    public Point GetPosition() { return Position; }
    public Point GetTargetPoint() { return TargetPoint; }
    public int GetCost() { return Cost; }
    public int GetDamage() { return Damage; }
    public int GetRange() { return Range; }
    public float GetAttackCooldown() { return AttackCooldown; }
    public float GetCurrentCooldown() { return CurrentCooldown; }
}