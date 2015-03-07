using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gen_mapa;
using UnityEngine;

public class Supergenerador : MonoBehaviour
{
    #region variables
    private Map _map;
    private PoolMaterial _poolMaterial;
    //posicio actual laberint 
    // Nombre màxim de sales a generar
    private int _maxSales;
    // sala actual        
    //elements map   
    private GameObject _contenidorInst;
    //the adjescent directions and the far directions north, south, east and  west
    readonly Punt2d[] _closeDirections = { new Punt2d(0, 1), new Punt2d(0, -1), new Punt2d(1, 0), new Punt2d(-1, 0) };
    //identifyiers type of map element  
    const int Ncambra = 1;
    const int NcambraIn = 2;
    const int NpassaV = 3;
    const int NpassaH = 4;
    const int Nligthouse = 5;
    const int NPassa = 1111;
    //Chance
    // chance to connect rooms with ha corridor
    private const int ChanceAcceptBuild = 5;
    //chance connect rooms when stuck
    private const int ChanceConnectStuck = 50;
    //Shows progress for the instantiation of the map
    public float LoadingBarProgress = 0;
    //References that will be used by the instantiator
    public Vector2 ProtaPosition;
    public Vector2 MinoPosition;
    private int _quadrantProta;
    public Vector2 PosZero;

    #endregion

    #region Funcions Inici

    public Map Generar_mapa(int x, int y, int niv, int maxSa)
    {
        Reset_valors();
        _map = new Map(x, y);
        ProtaPosition = new Vector2(Traduir_pos(_map.Pointer.X, 1), Traduir_pos(_map.Pointer.Y, 0));
        _quadrantProta = Quin_Quadrant(_map.Pointer);
        _poolMaterial = new PoolMaterial(niv);
        _maxSales = maxSa;
        _contenidorInst = new GameObject { name = "Map" };
        Crear_dungeon();
        //Debug.Log("he pogut: "+cont_puc);
        //Debug.Log("no he pogut: " + no_cont_puc);
        return _map;
    }

    void Reset_valors()
    {
        if (_contenidorInst != null) Destroy(_contenidorInst);
    }

    #endregion

    #region Crear laberint
    void Crear_dungeon()
    {

        var primeraSala = true;
        var countOnPath = 0;
        while (_maxSales > _map.RoomCount)
        {

            if (_map.RoomCount == 0 && primeraSala)
            {
                //primera sala
                primeraSala = false;
                PopulateWith(NcambraIn, new Punt2d(0, 0));
            }
            else
            {
                //calculs map
                List<Punt2d> emptyDirections;
                var tempDis = false;
                var repetir = countOnPath > 3;
                do
                {
                    emptyDirections = TestDirections();
                    if (repetir)
                    {
                        countOnPath = 0;
                        if (Utils.ChanceTrue(ChanceConnectStuck))
                        {
                            emptyDirections = new List<Punt2d>(IsNotEmpty(emptyDirections));

                            if (emptyDirections.Count != 0)
                            {
                                PopulateWith(NPassa, emptyDirections[Random.Range(0, emptyDirections.Count)]);
                            }
                        }
                        if (FindNextRoom())
                        {
                            emptyDirections = new List<Punt2d>(TestDirections());
                            tempDis = true;
                            repetir = false;
                        }
                        else
                        {
                            Error_fatal("Tot ple!! o fallo gordo");
                            break;
                        }
                    }
                    else
                    {
                        emptyDirections = new List<Punt2d>(testDirections(emptyDirections));
                        //for (int dir = 0; dir < emptyDirections.Count; dir++)
                        //{
                        //    Debug.Log(emptyDirections[dir].X + " " + emptyDirections[dir].Y);
                        //}
                        if (emptyDirections.Count == 0) repetir = true;
                        else
                        {
                            countOnPath++;
                            tempDis = true;
                        }
                    }

                } while (!tempDis);

                var tempDir = emptyDirections[Random.Range(0, emptyDirections.Count)];
                PopulateWith(NPassa, tempDir);
                if (testDirections(tempDir))
                {
                    PopulateWith(Ncambra, tempDir);
                }
                else
                {
                    if (FindNextRoom())
                    {
                        emptyDirections = TestDirections();
                        tempDis = true;
                    }
                    else
                    {
                        Error_fatal("Tot ple!! o fallo gordo");
                        break;
                    }

                }
            }

        }
        // Segona fase map    
        //Localització del minotaure 
        var quadrantMinId = Quadrant_Oposat(_quadrantProta);
        var quadrantCant = Quadrant_cant_ex(quadrantMinId);
        if (_map[quadrantCant.X, quadrantCant.Y] == Ncambra) MinoPosition = new Vector2(Traduir_pos(quadrantCant.X, 1), Traduir_pos(quadrantCant.Y, 0));
        else
        {
            var tempPunt = Super_mino(quadrantCant);
            MinoPosition = new Vector2(Traduir_pos(tempPunt.X, 1), Traduir_pos(tempPunt.Y, 0));
        }
        //Tercera fase map
        //Localització dels fars
        for (var i = 0; i <= 3; i++)
        {
            var possibleLighthouseLocations = LocationsInQuadrant(AreaQuadrant(i, 3));
            var lighthouseLocation = possibleLighthouseLocations[Random.Range(0, possibleLighthouseLocations.Count)];
            PopulateWith(Nligthouse, lighthouseLocation);
        }
        StartCoroutine(Colocar_al_mon());

    }

