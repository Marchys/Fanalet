using UnityEngine;

public class minotaurInfluence : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "roomControl")
        {
            other.gameObject.GetComponent<Control_sala>().minotaurEnters();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "roomControl")
        {
            other.gameObject.GetComponent<Control_sala>().minotaurLeaves();
        }
    }
}
