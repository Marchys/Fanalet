using UnityEngine;

public class UpdateCoords : MonoBehaviour
{

    Enemigo_Minotauro parentEsser;

    // Use this for initialization
    void Awake()
    {
        parentEsser = transform.parent.GetComponent<Enemigo_Minotauro>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trigg" && parentEsser.currentStateName != Enemigo_Minotauro.State.Sleep)
        {
            parentEsser.coor = other.GetComponent<Trigg_ele>().coor;
        }
    }

}
