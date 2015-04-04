using UnityEngine;
using System.Collections;

public class MapPoint : MonoBehaviour
{

    public Transform Target;

    private MiniMap _map;
    private RectTransform _myRectTransform;

    void Start()
    {
        _map = GetComponentInParent<MiniMap>();
        _myRectTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        Vector2 newPosition = _map.TransformPosition(Target.position);
        _myRectTransform.localPosition = newPosition;
    }
}
