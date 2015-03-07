using UnityEngine;

public class Sorting_sprites : MonoBehaviour 
{

    SpriteRenderer Ordre_render ;
    public float corrector;
    void Start()
    {
        Ordre_render = gameObject.GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
       Ordre_render.sortingOrder = (int)(Mathf.Round(((Ordre_render.bounds.min).y*-8) + corrector));     
    }
}
