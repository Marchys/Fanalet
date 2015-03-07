using UnityEngine;
using System.Collections;

public class MovementTest : MonoBehaviour
{

    private Rigidbody2D ownigid;

    void Start()
    {
        ownigid = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        Debug.Log(ownigid.velocity); 

        // Move senteces
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * 2, 0.8f),
                                             Mathf.Lerp(0, Input.GetAxis("Vertical") *2, 0.8f));
    }
}
