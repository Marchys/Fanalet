using UnityEngine;

public class Loading_bar : MonoBehaviour
{
    public float barDisplay; //current progress
    Vector2 pos;
    Vector2 size;
    public Texture2D emptyTex;
    public Texture2D fullTex;
    Supergenerador gen;
    void Start()
    {
        gen= GameObject.Find("Nexe").GetComponent<Supergenerador>();
        pos = new Vector2(Screen.width*0.4f, Screen.height*0.7f);
        size = new Vector2(Screen.width*0.2f, Screen.height*0.05f);
    }

    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();
    }
    
    void Update()
    {
        //for this example, the bar display is linked to the current time,
        //however you would set this value based on your desired display
        //eg, the loading progress, the player's health, or whatever.
      
        //      barDisplay = MyControlScript.staticHealth;
    }
}
