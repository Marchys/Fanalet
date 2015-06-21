using System.Collections;
using UnityEngine;

public class bola : MonoBehaviour
{
    public GameObject rastre;
    public Impacte en_xoc;

    public void Shoot(Vector2 direction,int damage, float impactSpeed, float ballSpeed)
    {
        en_xoc = new Impacte(direction, damage, impactSpeed);
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * ballSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemigo_tri" || col.gameObject.tag == "Escenari_coll" || col.gameObject.tag == "destructible")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Collider2D>().enabled = false;
            if (col.gameObject.tag == "Enemigo_tri")
            {
                col.transform.parent.BroadcastMessage("Impacte_mal", en_xoc, SendMessageOptions.DontRequireReceiver); ;
            }
            if (col.gameObject.tag == "destructible")
            {
                col.gameObject.GetComponent<Destructible>().Mal(en_xoc.Mal);
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
