using System.Drawing;

class TowerCannon : TowerBase
{
    public TowerCannon(TowerData useTowerData, Point setPosition) : base(useTowerData, setPosition) { }

    public override void Attack(ref Enemy enemy)
    {

    }

    public override void AttackDisplay(ref byte[] MapDisplayOverlayData, Point mapSize, Point center)
    {

    }
}