using System.Collections.Generic;
using UnityEngine;

// bad linq can cause memory leaks!!

namespace Gen_mapa
{
    public class PoolMaterial
    {
        private readonly GameObject[] _blocks = new GameObject[4];
        private readonly GameObject[] _triggEle = new GameObject[3];
        private readonly List<GameObject> _salesIn;
        private readonly List<GameObject> _salesNor;
        private readonly List<GameObject> _passa;
        private readonly List<GameObject> _lighthouseExterior;
        private readonly List<GameObject> _lighthouseInterior;
        private readonly List<GameObject> _ExitRoom;
        private readonly List<GameObject> _BlackMarketRoomExterior;
        private readonly List<GameObject> _BlackMarketRoomInterior;
        private readonly List<GameObject>[] _enemyRooms = new List<GameObject>[6];

        public PoolMaterial(int lvl)
        {
            var tempTriggArray = Resources.LoadAll("Random_gen/trigg_ele", typeof(GameObject)).Cast<GameObject>().ToArray();
            for (var i = 0; i < 3; i++)
            {
                switch (tempTriggArray[i].name)
                {
                    case "pass_h":
                        _triggEle[0] = tempTriggArray[i];
                        break;
                    case "pass_v":
                        _triggEle[1] = tempTriggArray[i];
                        break;
                    case "sala_nor":
                        _triggEle[2] = tempTriggArray[i];
                        break;
                }
            }

            var tempBlocksArray = Resources.LoadAll("Random_gen/blocks/nivell_" + lvl, typeof(GameObject)).Cast<GameObject>().ToArray();
            for (var i = 0; i < 4; i++)
            {
                switch (tempBlocksArray[i].name)
                {
                    //nord
                    case "0":
                        _blocks[0] = tempBlocksArray[i];
                        break;
                    //sud
                    case "1":
                        _blocks[1] = tempBlocksArray[i];
                        break;
                    //est
                    case "2":
                        _blocks[2] = tempBlocksArray[i];
                        break;
                    //oest
                    case "3":
                        _blocks[3] = tempBlocksArray[i];
                        break;
                }
            }

            _salesIn = LoadResource("Random_gen/sales_in/nivell_", lvl);
            _salesNor = LoadResource("Random_gen/sales_nor/nivell_", lvl);
            _passa = LoadResource("Random_gen/passa/nivell_", lvl);
            _lighthouseExterior = LoadResource("Random_gen/lighthouse/Exterior/nivell_", lvl);
            _lighthouseInterior = LoadResource("Random_gen/lighthouse/Interior/nivell_", lvl);
            _ExitRoom = LoadResource("Random_gen/exit/nivell_", lvl);
            _BlackMarketRoomExterior = LoadResource("Random_gen/black_market/Exterior/nivell_", lvl);
            _BlackMarketRoomInterior = LoadResource("Random_gen/black_market/Interior/nivell_", lvl);
            _enemyRooms[0] = LoadResource("Random_gen/enemy_rooms/standard/nivell_", lvl);
            _enemyRooms[1] = LoadResource("Random_gen/enemy_rooms/red/nivell_", lvl);
            _enemyRooms[2] = LoadResource("Random_gen/enemy_rooms/blue/nivell_", lvl);
            _enemyRooms[3] = LoadResource("Random_gen/enemy_rooms/yellow/nivell_", lvl);
            _enemyRooms[4] = LoadResource("Random_gen/enemy_rooms/all/nivell_", lvl);
            _enemyRooms[5] = LoadResource("Random_gen/enemy_rooms/boss/nivell_", lvl);
        }

        private List<GameObject> LoadResource(string resourceLocation, int lvlRoom)
        {
            return Resources.LoadAll(resourceLocation + lvlRoom, typeof(GameObject)).Cast<GameObject>().ToList();
        }

        public GameObject InitalRooms
        {
            get
            {
                var tempRandom = Random.Range(0, _salesIn.Count);
                var tempConserv = _salesIn[tempRandom];
                return tempConserv;
            }
        }

        public GameObject EmptyRooms
        {
            get
            {
                var tempRandom = Random.Range(0, _salesNor.Count);
                var tempConserv = _salesNor[tempRandom];
                return tempConserv;
            }
        }

        public GameObject[] Blocks
        {
            get
            {
                return _blocks;
            }
        }

        public GameObject Corridors(string os)
        {
            return os == _passa[0].name ? _passa[0] : _passa[1];
        }

        public GameObject LighthouseExterior
        {
            get
            {
                var tempRandom = Random.Range(0, _lighthouseExterior.Count);
                var tempConserv = _lighthouseExterior[tempRandom];
                //sales_in.RemoveAt(temp_random);
                return tempConserv;
            }
        }

        public GameObject LighthouseInterior
        {
            get
            {
                var tempRandom = Random.Range(0, _lighthouseInterior.Count);
                var tempConserv = _lighthouseInterior[tempRandom];
                //sales_in.RemoveAt(temp_random);
                return tempConserv;
            }
        }

        public GameObject ExitRoom
        {
            get
            {
                var tempRandom = Random.Range(0, _ExitRoom.Count);
                var tempConserv = _ExitRoom[tempRandom];
                //sales_in.RemoveAt(temp_random);
                return tempConserv;
            }
        }

        public GameObject BlackMarketExterior
        {
            get
            {
                var tempRandom = Random.Range(0, _BlackMarketRoomExterior.Count);
                var tempConserv = _BlackMarketRoomExterior[tempRandom];
                //sales_in.RemoveAt(temp_random);
                return tempConserv;
            }
        }

        public GameObject BlackMarketInterior
        {
            get
            {
                var tempRandom = Random.Range(0, _BlackMarketRoomInterior.Count);
                var tempConserv = _BlackMarketRoomInterior[tempRandom];
                //sales_in.RemoveAt(temp_random);
                return tempConserv;
            }
        }

        public GameObject EnemyRoom(int enemyRoomType)
        {
            int tempRandom = Random.Range(0, _enemyRooms[enemyRoomType - 20].Count);
            GameObject tempConserv = _enemyRooms[enemyRoomType - 20][tempRandom];
            //_enemyRooms[enemyRoomType - 20].RemoveAt(tempRandom);
            return tempConserv;
        }

        public GameObject[] TriggEle
        {
            get
            {
                return _triggEle;
            }
        }



    }

}