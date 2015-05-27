using UnityEngine;
using Random = UnityEngine.Random;

public class Gen_monster : MonoBehaviour
{
    public SlugEnemy.SlugType slug;

    public void Crear_Besties()
    {
        string enemyName = null;
        switch (slug)
        {
            case SlugEnemy.SlugType.StandardSlug:
                enemyName = "StandardSlug";
                break;
            case SlugEnemy.SlugType.RedSlug:
                enemyName = "RedSlug";
                break;
            case SlugEnemy.SlugType.BlueSlug:
                enemyName = "BlueSlug";
                break;
            case SlugEnemy.SlugType.YellowSlug:
                enemyName = "YellowSlug";
                break;
        }
        var tempo_mons = Instantiate(Bestiari.Generar[enemyName], new Vector3(transform.position.x, transform.position.y, Random.Range(0.000001F, 0.0001F)), Quaternion.identity) as GameObject;
        tempo_mons.transform.parent=gameObject.transform;
    }
}
