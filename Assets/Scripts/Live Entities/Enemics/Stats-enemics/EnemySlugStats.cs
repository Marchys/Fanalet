public class EnemySlugStats : EnemyStats
{	
    public EnemySlugStats()
    {
        EntityName = "Slug";
        EntityDescription = "It's a slug";
        Level = 0;
        Attack = 2;
        Life = 6;
        MaxLife = 6;
        BaseSpeed = BaseSpeed * 0.7f;
        AgroSpeed = BaseSpeed * 1.3f;
        CurrentSpeed = BaseSpeed;
        Mass = 40;	
	}	
}
