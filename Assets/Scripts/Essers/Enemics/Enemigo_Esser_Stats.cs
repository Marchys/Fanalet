public class Enemigo_Esser_Stats 
{
    private string entityName;
    private string entityDescription;

    private bool immortal = false;
    private int attack;
    private int life;
    private float baseSpeed = 5.5f;   
    private float attackCadence;
    private float mass;

    public string EntityName
    { 
        get{return entityName;}
        set{entityName = value;}
    }

    public string EntityDescription
    {
        get { return entityDescription; }
        set { entityDescription = value; }
    }

    public bool Immortal
    {
        get { return immortal; }
        set { immortal = value; }
    }

    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    public float BaseSpeed
    {
        get { return baseSpeed; }
        set { baseSpeed = value; }
    }   

    public float AttackCadence
    {
        get { return attackCadence; }
        set { attackCadence = value; }
    }

    public float Mass
    {
        get { return mass; }
        set { mass = value; }
    }



}
