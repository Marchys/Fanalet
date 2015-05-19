public class EnemyStats
{
    public EnemyStats()
    {
        EntityName = "";
        EntityDescription = "";
        Level = 0;
        Immortal = false;
        Attack = 0;
        Life = 0;
        MaxLife = 0;
        BaseSpeed = 5.5f;
        AgroSpeed = 0;
        CurrentSpeed = 0;
        Mass = 0;
    }


    public string EntityName { get; set; }

    public string EntityDescription { get; set; }

    public int Level { get; set; }

    public bool Immortal { get; set; }

    public int Attack { get; set; }

    public int Life { get; set; }

    public int MaxLife { get; set; }

    public float BaseSpeed { get; set; }

    public float AgroSpeed { get; set; }

    public float CurrentSpeed { get; set; }

    public float Mass { get; set; }
}
