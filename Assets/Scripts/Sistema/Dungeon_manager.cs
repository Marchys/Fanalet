using Gen_mapa;
using UnityEngine;

public class Dungeon_manager : MonoBehaviourEx, IHandle<EndTransitionGuiMessage>
{

    //supergenerador
    private Supergenerador re_supergenerador;
    private GameObject contenidor_mapa;

    private Map generatedMap;

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

    private int _idMessage;


    bool pri_niv = true;

    // Use this for initialization
    void Start()
    {
        _idMessage = GetInstanceID();
        ins_camera_prota = Instantiate(per_came, new Vector3(1, 1, 15), Quaternion.identity) as GameObject;
        if (ins_camera_prota != null) ins_camera_prota.GetComponent<Smooth_follow>().enabled = false;
        re_supergenerador = GetComponent<Supergenerador>();
        generatedMap = re_supergenerador.Generar_mapa(15, 15, 1, 64);
        _mapContainer = GameObject.Find("Map");
        Minimap.GetComponentInChildren<MiniMapLayout>().GenerateLayout(generatedMap);
        Crear_prota_mino();
        //StartCoroutine(Joc());           
    }

    public void Iniciar_nivell()
    {
        Messenger.Publish(new StartTransitionGuiMessage(Constants.GuiTransitions.HoleTransition, Constants.GuiTransitions.Out, _idMessage));
    }

    public void Crear_prota_mino()
    {
        if (pri_niv)
        {
            ins_prota = Instantiate(prota, new Vector3(re_supergenerador.ProtaPosition.x, re_supergenerador.ProtaPosition.y, prota.transform.position.z), Quaternion.identity) as GameObject;
            if (ins_prota != null)
            {
                ins_camera_prota.GetComponent<Smooth_follow>().target = ins_prota.transform;
                ins_camera_prota.GetComponent<Smooth_follow>().enabled = true;
                ins_camera_prota.GetComponent<ProceduralGridMover2D>().target = ins_prota.transform;
            }
            canvasGui.GetComponent<Canvas>().worldCamera = ins_camera_prota.GetComponent<Camera>();// Set player Minimap targets

        }
        minotaure = Instantiate(Bestiari.Generar["Minotauro"], new Vector2(re_supergenerador.MinoPosition.x, re_supergenerador.MinoPosition.y), Quaternion.identity) as GameObject;
        var minotauri = minotaure.GetComponent<Mapa_General_p>();
        minotauri.map = generatedMap;
        minotauri.Crear_pathfinding_grid();
        //SET minotaur targets
        SetMinimapReferences();
    }

    private void SetMinimapReferences()
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

    public void Handle(EndTransitionGuiMessage message)
    {
        if (message.MessageId != _idMessage) return;
        if (pri_niv)
        {
            pri_niv = false;
            Messenger.Publish(new ContinueMessage());
        }

    }
}