    #endregion

    #region quadrant
    Punt2d Quadrant_cant_ex(int quad)
    {
        switch (quad)
        {
            case 0:
                return new Punt2d(0, 0);
            case 1:
                return new Punt2d(_map.Width - 1, 0);
            case 2:
                return new Punt2d(0, _map.Height - 1);
            case 3:
                return new Punt2d(_map.Width - 1, _map.Height - 1);
            default:
                Debug.Log("Error esquina");
                return new Punt2d(6666, 6666);
        }
    }

    int Quadrant_Oposat(int quad)
    {
        switch (quad)
        {
            case 0:
                return 3;
            case 1:
                return 2;
            case 2:
                return 1;
            case 3:
                return 0;
            default:
                Debug.Log("Error quadrant oposat");
                return 6000;
        }
    }

    int Quin_Quadrant(Punt2d puntQua)
    {
        if (puntQua.X > (_map.Width / 2))
        {
            return puntQua.Y > (_map.Height / 2) ? 3 : 1;
        }
        return puntQua.Y < (_map.Height / 2) ? 0 : 2;
    }

    Quadrant AreaQuadrant(int quad, int quadSize)
    {

        //quadSize must be 2 or 3
        Quadrant tempQuadrant;
        switch (quad)
        {
            case 0:
                tempQuadrant = new Quadrant(0, 0, (_map.Width / quadSize) - 1, (_map.Height / quadSize) - 1);
                break;
            case 1:
                tempQuadrant = new Quadrant(_map.Width - _map.Width / quadSize, 0, _map.Width - 1, (_map.Height / quadSize) - 1);
                break;
            case 2:
                tempQuadrant = new Quadrant(0, _map.Height - _map.Height / quadSize, (_map.Width / quadSize) - 1, _map.Height - 1);
                break;
            case 3:
                tempQuadrant = new Quadrant(_map.Width - _map.Width / quadSize, _map.Height - _map.Height / quadSize, _map.Width - 1, _map.Height - 1);
                break;
            default:
                Debug.Log("Funció map error");
                tempQuadrant = new Quadrant(0, 0, 5, 5);
                break;
        }
        return tempQuadrant;
    }

    List<Punt2d> LocationsInQuadrant(Quadrant quadrant)
    {
        var locations = new List<Punt2d>();
        for (var x = quadrant.TopLeftCorner.X; x < quadrant.BottomRightCorner.X; x++)
        {
            for (var y = quadrant.TopLeftCorner.Y; y < quadrant.BottomRightCorner.Y; y++)
            {
                if (_map[x, y].Equals(1))
                {
                    locations.Add(new Punt2d(x, y));
                }
            }

        }
        return locations;
    }
    #endregion

    #region Funcions ajuda laberint
    //test if all 4 directions from the pointer are empty
    private List<Punt2d> TestDirections()
    {
        var direccionsDis = new List<Punt2d>();

        for (var dir = 0; dir < 4; dir++)
        {
            if (_map[_map.Pointer.X + _closeDirections[dir].X, _map.Pointer.Y + _closeDirections[dir].Y] == 0) direccionsDis.Add(_closeDirections[dir]);
        }
        return direccionsDis;

    }

