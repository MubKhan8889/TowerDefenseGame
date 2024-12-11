using System.Drawing;

class TowerCannon : TowerBase
{
    public TowerCannon(TowerData useTowerData, Point setPosition) : base(useTowerData, setPosition) { }

    public override bool DoAttack(ref List<Enemy> refEnemies)
    {
        Enemy? findEnemy = this.FindFurthestEnemy(ref refEnemies);
        if (findEnemy == null) return false;

        this.TargetPoint = findEnemy.GetPosition();
        IEnumerable<Enemy> enemiesToDamage = refEnemies.Where(e => e.DistanceFromPoint(this.TargetPoint) <= 3.25f);
        foreach (Enemy enemy in enemiesToDamage) findEnemy.TakeDamage(this.Damage);

        return true;
    }

    public override void AttackDisplay(ref byte[] MapDisplayOverlayData, Point mapSize)
    {
        int startX = (this.TargetPoint.X - 3 >= 0) ? this.TargetPoint.X - 3 : 0;
        int startY = (this.TargetPoint.Y - 3 >= 0) ? this.TargetPoint.Y - 3 : 0;

        int endX = (this.TargetPoint.X + 4 < mapSize.X) ? this.TargetPoint.X + 4 : mapSize.X - 1;
        int endY = (this.TargetPoint.Y + 4 < mapSize.Y) ? this.TargetPoint.Y + 4 : mapSize.Y - 1;

        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                int xDiff = this.TargetPoint.X - x;
                int yDiff = this.TargetPoint.Y - y;
                float magnitude = MathF.Sqrt(MathF.Pow(xDiff, 2) + MathF.Pow(yDiff, 2));

                if (magnitude <= 3.25)
                {
                    int translateToIndex = x + (mapSize.X * y);
                    MapDisplayOverlayData[translateToIndex] = 253;
                }
            }
        }
    }
}