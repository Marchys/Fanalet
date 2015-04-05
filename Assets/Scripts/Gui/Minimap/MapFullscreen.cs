using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapFullscreen : MonoBehaviour
{
    public GameObject BackgroundGameObject;
    public GameObject PanelGameObject;
    public GameObject Minimap;

    private Vector3 _originalMinimapPosition;
    private RectTransform minimapRectTransform;

    void Start()
    {
        _originalMinimapPosition = Minimap.transform.localPosition;
        minimapRectTransform = Minimap.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Map"))
        {
            ShowFullscreenMinimap();
        }
        if (Input.GetButtonUp("Map"))
        {
            HideFullscreenMinimap();
        }
    }

    private void ShowFullscreenMinimap()
    {
        PanelGameObject.SetActive(false);
        Minimap.GetComponent<Mask>().enabled = false;
        BackgroundGameObject.transform.localScale = new Vector2(200, 200);
        minimapRectTransform.localPosition = new Vector3(0,0,0);
    }

    private void HideFullscreenMinimap()
    {
        PanelGameObject.SetActive(true);
        Minimap.GetComponent<Mask>().enabled = true;
        BackgroundGameObject.transform.localScale = new Vector2(1, 1);
        minimapRectTransform.localPosition = _originalMinimapPosition;
    }
}
