using UnityEngine;

public class UpdateCoords : MonoBehaviour
{

    EnemyMinotaur parentEsser;

    // Use this for initialization
    void Awake()
    {
        parentEsser = transform.parent.GetComponent<EnemyMinotaur>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trigg" && parentEsser.currentStateName != EnemyMinotaur.State.Sleep)
        {
            parentEsser.coor = other.GetComponent<Trigg_ele>().coor;
        }
    }

}
