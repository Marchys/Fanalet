public class Enemigo_Minotauro_Stats : Enemigo_Esser_Stats
{
    public Enemigo_Minotauro_Stats()
    {
        EntityName = "Minotauro";
        EntityDescription = "El Guardián inmortal del Tártaro";
        Attack = 10;
        Life = 666;
        BaseSpeed = BaseSpeed * 1f;
        AttackCadence = 500;
        Mass = 200;
        Immortal = true;
    }
}