    //test if all 4 directions from the point provided are empty
    private List<Punt2d> TestDirectionsFrom(Punt2d point)
    {
        var direccionsDis = new List<Punt2d>();

        for (var dir = 0; dir < 4; dir++)
        {
            if (_map[point.X + _closeDirections[dir].X, point.Y + _closeDirections[dir].Y] == 0) direccionsDis.Add(_closeDirections[dir]);
        }
        return direccionsDis;
    }

    //test if and specific direction from the pointer is empty
    private bool testDirections(Punt2d dir)
    {
        return _map[_map.Pointer.X + dir.X, _map.Pointer.Y + dir.Y] == 0;
    }

    //checks if all 4 directions 2 positions away from the pointer are empty, if they aren't, they are removed 
    private List<Punt2d> testDirections(List<Punt2d> directionsCheck)
    {
        var tempFarDirections = Enumerable.ToList(directionsCheck.Select(t => new Punt2d(t.X * 2, t.Y * 2)));

        var tempList = new List<Punt2d>(directionsCheck);

        for (var dir = 0; dir < directionsCheck.Count; dir++)
        {
            var positionValue = _map[_map.Pointer.X + tempFarDirections[dir].X, _map.Pointer.Y + tempFarDirections[dir].Y];
            if (positionValue == 666) tempList.Remove(directionsCheck[dir]);
            else if (positionValue != 0)
            {
                var point = new Punt2d(_map.Pointer.X + tempFarDirections[dir].X, _map.Pointer.Y + tempFarDirections[dir].Y);
                if (TestDirectionsFrom(point).Count < 3)
                {
                    if (!Utils.ChanceTrue(ChanceAcceptBuild))
                    {
                        tempList.Remove(directionsCheck[dir]);
                    }
                }
            }
        }
        return tempList;
    }

    //checks if all 4 directions 2 positions away from the pointer are empty,and removes the ones that are empty
    private IEnumerable<Punt2d> IsNotEmpty(IList<Punt2d> directionsCheck)
    {
        var tempFarDirections = Enumerable.ToList(directionsCheck.Select(t => new Punt2d(t.X * 2, t.Y * 2)));

        var tempList = new List<Punt2d>(directionsCheck);

        for (var dir = 0; dir < directionsCheck.Count; dir++)
        {
            var positionValue = _map[_map.Pointer.X + tempFarDirections[dir].X, _map.Pointer.Y + tempFarDirections[dir].Y];
            if (positionValue == 666) tempList.Remove(directionsCheck[dir]);
            else if (positionValue == 0) tempList.Remove(directionsCheck[dir]);
        }
        return tempList;
    }

    private bool FindNextRoom()
    {
        var llistaCambres = new List<Punt2d>();
        var mapWidth = _map.Width;
        var mapHeight = _map.Height;
        for (var x = 0; x < mapWidth; ++x)
        {
            for (var y = 0; y < mapHeight; ++y)
            {
                if (_map[x, y].Equals(1))
                {
                    llistaCambres.Add(new Punt2d(x, y));
                }

            }
        }
        var anyCamb = true;
        do
        {
            var tempRandom = Random.Range(0, llistaCambres.Count);
            _map.Pointer = llistaCambres[tempRandom];
            llistaCambres.RemoveAt(tempRandom);
            var tempCub = new List<Punt2d>(TestDirections());
            if (tempCub.Count != 0)
            {
                if (testDirections(tempCub).Count != 0)
                    return true;
            }
            if (llistaCambres.Count == 0) anyCamb = false;
        } while (anyCamb);
        return false;
    }

