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
        BaseSpeed = 5.5f;
        Mass = 0;
    }

    public string EntityName { get; set; }

    public string EntityDescription { get; set; }

    public int Level { get; set; }

    public bool Immortal { get; set; }

    public int Attack { get; set; }

    public int Life { get; set; }

    public float BaseSpeed{ get; set; }

    public float Mass { get; set; }
}
