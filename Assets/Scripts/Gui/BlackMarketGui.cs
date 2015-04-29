using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlackMarketGui : MonoBehaviourEx, IHandle<StartBlackShopMessage>
{
    public GameObject RedHeartGameObject;
    public GameObject BlueHeartGameObject;
    public GameObject YellowHeartGameObject;

    public Text RedHeartText;
    public Text BlueHeartText;
    public Text YellowHeartText;

    public Button BuyButton;

    private BaseCaracterStats priceTools;

    private StartBlackShopMessage _message;


    void Start()
    {
        priceTools = new BaseCaracterStats {BlueHearts = -5, YellowHearts = -5, OldTools = true};
    }

    public void Handle(StartBlackShopMessage message)
    {
        _message = message;
        Messenger.Publish(new BlurMessage(true));
        //Show all the interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        BuyButton.interactable = true;
        if (priceTools.RedHearts == 0)
        {
            RedHeartGameObject.SetActive(false);

        }
        if (priceTools.BlueHearts == 0)
        {
           BlueHeartGameObject.SetActive(false);
        }
        if (priceTools.YellowHearts == 0)
        {
            YellowHeartGameObject.SetActive(false);
        }
        
        if ((priceTools.RedHearts + _message.ProtaStats.RedHearts) < 0 ||
             (priceTools.BlueHearts + _message.ProtaStats.BlueHearts) < 0 ||
             (priceTools.YellowHearts + _message.ProtaStats.YellowHearts) < 0)
        {
            BuyButton.interactable = false;
        }

        RedHeartText.text = priceTools.RedHearts.ToString();
        YellowHeartText.text = priceTools.YellowHearts.ToString();
        BlueHeartText.text = priceTools.BlueHearts.ToString();
    }

    public void Buy ()
    {
        _message.ProtaStats.UpdateStats(priceTools, Messenger);
        //hide all the interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Messenger.Publish(new BlurMessage(false));
        Messenger.Publish(new EndBlackShopMessage(_message.MessageId, true));
    }

      public void Cancel()
    {
        //hide all the  interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Messenger.Publish(new BlurMessage(false));
        Messenger.Publish(new EndBlackShopMessage(_message.MessageId, false));
    }
}
