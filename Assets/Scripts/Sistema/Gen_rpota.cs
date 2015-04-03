using UnityEngine;
using UnityEngineInternal;

public class Gen_rpota : MonoBehaviour {

    public GameObject prota;
    public GameObject camera_prota;
	// Use this for initialization
	void Start ()
    {
        var temp_prota = Instantiate(prota, new Vector3(0, 2.5f, transform.position.z), Quaternion.identity) as GameObject;
        var temp_cam = Instantiate(camera_prota, camera_prota.transform.position, Quaternion.identity) as GameObject;
        GameObject.FindWithTag("Canvas").GetComponent<Canvas>().worldCamera = temp_cam.GetComponent<Camera>();
        temp_cam.GetComponent<Smooth_follow>().target = temp_prota.transform ;
        temp_cam.GetComponent<ProceduralGridMover2D>().enabled = false;
        temp_prota.GetComponent<Protas>().Activat = true;

	}	
	
}
