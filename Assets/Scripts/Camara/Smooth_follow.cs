using UnityEngine;

public class Smooth_follow : MonoBehaviour
{

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            //Vector3 point = camera.WorldToViewportPoint(target.position);
            var delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, transform.position.z)); //(new Vector3(0.5, 0.5, point.z));
            var destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(new Vector2(transform.position.x, transform.position.y), new Vector2(destination.x, destination.y), ref velocity, dampTime);
        }

    }
}