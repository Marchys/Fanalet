using UnityEngine;

public class Gen_rpota : MonoBehaviourEx, IHandle<EndTransitionGuiMessage>
{

    public GameObject prota;
    public GameObject camera_prota;
    private GameObject temp_prota;

    private int _idMessage;
	// Use this for initialization
	void Start ()
	{
	    _idMessage = GetInstanceID();
        temp_prota = Instantiate(prota, new Vector3(0, 2.5f, transform.position.z), Quaternion.identity) as GameObject;
        var temp_cam = Instantiate(camera_prota, camera_prota.transform.position, Quaternion.identity) as GameObject;
        GameObject.FindWithTag("Canvas").GetComponent<Canvas>().worldCamera = temp_cam.GetComponent<Camera>();
        temp_cam.transform.position = temp_prota.transform.position; 
        temp_cam.GetComponent<Smooth_follow>().target = temp_prota.transform;
        temp_cam.GetComponent<ProceduralGridMover2D>().enabled = false;
        Messenger.Publish(new StartTransitionGuiMessage(Constants.GuiTransitions.HoleTransition, Constants.GuiTransitions.Out, _idMessage));
	}


    public void Handle(EndTransitionGuiMessage message)
    {
        if (message.MessageId != _idMessage) return;
        temp_prota.GetComponent<Protas>().Activat = true;
        BaseCaracterStats specialIslandLife = new BaseCaracterStats(){Life = 2000000};
        BaseProtagonistStats tempProtaStats = temp_prota.GetComponent<Protas>().Character;
        tempProtaStats.UpdateStats(specialIslandLife,Messenger);
    }
}
