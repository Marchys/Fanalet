using UnityEngine;
using System.Collections;

public class LighthouseInterior : MonoBehaviourEx {

    public GameObject LighthouseRoom;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Prota"))
        {
            Vector2 targetLocation = new Vector2(LighthouseRoom.transform.position.x + 12, LighthouseRoom.transform.position.y-9.25f); 
            Camera.main.transform.position = targetLocation;
            other.transform.position = targetLocation;
            Messenger.Publish(new ProtaExitsLighthouseMessage());
        }
    }
}
