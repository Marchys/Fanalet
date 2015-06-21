using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour, IVulnerable<int>, IMort
{
    protected int life;
    public GameObject ParticleDeath;

    public enum DestructibleTypes
    {
        Barrel
    }

    public DestructibleTypes Destructibles;

    void Start()
    {
        switch (Destructibles)
        {
            case DestructibleTypes.Barrel:
                life = Constants.Destructibles.BarrelLife;
                break;
        }
    }

    public void Mal(int damagePoints)
    {

        life -= damagePoints;
        if (life <= 0) Mort();
    }

    public void Mort()
    {
        Instantiate(ParticleDeath, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        if (Destructibles == DestructibleTypes.Barrel)
        {
            if (Utils.ChanceTrue(60))
            {
                Instantiate(ItemDictionary.Generar["RandomOilBottle"], new Vector3(transform.position.x, transform.position.y, Random.Range(0.000001F, 0.0001F)), Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}
