using System.Collections;
using UnityEngine;

public class bola : MonoBehaviour
{
    public GameObject rastre;
    public Vector2 dir;
    public Impacte en_xoc;

    void Start()
    {
        en_xoc = new Impacte(dir, 2, 4f);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemigo_tri" || col.gameObject.tag == "Escenari_coll")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Collider2D>().enabled = false;
            if (col.gameObject.tag == "Enemigo_tri")
            {
                col.transform.parent.BroadcastMessage("Impacte_mal", en_xoc, SendMessageOptions.DontRequireReceiver); ;
            }
            StartCoroutine("Autoexplode");
        }
    }

    IEnumerator Autoexplode()
    {
        Instantiate(rastre, transform.position, Quaternion.identity);
        while (gameObject.GetComponent<Light>().intensity > 0)
        {
            gameObject.GetComponent<Light>().intensity -= 0.5f * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);

    }

}
