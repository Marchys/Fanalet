public class BaseCaracterStats {
   
    private float _baseSpeed = 5.5f;

    protected string EntityName { get; set; }

    protected string EntityDescription { get; set; }

    protected int Attack { get; set; }

    public int OiLife { get; set; }

    public int MaxOiLife { get; set; }

    public float BaseSpeed
    {
        get { return _baseSpeed; }
        set { _baseSpeed = value; }
    }

    public int Agility { get; set; }

    protected float AttackCadence { get; set; }

    protected float ResKnockback { get; set; }

    //items quantity

    public int YellowHearts { get; set; }
    public int RedHearts { get; set; }
    public int BlueHearts { get; set; }

}
