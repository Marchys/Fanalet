public class EnemyYellowSlugStats : EnemyStats
{
    public EnemyYellowSlugStats()
    {
        EntityName = "YellowSlug";
        EntityDescription = "It's a yellow slug";
        Level = 3;
        Attack = 7;
        Life = 30;
        MaxLife = 30;
        BaseSpeed = BaseSpeed * 1f;
        AgroSpeed = BaseSpeed * 2.5f;
        CurrentSpeed = BaseSpeed;
        Mass = 40;
    }
}
