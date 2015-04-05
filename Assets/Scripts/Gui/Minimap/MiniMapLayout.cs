﻿using UnityEngine;
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
    private Punt2d inRoomCoor;

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

    public void GenerateLayout(Map map)
    {
        var w = map.Width;
        var h = map.Height;
        miniMapLayout = new GameObject[w, h];
        for (var x = 0; x < w; ++x)
        {
            for (var y = 0; y < h; ++y)
            {

                switch (map[x, y])
                {
                    case NcambraIn:
                        var tempRoomIn = Instantiate(RoomGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempRoomIn.transform.localScale = new Vector2(0.02f, 0.02f);
                        tempRoomIn.transform.parent = transform;
                        tempRoomIn.SetActive(true);
                        miniMapLayout[x, y] = tempRoomIn;
                        inRoomCoor = new Punt2d(x,y);
                        break;
                    case Ncambra:
                        var tempRoom = Instantiate(RoomGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempRoom.transform.localScale = new Vector2(0.02f, 0.02f);
                        tempRoom.transform.parent = transform;
                        tempRoom.SetActive(false);
                        miniMapLayout[x, y] = tempRoom;
                        break;
                    case NpassaV:
                        var tempVerticalCorridor = Instantiate(VerticalCorridorGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempVerticalCorridor.transform.localScale = new Vector2(0.02f, 0.02f);
                        tempVerticalCorridor.transform.parent = transform;
                        tempVerticalCorridor.SetActive(false);
                        miniMapLayout[x, y] = tempVerticalCorridor;
                        break;
                    case NpassaH:
                        var tempHorizontalCorridor = Instantiate(HorizontalCorridorGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempHorizontalCorridor.transform.localScale = new Vector3(0.02f, 0.02f);
                        tempHorizontalCorridor.transform.parent = transform;
                        tempHorizontalCorridor.SetActive(false);
                        miniMapLayout[x, y] = tempHorizontalCorridor;
                        break;
                    case Nligthouse:
                        var tempLighthouseRoom = Instantiate(LightHouseRoomGameObject, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempLighthouseRoom.transform.localScale = new Vector2(0.02f, 0.02f);
                        tempLighthouseRoom.transform.parent = transform;
                        tempLighthouseRoom.SetActive(false);
                        miniMapLayout[x, y] = tempLighthouseRoom;
                        break;

                }

            }
        }
        transform.localScale = new Vector2(0.7f, 0.945f);
        Messenger.Publish(new EnterAreaMessage(inRoomCoor));
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