//public class Interficies : MonoBehaviour {

//    // Use this for initialization
//    void Start () {
	
//    }
	
//    // Update is called once per frame
//    void Update () {
	
//    }
//}

public interface IVulnerable<T>
{
    void Mal(T punts_de_mal);
}

public interface IMort
{
    void Mort();
}

public interface IDoAtack
{
    void FerMal();
}


