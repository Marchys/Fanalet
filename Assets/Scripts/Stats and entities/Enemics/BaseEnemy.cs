using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviourEx, IVulnerable<int>, IMort, IDoAtack, IHandle<StopMessage>, IHandle<PlayerDeathMessage>
{

    public GameObject ParticleDeath;
    protected EnemyStats character;
    protected bool estat_stop = false;
    protected Transform ownTransform;
    protected Seeker seeker;
    protected float modPosY;
    protected Protas ProtaG;
    protected GameObject prota;
    protected GameObject or_point;
    protected Animator anim;
    new protected SpriteRenderer renderer;
    protected Sorting_sprites layerSorting;
    protected Rigidbody2D ownRigidbody2D;

    //public SpriteRenderer sprite_ren_pro;
    //public SpriteRenderer sprite_ren_en;

    protected virtual void Start()
    {
        //sprite_ren_en = GetComponent<SpriteRenderer>();
        prota = GameObject.FindWithTag("Prota");
        //sala_trans = transform.parent.parent.parent;
        //mapa_sala = sala_trans.GetComponent<Mapa_sala>();            
        ProtaG = prota.GetComponent<Protas>();
        //sprite_ren_pro = prota.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        ownTransform = GetComponent<Transform>();
        renderer = GetComponent<SpriteRenderer>();
        ownRigidbody2D = GetComponent<Rigidbody2D>();
        if (!character.Immortal)
        {
            layerSorting = GetComponent<Sorting_sprites>();
            //set default values;
            layerSorting.enabled = false;
        }

    }

    public void Mal(int mal)
    {
        if (!character.Immortal)
        {
            //StopCoroutine("Flash_red");
            //StartCoroutine("Flash_red");
            character.Life -= mal;
            if (character.Life <= 0) Mort();
        }

    }

    IEnumerator Flash_red()
    {
        var alCol = 0f;
        renderer.color = new Color(1f, alCol, alCol, 1F);
        while (true)
        {
            if (alCol < 1f)
            {
                renderer.color = new Color(1f, alCol += 1f * Time.deltaTime, alCol += 1f * Time.deltaTime, 1F);
            }
            yield return null;
        }

    }

    public void Mort()
    {
        GameObject itemToSpawnGameObject = null;
        switch (character.Level)
        {
            case 0:
                itemToSpawnGameObject = ItemDictionary.Generar["OilBottle"];
                break;
            case 1:
                itemToSpawnGameObject = ItemDictionary.Generar["RedHeart"];
                break;
            case 2:
                itemToSpawnGameObject = ItemDictionary.Generar["BlueHeart"];
                break;
            case 3:
                itemToSpawnGameObject = ItemDictionary.Generar["YellowHeart"];
                break;
            default:
                break;

        }

        Instantiate(ParticleDeath, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        if (itemToSpawnGameObject != null)
        {
            Instantiate(itemToSpawnGameObject, new Vector3(transform.position.x, transform.position.y, Random.Range(0.000001F, 0.0001F)), Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public virtual void FerMal()
    {
        Debug.Log("faig");
    }


    public virtual void Handle(StopMessage message)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Handle(PlayerDeathMessage message)
    {
        throw new System.NotImplementedException();
    }
}
