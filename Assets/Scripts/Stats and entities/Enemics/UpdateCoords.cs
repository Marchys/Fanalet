using UnityEngine;

public class UpdateCoords : MonoBehaviour
{

    MinotaurEnemy parentEsser;

    // Use this for initialization
    void Awake()
    {
        parentEsser = transform.parent.GetComponent<MinotaurEnemy>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trigg" && parentEsser.currentStateName != MinotaurEnemy.State.Sleep)
        {
            parentEsser.coor = other.GetComponent<Trigg_ele>().coor;
        }
    }

}
