using System.Drawing;

class TowerFactory
{
    public TowerBase CreateTower(TowerData useTowerData, Point setPosition)
    {
        if (useTowerData.Type == TowerType.Cannon) return new TowerCannon(useTowerData, setPosition);

        return new TowerBasic(useTowerData, setPosition);
    }
}