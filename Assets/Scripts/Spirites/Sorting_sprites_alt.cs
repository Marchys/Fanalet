using UnityEngine;

public class Sorting_sprites_alt : MonoBehaviour {
    
    public int corrector;
    void Start()
    {
        //Change Foreground to the layer you want it to display on 
        //You could prob. make a public variable for this
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Movim";
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = (int)((transform.position.y*-4)+corrector);
        //particleSystem.renderer.sortingOrder = 10;
    }
   
}
