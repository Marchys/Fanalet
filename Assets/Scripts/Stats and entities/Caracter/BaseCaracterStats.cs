public class BaseCaracterStats 
{
    public BaseCaracterStats()
    {
        EntityName = "";
        EntityDescription = "";
        Attack = 0;
        Life = 0;
        MaxLife = 0;
        BaseSpeed = 0;
        CurrentSpeed = 0;
        AttackCadence = 0;
        RedHearts = 0;
        BlueHearts = 0;
        YellowHearts = 0;
        OldTools = false;
    }

    //Entity Data
    public string EntityName { get; set; }

    public string EntityDescription { get; set; }

    //Entity propieties
    public int Attack { get; set; }

    public int Life { get; set; }

    public int MaxLife { get; set; }

    public float BaseSpeed { get; set; }

    public float CurrentSpeed { get; set; }

    public float AttackCadence { get; set; }

    //items quantity

    public int YellowHearts { get; set; }
    public int RedHearts { get; set; }
    public int BlueHearts { get; set; }
    public bool OldTools { get; set; }

}
