using UnityEngine;

public class ParticleTop : MonoBehaviour {

    public int sortingOrder=0;
    

    void Start()
    {
        //Change Foreground to the layer you want it to display on 
        //You could prob. make a public variable for this
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "porencima";
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = sortingOrder;
    }
  
}
