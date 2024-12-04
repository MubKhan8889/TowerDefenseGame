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
        CurrentCooldown -= interval;
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
        float furthestDistance = 0;
        int enemyIndex = -1;

        for (int i = 0; i < allEnemies.Count; i++)
        {
            Enemy enemy = allEnemies[i];

            int xDiff = enemy.GetPosition().X - Position.X;
            int yDiff = enemy.GetPosition().Y - Position.Y;
            float magnitude = MathF.Sqrt(MathF.Pow(xDiff, 2) + MathF.Pow(yDiff, 2));

            if (enemy.GetTrackProgress() > furthestDistance && magnitude < Range && enemy.GetIsDead() == false)
            {
                furthestDistance = enemy.GetTrackProgress();
                enemyIndex = i;
            }
        }

        return (enemyIndex != -1) ? allEnemies[enemyIndex] : null;
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