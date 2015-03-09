using UnityEngine;

public class Propietats : MonoBehaviour{ 

    public float velocitat_base;
    public float agilitat;

    public Vector2 Pos_mod() 
    {
        return new Vector2(transform.position.x, transform.position.y - 0.50F);
    }
    
}
