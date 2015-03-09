using System.Collections;
using UnityEngine;

public class explosionDamage : MonoBehaviour
{


    public Protas protaG;
    public int damage;
    
    IEnumerator expansionBlastArea()
    {
        var result = new Collider2D[3];
        float hitRadius = 1;
        var hit = false;
        while (hitRadius < 4.3f)
        {
            hitRadius += 3f * Time.deltaTime;
            if (!hit && Physics2D.OverlapCircleNonAlloc(transform.position, hitRadius, result, 16384) > 0)
            {
                protaG.Mal(damage);
                hit = true;
            }
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
