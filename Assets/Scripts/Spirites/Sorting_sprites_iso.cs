using UnityEngine;

public class Sorting_sprites_iso : MonoBehaviour {

    SpriteRenderer Ordre_render;
    public int corrector;
    void Start()
    {
        Ordre_render = gameObject.GetComponent<SpriteRenderer>();
        Ordre_render.sortingOrder = (int)(Mathf.Round((Ordre_render.bounds.min).y * -8))+corrector;   
    }   
}
