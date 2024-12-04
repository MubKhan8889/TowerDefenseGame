using System.Drawing;

class TowerCannon : TowerBase
{
    public TowerCannon(TowerData useTowerData, Point setPosition) : base(useTowerData, setPosition) { }

    public override bool DoAttack(ref List<Enemy> refEnemies)
    {
        return false;
    }

    public override void AttackDisplay(ref byte[] MapDisplayOverlayData, Point mapSize)
    {

    }
}