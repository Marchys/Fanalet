using UnityEngine;
using System.Collections;
using Gen_mapa;

public class MiniMapGenerator : MonoBehaviour
{
    public GameObject VerticalCorridorGameObject;
    public GameObject HorizontalCorridorGameObject;
    public GameObject RoomGameObject;

    //identifyiers type of map element  
    const int Ncambra = 1;
    const int NcambraIn = 2;
    const int Nligthouse = 5;
    const int NpassaV = 3;
    const int NpassaH = 4;

    private float Traduir_pos(float valor, int or)
    {
        switch (or)
        {
            // 13 vertical
            case 0:
                return valor * 0.40f;
            // 17 horitzontal
            case 1:
                return valor * 0.70f;
        }
        return 0;
    }
    
    public void GenerateMiniMap(Map map)
    {       
        var w = map.Width;
        var h = map.Height;
        for (var x = 0; x < w; ++x)
        {
            for (var y = 0; y < h; ++y)
            {

                switch (map[x, y])
                {
                    case Ncambra:
                    case NcambraIn:
                    case Nligthouse:
                        var tempRoom = Instantiate(RoomGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)),Quaternion.identity) as GameObject;
                        tempRoom.transform.localScale = new Vector2(0.02f, 0.02f);
                        tempRoom.transform.parent = transform;
                        break;
                    case NpassaV:
                        var tempVerticalCorridor = Instantiate(VerticalCorridorGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempVerticalCorridor.transform.localScale = new Vector2(0.02f, 0.02f); 
                        tempVerticalCorridor.transform.parent = transform;
                        break;
                    case NpassaH:
                        var tempHorizontalCorridor = Instantiate(HorizontalCorridorGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempHorizontalCorridor.transform.localScale = new Vector3(0.02f, 0.02f);
                        tempHorizontalCorridor.transform.parent = transform;
                        break;
                }

            }
        }
      transform.localScale = new Vector2(0.7f,0.945f);
    }
}
