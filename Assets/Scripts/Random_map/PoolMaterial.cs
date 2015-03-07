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
        private readonly List<GameObject> _lighthouse;
        private readonly GameObject _constructMapa;

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
            
            var tempSalesInArray = Resources.LoadAll("Random_gen/sales_in/nivell_" + lvl, typeof(GameObject)).Cast<GameObject>().ToArray();
            _salesIn = tempSalesInArray.ToList();
            var tempSalesNorArray = Resources.LoadAll("Random_gen/sales_nor/nivell_" + lvl, typeof(GameObject)).Cast<GameObject>().ToArray();
            _salesNor = tempSalesNorArray.ToList();
            var tempPassaArray = Resources.LoadAll("Random_gen/passa/nivell_" + lvl, typeof(GameObject)).Cast<GameObject>().ToArray();
            _passa = tempPassaArray.ToList();
            var tempNeedArray = Resources.LoadAll("Random_gen/necesita_sala_sempre/Construct_mapa", typeof(GameObject)).Cast<GameObject>().ToArray();
            _constructMapa = tempNeedArray[0];
            var tempLighthouse = Resources.LoadAll("Random_gen/lighthouse/nivell_" + lvl, typeof(GameObject)).Cast<GameObject>().ToArray();
            _lighthouse = tempLighthouse.ToList();


        }

        public GameObject SalesIn
        {
          get{
              var tempRandom = Random.Range(0,_salesIn.Count);
              var tempConserv = _salesIn[tempRandom];
              return tempConserv;         
             }
        }

        public GameObject SalesNor
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

        public GameObject Pass(string os)
        {
            return os == _passa[0].name ? _passa[0] : _passa[1];
        }

        public GameObject Lighthouse
        {
            get
            {
                var tempRandom = Random.Range(0, _lighthouse.Count);
                var tempConserv = _lighthouse[tempRandom];
                //sales_in.RemoveAt(temp_random);
                return tempConserv;
            }
        }
        
        public GameObject ConstructMapa
        {
            get { return _constructMapa;}
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