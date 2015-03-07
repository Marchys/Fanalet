using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Projectile : MonoBehaviour
{
    private string projectileName;
    private Color lightColor;

    private float damage;
    private float lifeSpan;
    private float speed;
    private Vector2 direction;

    private bool isDrawn;

    private Color particleColor;
    private bool particleSize;


    public string Name
    {
        get { return projectileName; }
        set { projectileName = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float LifeSpan
    {
        get { return lifeSpan; }
        set { lifeSpan = value; }
    }

    public float Speed
    {

        get { return speed; }
        set { speed = value; }
    }

    public Color LightColor
    {
        get { return lightColor; }
        set { lightColor = value; }
    }
    
    public void SetDirection(Vector2 tempDir)
    {
        direction = tempDir;
        GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);    
    }
   
}
