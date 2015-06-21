using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviourEx, IHandle<LoadingTick>
{
    public Text QuantityLoaded;

    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void Handle(LoadingTick message)
    {
        QuantityLoaded.text = message.CurrentLoaded + "/" + message.Total;
        if (message.CurrentLoaded < message.Total) return;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
