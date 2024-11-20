class Enemy
{
    private byte DisplayID;

    private int Health;
    private float Speed;
    private int Damage;

    private float TrackProgress;
    private bool IsDead;

    public Enemy(EnemyData useEnemyData)
    {
        DisplayID = useEnemyData.DisplayID;
        Health = useEnemyData.Health;
        Speed = useEnemyData.Speed;
        Damage = useEnemyData.Damage;

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

    // Get and Set
    public byte GetDisplayID() { return DisplayID; }
    public int GetHealth() { return Health; }
    public float GetSpeed() { return Speed; }
    public float GetDamage() { return Damage; }
    public float GetTrackProgress() { return TrackProgress; }
    public bool GetIsDead() { return IsDead; }

    public int GetCastedTrackProgress() { return (int)Math.Round(TrackProgress); }
}