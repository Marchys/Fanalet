using System.Collections.Generic;
using EpPathFinding.cs;
using Gen_mapa;
using UnityEngine;

public class Mapa_General_p : MonoBehaviour {

    BaseGrid mapa_pa_sala;   
    //public GameObject dispo;
    //public GameObject obst;
    //public GameObject marca_ruta;     
   
    //Vector2 pos_0;
    public Map map;
    List<GridPos> llocs_disponibles = new List<GridPos>();

    //string on_trobo="3,2";
    //string on_vaig="7,9";
	
    // Use this for initialization
	//void Start () {
        //Caminable();        
        //List<GridPos> ruta=Ruta_a(on_trobo,on_vaig);
        //foreach(GridPos cosa in ruta)
        //{
        //    Instantiate(marca_ruta, new Vector2(cosa.x * 2.5f, cosa.y * 2.5f), Quaternion.identity);
        //}     
    //} 
    //void Start () 
    //{      
    //    pos_0 = new Vector2(0,0);        
    //} 

    public void Crear_pathfinding_grid()
    {            
            var matriu_bloqueig = new bool[map.Width][];
            for (var ampleTrav = 0; ampleTrav < map.Width; ampleTrav++)
            {
                matriu_bloqueig[ampleTrav] = new bool[map.Height];
                for (var alturaTrav = 0; alturaTrav < map.Height; alturaTrav++)
                {                    
                    if (map[ampleTrav, alturaTrav] != 0)
                    {
                       // Instantiate(Bestiari.Generar["blue"], Grid_Vector(new GridPos(ampleTrav, alturaTrav)), Quaternion.identity);
                        matriu_bloqueig[ampleTrav][alturaTrav] = true;
                        if (map[ampleTrav, alturaTrav] < 3)llocs_disponibles.Add(new GridPos(ampleTrav, alturaTrav));
                    }
                    //else Instantiate(Bestiari.Generar["red"], Grid_Vector(new GridPos(ampleTrav, alturaTrav)), Quaternion.identity);                 
                }
            }
            mapa_pa_sala = new StaticGrid(map.Width, map.Height, matriu_bloqueig);
                 
    }

    public List<Vector2> Ruta_a(Punt2d origen, Punt2d final)
    {
        var jpParam = new JumpPointParam(mapa_pa_sala, false, false, false, HeuristicMode.MANHATTAN);
        //jpParam.Reset(new GridPos(0,14), new GridPos(0,12));
        jpParam.Reset(new GridPos(origen.X, origen.Y), new GridPos(final.X, final.Y));
        //Debug.Log(Vector_Grid(origen).x+" "+Vector_Grid(origen).y+"origen");
        var ruta_a = JumpPointFinder.FindPath(jpParam);
        var ruta_a_tra = new List<Vector2>();
        foreach (var cosa in ruta_a)
        {
            //Debug.Log(cosa.x+" "+cosa.y);
            ruta_a_tra.Add(Grid_Vector(cosa));
        }
        //Debug.Log(ruta_a_tra.Count + " Hola");
        return ruta_a_tra;
    }  


    public List<Vector2> Ruta_a(Punt2d origen)
    {
        var jpParam = new JumpPointParam(mapa_pa_sala, false, false, false, HeuristicMode.MANHATTAN);
        //jpParam.Reset(new GridPos(0,14), new GridPos(0,12));
        //Debug.Log(origen.X+" "+origen.Y);
        jpParam.Reset(new GridPos (origen.X,origen.Y), Random_pos());
        //Debug.Log(Vector_Grid(origen).x+" "+Vector_Grid(origen).y+"origen");
        var ruta_a = JumpPointFinder.FindPath(jpParam);
        var ruta_a_tra = new List<Vector2>();
        foreach (var cosa in ruta_a)
        {
            //Debug.Log(cosa.x+" "+cosa.y);
            ruta_a_tra.Add(Grid_Vector(cosa));
        }
        //Debug.Log(ruta_a_tra.Count + " Hola");
        return ruta_a_tra;
    }  

    //GridPos Vector_Grid(Vector2 vector)
    //{
        //int ruddy = UnityEngine.Random.Range(1, 99999);
       // Vector2 vector_pros = new Vector2(vector.x - pos_0.x, vector.y-pos_0.y);
        //Debug.Log(vector_pros.x + " ruddy " + vector_pros.y);
        //GridPos pos_nor = new GridPos((int)(vector_pros.x) / 17, (int)(vector_pros.y) / 13);
        //Debug.Log(pos_nor.x + " "+"traduida"+" " + pos_nor.y);
        //Time.timeScale = 0;
        //return pos_nor;
    //}

    Vector2 Grid_Vector(GridPos gridpos)
    {
        var vector_pos = new Vector2(gridpos.x*17, gridpos.y*13);
        //Debug.Log(vector_pos.x+"traduida"+vector_pos.y);
        return vector_pos;
    }

    GridPos Random_pos()
    {        
        var num_rand = Random.Range(0, llocs_disponibles.Count);  
        //Debug.Log(llocs_disponibles[num_rand].x+" "+llocs_disponibles[num_rand].y+"target_dir");
        return llocs_disponibles[num_rand];
    }

    void logruta(List<GridPos> ruta_a)
    {
        var ruddy = Random.Range(1,99999);
        Debug.Log("Ruta" + " " + ruddy);
        foreach (var cosa in ruta_a)
        {
            Debug.Log(cosa.x + " " + cosa.y + " " + ruddy);
        }        
    }

}



