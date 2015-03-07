using System.Collections;
using UnityEngine;

public class generar_game : MonoBehaviour {

    int cont = 0;
    // Use this for initialization
	void Start () {
        StartCoroutine(cosa());
	}

    IEnumerator cosa()
    {
        while (true)
        {
            cont +=20;
            Debug.Log(cont);
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato");
            new GameObject("patato"); 
            yield return null;

        }
        
    }
	
	
}
