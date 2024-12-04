using System.Drawing;

class TowerBasic : TowerBase
{
    public TowerBasic(TowerData useTowerData, Point setPosition) : base(useTowerData, setPosition) { }

    public override bool DoAttack(ref List<Enemy> refEnemies)
    {
        Enemy? findEnemy = this.FindFurthestEnemy(ref refEnemies);
        if (findEnemy == null) return false;

        this.TargetPoint = findEnemy.GetPosition();
        findEnemy.TakeDamage(Damage);
        return true;
    }

    public override void AttackDisplay(ref byte[] MapDisplayOverlayData, Point mapSize)
    {
        int translateToIndex = this.TargetPoint.X + (mapSize.X * this.TargetPoint.Y);
        MapDisplayOverlayData[translateToIndex] = 253;
    }
}