    private void PopulateWith(int elementType, Punt2d dir)
    {
        if (elementType < 4 || elementType == 1111)
        {
            if (elementType == 1 || elementType == 2) _map.RoomCount++;
            _map.Pointer.X += dir.X;
            _map.Pointer.Y += dir.Y;
            if (elementType == 1111)
            {
                elementType = dir.Y != 0 ? 3 : 4;
            }
        }
        else
        {
            _map.Pointer.X = dir.X;
            _map.Pointer.Y = dir.Y;
        }

        _map[_map.Pointer.X, _map.Pointer.Y] = elementType;
    }
    Punt2d Super_mino(Punt2d punt)
    {
        var direccionsDis = new List<int>();

        for (var i = 0; i < 4; i++)
        {
            switch (i)
            {
                //nord
                case 0:
                    if (_map[punt.X, punt.Y + 2] == Ncambra) direccionsDis.Add(0);
                    break;
                //sud
                case 1:
                    if (_map[punt.X, punt.Y - 2] == Ncambra) direccionsDis.Add(1);
                    break;
                //est
                case 2:
                    if (_map[punt.X + 2, punt.Y] == Ncambra) direccionsDis.Add(2);
                    break;
                //oest
                case 3:
                    if (_map[punt.X - 2, punt.Y] == Ncambra) direccionsDis.Add(3);
                    break;
            }
        }

//        Debug.Log(direccionsDis.Count);
        var dir = direccionsDis[Random.Range(0, direccionsDis.Count)];
        switch (dir)
        {
            //nord
            case 0:
                return new Punt2d(punt.X, punt.Y + 2);
            //sud
            case 1:
                return new Punt2d(punt.X, punt.Y - 2);
            //est
            case 2:
                return new Punt2d(punt.X + 2, punt.Y);
            //oest
            case 3:
                return new Punt2d(punt.X - 2, punt.Y);
        }
        return new Punt2d(55555, 55555);
    }

    List<int> Test_dir_blo(Punt2d punt)
    {
        int tempCont;
        var direccionsDis = new List<int>();

        for (var i = 0; i < 4; i++)
        {
            switch (i)
            {
                //nord
                case 0:
                    tempCont = _map[punt.X, punt.Y + 1];
                    if (tempCont == 0 || tempCont == 666) direccionsDis.Add(0);
                    break;
                //sud
                case 1:
                    tempCont = _map[punt.X, punt.Y - 1];
                    if (tempCont == 0 || tempCont == 666) direccionsDis.Add(1);
                    break;
                //est
                case 2:
                    tempCont = _map[punt.X + 1, punt.Y];
                    if (tempCont == 0 || tempCont == 666) direccionsDis.Add(2);
                    break;
                //oest
                case 3:
                    tempCont = _map[punt.X - 1, punt.Y];
                    if (tempCont == 0 || tempCont == 666) direccionsDis.Add(3);
                    break;
            }
        }
        return direccionsDis;
    }

    float Traduir_pos(float valor, int or)
    {
        switch (or)
        {
            // 13 vertical
            case 0:
                return valor * 13;
            // 17 horitzontal
            case 1:
                return valor * 17;
        }
        return 0;
    }

    float TransformPosition(float valor, int or)
    {
        switch (or)
        {
            // 13 vertical
            case 0:
                return (valor * 13)+8;
            // 17 horitzontal
            case 1:
                return (valor * 17)-12;
        }
        return 0;
    }
   
    void Error_fatal(string cosa)
    {
        Debug.Log("Error_Catastrofic");
        Debug.Log(cosa);
    }

    void Error_no_fatal(string cosa)
    {
        Debug.Log("Error_no_Catastrofic");
        Debug.Log(cosa);
    }

