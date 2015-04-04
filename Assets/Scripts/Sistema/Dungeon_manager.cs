using Gen_mapa;
using UnityEngine;

public class Dungeon_manager : MonoBehaviour {

    //supergenerador
    private Supergenerador re_supergenerador;
    private GameObject contenidor_mapa;

    private Map generatedMap;   
   
    public GameObject loading_came;
    public GameObject per_came;
    public GameObject prota;
    public GameObject minotaure;
    private GameObject _mapContainer;

    public GameObject PlayerMinimap;
    public GameObject MinotaurMinimap;
    public GameObject Minimap;
    public GameObject canvasGui;
    public GameObject MinimapLayout;

    private GameObject ins_prota;
    private GameObject ins_camera_prota;
    private Camera camara_prin;   
    

    bool pri_niv = true;

    void Awake()
    {
       
    }

    // Use this for initialization
	void Start () {
        re_supergenerador = GetComponent<Supergenerador>();       
        generatedMap = re_supergenerador.Generar_mapa(15, 15, 1, 64);
        _mapContainer = GameObject.Find("Map");
        Minimap.GetComponentInChildren<MiniMapGenerator>().GenerateMiniMap(generatedMap);
        Crear_prota_mino();
        //StartCoroutine(Joc());           
	}

    public void Iniciar_nivell()
    {
        if (pri_niv)
        {
            pri_niv = false;                      
            loading_came.SetActive(false);
            ins_camera_prota.SetActive(true);
            ins_prota.GetComponent<Protas>().Activat = true;
            minotaure.GetComponent<Enemigo_Minotauro>().a_patrullar();
        }
    }

    public void Crear_prota_mino()
    {
        if (pri_niv)
        {
            ins_prota = Instantiate(prota, new Vector3(re_supergenerador.ProtaPosition.x, re_supergenerador.ProtaPosition.y, prota.transform.position.z), Quaternion.identity) as GameObject;
            ins_camera_prota = Instantiate(per_came, new Vector3(re_supergenerador.ProtaPosition.x, re_supergenerador.ProtaPosition.y, 15), Quaternion.identity) as GameObject;            
            ins_camera_prota.SetActive(false);            
            ins_camera_prota.GetComponent<Smooth_follow>().target = ins_prota.transform;
            ins_camera_prota.GetComponent<ProceduralGridMover2D>().target = ins_prota.transform;
            canvasGui.GetComponent<Canvas>().worldCamera = ins_camera_prota.GetComponent<Camera>();// Set player Minimap targets
           
        }
        minotaure = Instantiate(Bestiari.Generar["Minotauro"], new Vector2(re_supergenerador.MinoPosition.x, re_supergenerador.MinoPosition.y), Quaternion.identity) as GameObject;
        var minotauri = minotaure.GetComponent<Mapa_General_p>();       
        minotauri.map = generatedMap;
        minotauri.Crear_pathfinding_grid();
        //SET minotaur targets
        setMinimapReferences();
    }

    private void setMinimapReferences()
    {
        //Enable Scripts
        Minimap.GetComponent<MiniMap>().enabled = true;
        PlayerMinimap.GetComponent<MapPoint>().enabled = true;
        MinotaurMinimap.GetComponent<MapPoint>().enabled = true;
        MinimapLayout.GetComponent<MapPoint>().enabled = true;

        //SetScriptValues
        Minimap.GetComponent<MiniMap>().Target = ins_prota.transform;
        PlayerMinimap.GetComponent<MapPoint>().Target = ins_prota.transform;
        MinotaurMinimap.GetComponent<MapPoint>().Target = minotaure.transform;
        MinimapLayout.GetComponent<MapPoint>().Target = _mapContainer.transform;
    }
    //IEnumerator Joc()
    //{
    //      while(true)
    //        {



    //          yield return null;
    //        {
    //}
   



     

}
