using UnityEngine;

public class Per_2d_part : MonoBehaviour {

    void Start()
    {
        //Change Foreground to the layer you want it to display on 
        //You could prob. make a public variable for this
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Movim";
        //particleSystem.renderer.sortingOrder = 10;
    }
    void LateUpdate()
    {
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = (int) transform.position.y * -8;
    }
}
