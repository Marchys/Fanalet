public class EnemySlugStats : EnemyStats
{	
    public EnemySlugStats()
    {
        EntityName = "Slug";
        EntityDescription = "It's a slug";
        Level = 0;
        Attack = 2;
        Life = 4;
        MaxLife = 4;
        BaseSpeed = BaseSpeed * 0.6f;
        AgroSpeed = BaseSpeed * 1.1f;
        CurrentSpeed = BaseSpeed;
        Mass = 40;	
	}	
}
