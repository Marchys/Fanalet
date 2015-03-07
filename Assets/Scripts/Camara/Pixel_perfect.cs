using UnityEngine;

public class Pixel_perfect : MonoBehaviour {

    public void Awake()
    {
        //
        //GetComponent<Camera>().orthographicSize = (Screen.height / 2.0f / 100f); // 100f is the PixelPerUnit that you have set on your sprite. Default is 100.
        //camera.orthographicSize = (Screen.height / 1.5f / 100f); 
    }
}