    IEnumerator Colocar_al_mon()
    {
        var w = _map.Width;
        var h = _map.Height;
        var total = h * w;
        var un = (float)1 / total;
        for (var x = 0; x < w; ++x)
        {
            for (var y = 0; y < h; ++y)
            {
                switch (_map[x, y])
                {
                    case Ncambra:
                        var tempCambra = Instantiate(_poolMaterial.SalesNor, new Vector2(TransformPosition(x, 1), TransformPosition(y, 0)), Quaternion.identity) as GameObject;
                        var tempContenidor = Instantiate(_poolMaterial.ConstructMapa, new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;

                        Colocar_blocks(Test_dir_blo(new Punt2d(x, y)), tempCambra);
                        tempCambra.transform.parent = tempContenidor.transform;
                        tempContenidor.transform.parent = _contenidorInst.transform;

                        var tempTrigg = Instantiate(_poolMaterial.TriggEle[2], new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempTrigg.GetComponent<Trigg_ele>().coor = new Punt2d(x, y);
                        tempTrigg.transform.parent = tempCambra.transform;

                        tempCambra.BroadcastMessage("Crear_Besties", SendMessageOptions.DontRequireReceiver);
                        break;
                    case NpassaV:
                        var passV = Instantiate(_poolMaterial.Pass("pass_V"), new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        passV.transform.parent = _contenidorInst.transform;

                        var tempTriggV = Instantiate(_poolMaterial.TriggEle[1], new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempTriggV.GetComponent<Trigg_ele>().coor = new Punt2d(x, y);
                        tempTriggV.transform.parent = passV.transform;
                        break;
                    case NpassaH:
                        var passH = Instantiate(_poolMaterial.Pass("pass_H"), new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0) - 2), Quaternion.identity) as GameObject;
                        passH.transform.parent = _contenidorInst.transform;

                        var tempTriggH = Instantiate(_poolMaterial.TriggEle[0], new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempTriggH.GetComponent<Trigg_ele>().coor = new Punt2d(x, y);
                        tempTriggH.transform.parent = passH.transform;
                        break;
                    case NcambraIn:
                        var tempCambraIn = Instantiate(_poolMaterial.SalesIn, new Vector2(TransformPosition(x, 1), TransformPosition(y, 0)), Quaternion.identity) as GameObject;
                        Colocar_blocks(Test_dir_blo(new Punt2d(x, y)), tempCambraIn);
                        tempCambraIn.transform.parent = _contenidorInst.transform;

                        var tempTriggIn = Instantiate(_poolMaterial.TriggEle[2], new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempTriggIn.GetComponent<Trigg_ele>().coor = new Punt2d(x, y);
                        tempTriggIn.transform.parent = tempCambraIn.transform;
                        break;
                    case Nligthouse:
                        var templighthouse = Instantiate(_poolMaterial.Lighthouse, new Vector2(TransformPosition(x, 1), TransformPosition(y, 0)), Quaternion.identity) as GameObject;
                        Colocar_blocks(Test_dir_blo(new Punt2d(x, y)), templighthouse);
                        templighthouse.transform.parent = _contenidorInst.transform;

                        var tempTriggLh = Instantiate(_poolMaterial.TriggEle[2], new Vector2(Traduir_pos(x, 1), Traduir_pos(y, 0)), Quaternion.identity) as GameObject;
                        tempTriggLh.GetComponent<Trigg_ele>().coor = new Punt2d(x, y);
                        tempTriggLh.transform.parent = templighthouse.transform;
                        break;
                    default:
                        //Debug.Log("this shoulden't be here see the supergenerator");
                        break;
                }
                LoadingBarProgress += un;
                yield return null;

            }

        }
        GetComponent<Dungeon_manager>().Iniciar_nivell();
    }

    void Colocar_blocks(List<int> bloqueigLlocs, GameObject sala)
    {

        foreach (var bloquejat in bloqueigLlocs)
        {
            switch (bloquejat)
            {
                case 0:
                    var tempBlock0 = Instantiate(_poolMaterial.Blocks[0], new Vector2(sala.transform.position.x + 12, sala.transform.position.y-8), Quaternion.identity) as GameObject;
                    tempBlock0.transform.parent = sala.transform;
                    break;
                case 1:
                    var tempBlock1 = Instantiate(_poolMaterial.Blocks[1], new Vector2(sala.transform.position.x + 12, sala.transform.position.y-8), Quaternion.identity) as GameObject;
                    tempBlock1.transform.parent = sala.transform;
                    break;
                case 2:
                    var tempBlock2 = Instantiate(_poolMaterial.Blocks[2], new Vector2(sala.transform.position.x + 12, sala.transform.position.y-8), Quaternion.identity) as GameObject;
                    tempBlock2.transform.parent = sala.transform;
                    break;
                case 3:
                    var tempBlock3 = Instantiate(_poolMaterial.Blocks[3], new Vector2(sala.transform.position.x + 12, sala.transform.position.y-8), Quaternion.identity) as GameObject;
                    tempBlock3.transform.parent = sala.transform;
                    break;
            }

        }

    }

    #endregion

}

