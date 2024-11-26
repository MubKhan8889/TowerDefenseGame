using System.Drawing;

class Enemy
{
    private byte DisplayID;
    private Point Position;

    private int Health;
    private float Speed;
    private int Damage;

    private int MoneyReward;
    private float TrackProgress;
    private bool IsDead;

    public Enemy(EnemyData useEnemyData)
    {
        DisplayID = useEnemyData.DisplayID;
        Health = useEnemyData.Health;
        Speed = useEnemyData.Speed;
        Damage = useEnemyData.Damage;
        MoneyReward = useEnemyData.MoneyReward;

        TrackProgress = 0;
        IsDead = false;
    }

    // Base Functions
    public void Update(float dt, float trackLength)
    {
        if (IsDead == true) return;

        TrackProgress += Speed * dt;
        if (TrackProgress >= trackLength) IsDead = true;
    }

    public void TakeDamage(int takeDamage)
    {
        Health -= takeDamage;
        if (Health <= 0) IsDead = true;
    }

    // Get
    public byte GetDisplayID() { return DisplayID; }
    public Point GetPosition() { return Position; }
    public int GetHealth() { return Health; }
    public float GetSpeed() { return Speed; }
    public int GetDamage() { return Damage; }
    public int GetMoneyReward() { return MoneyReward; }
    public float GetTrackProgress() { return TrackProgress; }
    public bool GetIsDead() { return IsDead; }

    public int GetCastedTrackProgress() { return (int)Math.Round(TrackProgress); }

    // Set
    public void SetPosition(Point setPosition) { Position = setPosition; }
}