using UnityEngine;

public class Gen_rpota : MonoBehaviour {

    public GameObject prota;
    public GameObject camera_prota;
	// Use this for initialization
	void Start ()
    {
        var temp_prota = Instantiate(prota, new Vector3(0, 2.5f, prota.transform.position.z), Quaternion.identity) as GameObject;
        var temp_cam = Instantiate(camera_prota, new Vector2(0, 2.5f), Quaternion.identity) as GameObject;        
        temp_cam.GetComponent<Smooth_follow>().target = temp_prota.transform ;
        temp_cam.GetComponent<ProceduralGridMover2D>().enabled = false;
        temp_prota.GetComponent<Protas>().Activat = true;

	}	
	
}
