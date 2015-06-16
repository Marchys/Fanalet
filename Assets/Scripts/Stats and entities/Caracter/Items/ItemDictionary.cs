using UnityEngine;
using System.Collections.Generic;

public class ItemDictionary
{
	 private static ItemDictionary instance = new ItemDictionary();
        Dictionary<string, GameObject> itemDictionary = new Dictionary<string, GameObject>();

        public static ItemDictionary Generar
        {
            get { return instance; }
        }

        public ItemDictionary()
        {
        
        var tempItemArray = Resources.LoadAll("Items", typeof(GameObject)).Cast<GameObject>().ToArray();

        for (var i = 0; i < tempItemArray.Length; i++)
            {
                itemDictionary.Add(tempItemArray[i].name, tempItemArray[i]);
            }    
           
        }

        public GameObject this[string item]
        {
            get { return itemDictionary[item];}
        }
}
