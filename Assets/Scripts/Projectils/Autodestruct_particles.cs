using System.Collections;
using UnityEngine;

public class Autodestruct_particles : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(destruir());
    }

    IEnumerator destruir()
    {
        while (gameObject.GetComponent<Light>().intensity > 0)
        {
            gameObject.GetComponent<Light>().intensity -= 0.5f * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }


}
