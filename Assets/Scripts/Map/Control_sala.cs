using UnityEngine;

public class Control_sala : MonoBehaviour
{

    bool playerInArea = false;
    bool minotaurInArea = false;


    public void minotaurEnters()
    {
        minotaurInArea = true;
        setRoomState(true);
    }

    public void minotaurLeaves()
    {
        minotaurInArea = false;
        setRoomState(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Prota")
        {
            playerInArea = true;
            BroadcastMessage("switchZOrder", true, SendMessageOptions.DontRequireReceiver);
            setRoomState(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Prota")
        {
            playerInArea = false;
            BroadcastMessage("switchZOrder", false, SendMessageOptions.DontRequireReceiver);
            setRoomState(true);
        }

    }

    private void setRoomState(bool goToSleep)
    {
        if (goToSleep)
        {
            BroadcastMessage("Sleep", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            if (!minotaurInArea && playerInArea) BroadcastMessage("WakeUp", SendMessageOptions.DontRequireReceiver);
        }

    }
}
