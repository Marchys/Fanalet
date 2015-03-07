using UnityEngine;
using Random = UnityEngine.Random;

public class Gen_monster : MonoBehaviour 
{
	public string bestia_name = "babosa";

    //public enum Monster
    //{
    //    Babosa = 1,
    //}
    //public Monster MonsterType;
    public void Crear_Besties()
    {
        var tempo_mons = Instantiate(Bestiari.Generar[bestia_name], new Vector3(transform.position.x, transform.position.y, Random.Range(0.000001F, 0.0001F)), Quaternion.identity) as GameObject;
        tempo_mons.transform.parent=gameObject.transform;
    }
}
