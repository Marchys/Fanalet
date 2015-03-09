using System.Collections.Generic;
using UnityEngine;

// bad linq can cause memory leaks!!

    public class Bestiari 
    {
        private static Bestiari instance = new Bestiari();
        Dictionary<string, GameObject> bestiari = new Dictionary<string, GameObject>();

        public static Bestiari Generar
        {
            get { return instance; }
        }   

        public Bestiari()
        {
        
        var tempBestiariArray = Resources.LoadAll("Bestiari", typeof(GameObject)).Cast<GameObject>().ToArray();

            for (var i = 0; i < tempBestiariArray.Length; i++)
            {
                bestiari.Add(tempBestiariArray[i].name, tempBestiariArray[i]);
            }    
           
        }

        public GameObject this[string bestia]
        {
            get { return bestiari[bestia];}
        }

    }
