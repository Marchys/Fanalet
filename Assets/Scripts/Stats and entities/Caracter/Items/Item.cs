using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Item : MonoBehaviour
{

    public BaseCaracterStats itemStatsModified;
    public GameObject collectedParticles;
  
    //Define Enum
    public enum ItemTypes
    {
        RedHeart,
        BlueHeart,
        YellowHeart,
        OilBottle
    }

    //This is what you need to show in the inspector.
    public ItemTypes Items;

    void Start()
    {
        switch (Items)
        {
            case ItemTypes.RedHeart:
                itemStatsModified = Constants.Items.RedHeart;
                break;
            case ItemTypes.BlueHeart:
                itemStatsModified = Constants.Items.BlueHeart;
                break;
            case ItemTypes.YellowHeart:
                itemStatsModified = Constants.Items.YellowHeart;
                break;
            case ItemTypes.OilBottle:
                itemStatsModified = Constants.Items.OilBottle;
                break;
        }
    }

    public void Collected()
    {
        Instantiate(collectedParticles, new Vector3(transform.position.x, transform.position.y, Random.Range(0.000001F, 0.0001F)), Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }
}
