using UnityEngine;
using Random = UnityEngine.Random;

public class Gen_monster : MonoBehaviour
{
    public EnemySlug.SlugType slug;

    public void Crear_Besties()
    {
        string enemyName = null;
        switch (slug)
        {
            case EnemySlug.SlugType.StandardSlug:
                enemyName = "StandardSlug";
                break;
            case EnemySlug.SlugType.RedSlug:
                enemyName = "RedSlug";
                break;
            case EnemySlug.SlugType.BlueSlug:
                enemyName = "BlueSlug";
                break;
            case EnemySlug.SlugType.YellowSlug:
                enemyName = "YellowSlug";
                break;
        }
        var tempo_mons = Instantiate(Bestiari.Generar[enemyName], new Vector3(transform.position.x, transform.position.y, Random.Range(0.000001F, 0.0001F)), Quaternion.identity) as GameObject;
        tempo_mons.transform.parent=gameObject.transform;
    }
}
