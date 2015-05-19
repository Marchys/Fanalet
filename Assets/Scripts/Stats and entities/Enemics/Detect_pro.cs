using UnityEngine;

public class Detect_pro : MonoBehaviour {

    Transform pare;

    void Start()
    {
       pare = transform.parent;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sub_prota")
        {           
            pare.BroadcastMessage("Atac", SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sub_prota")
        {
            //Debug.Log("exit");
            pare.BroadcastMessage("Pers", SendMessageOptions.DontRequireReceiver);           
        }     
    } 
}
