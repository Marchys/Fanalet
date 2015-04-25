public class EnemyBossSlugStats : EnemyStats
{
    public EnemyBossSlugStats()
    {
        EntityName = "BossSlug";
        EntityDescription = "It's a Boss slug";
        Level = 4;
        Attack = 10;
        Life = 50;
        MaxLife = 50;
        BaseSpeed = BaseSpeed * 1f;
        AgroSpeed = BaseSpeed * 2.5f;
        CurrentSpeed = BaseSpeed;
        Mass = 60;
    }
}
