public class EnemyMinotaurStats : EnemyStats
{
    public EnemyMinotaurStats()
    {
        EntityName = "Minotauro";
        EntityDescription = "El Guardián inmortal del Tártaro";
        Level = 10;
        Attack = 10;
        Life = 666;
        BaseSpeed = BaseSpeed * 1f;
        Mass = 200;
        Immortal = true;
    }
}
