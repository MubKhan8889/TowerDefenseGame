using System.Drawing;

class TowerBasic : TowerBase
{
    public TowerBasic(TowerData useTowerData, Point setPosition) : base(useTowerData, setPosition) { }

    public override void Attack(ref Enemy getEnemy)
    {
        getEnemy.TakeDamage(Damage);
    }

    public override void AttackDisplay(ref byte[] MapDisplayOverlayData, Point mapSize, Point center)
    {
        int translateToIndex = center.X + (mapSize.X * center.Y);
        MapDisplayOverlayData[translateToIndex] = 253;
    }
}