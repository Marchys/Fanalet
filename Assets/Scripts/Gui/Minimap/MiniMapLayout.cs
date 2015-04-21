using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Gen_mapa;
using UnityEngine.UI;

public class MiniMapLayout : MonoBehaviourEx, IHandle<EnterAreaMessage>
{
    public GameObject VerticalCorridorGameObject;
    public GameObject HorizontalCorridorGameObject;
    public GameObject RoomGameObject;
    public GameObject LightHouseRoomGameObject;

    private GameObject[,] miniMapLayout;

    readonly Punt2d[] _closeDirections = { new Punt2d(0, 1), new Punt2d(0, -1), new Punt2d(1, 0), new Punt2d(-1, 0) };

    //identifiers basic map elements  
    const int EmptyRoomId = 1;
    const int InitalRoomId = 2;
    const int VerticalCorridorId = 3;
    const int HorizontalCorridorId = 4;
    //identifiers complex map elements
    const int LighthouseRoomId = 10;
    const int ExitRoomId = 11;
    const int BlackMarketRoomId = 12;
    const int NPCRoomId = 13;
    // identifiers enemy rooms
    const int StandardEnemyRoomId = 20;
    const int RedEnemyRoomId = 21;
    const int BlueEnemyRoomId = 22;
    const int YellowEnemyRoomId = 23;
    const int AllEnemyRoomId = 24;
    const int BossEnemyRoomId = 25;    

    void Start()
    {
     //  GetComponent<RectTransform>().localPosition = new Vector2(-1772, 210);
    }
    public void GenerateLayout(Map map)
    {
        var w = map.Width;
        var h = map.Height;
        var inRoomCoor = new Punt2d(0,0);

        miniMapLayout = new GameObject[w, h];
        for (var x = 0; x < w; ++x)
        {
            for (var y = 0; y < h; ++y)
            {

                switch (map[x, y])
                {
                    case InitalRoomId:
                        var tempRoomIn = Instantiate(RoomGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempRoomIn.transform.localScale = new Vector2(0.02f, 0.02f);
                        tempRoomIn.transform.SetParent(transform, true);
                        tempRoomIn.SetActive(true);
                        miniMapLayout[x, y] = tempRoomIn;
                        inRoomCoor = new Punt2d(x,y);
                        break;
                    case EmptyRoomId:
                        var tempRoom = Instantiate(RoomGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempRoom.transform.localScale = new Vector2(0.02f, 0.02f);
                        tempRoom.transform.SetParent(transform, true);
                        tempRoom.SetActive(false);
                        miniMapLayout[x, y] = tempRoom;
                        break;
                    case VerticalCorridorId:
                        var tempVerticalCorridor = Instantiate(VerticalCorridorGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempVerticalCorridor.transform.localScale = new Vector2(0.02f, 0.02f);
                        tempVerticalCorridor.transform.SetParent(transform, true);
                        tempVerticalCorridor.SetActive(false);
                        miniMapLayout[x, y] = tempVerticalCorridor;
                        break;
                    case HorizontalCorridorId:
                        var tempHorizontalCorridor = Instantiate(HorizontalCorridorGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempHorizontalCorridor.transform.localScale = new Vector3(0.02f, 0.02f);
                        tempHorizontalCorridor.transform.SetParent(transform, true);
                        tempHorizontalCorridor.SetActive(false);
                        miniMapLayout[x, y] = tempHorizontalCorridor;
                        break;
                    case LighthouseRoomId:
                        var tempLighthouseRoom = Instantiate(LightHouseRoomGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempLighthouseRoom.transform.localScale = new Vector2(0.02f, 0.02f);
                        tempLighthouseRoom.transform.SetParent(transform, true);
                        tempLighthouseRoom.SetActive(false);
                        miniMapLayout[x, y] = tempLighthouseRoom;
                        break;

                }

            }
        }
        transform.localScale = new Vector2(0.7f, 0.945f);
        Messenger.Publish(new EnterAreaMessage(inRoomCoor));
    }


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

    public void Handle(EnterAreaMessage message)
    {
        miniMapLayout[message.Coor.X, message.Coor.Y].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        for (var dir = 0; dir < 4; dir++)
        {
            int xCoor = message.Coor.X + _closeDirections[dir].X;
            int yCoor = message.Coor.Y + _closeDirections[dir].Y;
            if (miniMapLayout.GetLength(0) > xCoor && xCoor >= 0 && miniMapLayout.GetLength(1) > yCoor && yCoor >= 0)
            {
                if (miniMapLayout[xCoor, yCoor] != null)
                {
                    if (!miniMapLayout[xCoor, yCoor].activeInHierarchy)
                    {
                       miniMapLayout[xCoor, yCoor].SetActive(true);
                       miniMapLayout[xCoor, yCoor].GetComponent<Image>().color = new Color32(255,255,255,50);
                    }
                }
            }
        }
    }
}
