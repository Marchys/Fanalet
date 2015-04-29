public class EnemyMinotaurStats : EnemyStats
{
    public EnemyMinotaurStats()
    {
        EntityName = "Minotauro";
        EntityDescription = "El Guardián inmortal del Tártaro";
        Level = 10;
        Attack = 10;
        Life = 666;
        MaxLife = 666;
        BaseSpeed = BaseSpeed * 1f;
        AgroSpeed = BaseSpeed * 1.5f;
        CurrentSpeed = BaseSpeed;
        Mass = 200;
        Immortal = true;
    }
}
