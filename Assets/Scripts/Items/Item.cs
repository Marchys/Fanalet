using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Item : MonoBehaviour
{

    public BaseCaracterStats itemStatsModified;
  
    //Define Enum
    public enum ItemTypes
    {
        RedHeart,
        BlueHeart,
        YellowHeart
    }

    //This is what you need to show in the inspector.
    public ItemTypes Items;

    void Start()
    {
        switch (Items)
        {
            case ItemTypes.RedHeart:
                itemStatsModified = new RedHeartStats();
                break;
            case ItemTypes.BlueHeart:
                itemStatsModified = new BlueHeartStats();
                break;
            case ItemTypes.YellowHeart:
                itemStatsModified = new YellowHeartStats();
                break;
        }
    }
}
