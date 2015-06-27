using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlackMarketGui : MonoBehaviourEx, IHandle<StartBlackShopMessage>
{
    public Text RedHeartText;
    public Text BlueHeartText;
    public Text YellowHeartText;

    public Button BuyButton;

    private readonly BaseCaracterStats _priceTools = Constants.Prices.PriceTools;

    private StartBlackShopMessage _message;

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
      
        if ((_priceTools.RedHearts + _message.ProtaStats.RedHearts) < 0 ||
             (_priceTools.BlueHearts + _message.ProtaStats.BlueHearts) < 0 ||
             (_priceTools.YellowHearts + _message.ProtaStats.YellowHearts) < 0)
        {
            BuyButton.interactable = false;
        }

        RedHeartText.text = "X"+Mathf.Abs(_priceTools.RedHearts);
        YellowHeartText.text = "X"+Mathf.Abs(_priceTools.YellowHearts);
        BlueHeartText.text = "X"+Mathf.Abs(_priceTools.BlueHearts);
    }

    public void Buy ()
    {
        _message.ProtaStats.UpdateStats(_priceTools, Messenger);
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
