using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectLighthouseActivation : MonoBehaviourEx, IHandle<StartPayLighthouseMessage>
{

    //pay for lighthouse activation
    public Button redChoiseButton;
    public Button blueChoiseButton;
    public Button yellowChoiseButton;
    public Button payButton;
    public Text ActivationPriceText;
    private StartPayLighthouseMessage Message;
    BaseCaracterStats modifiedStats;

    public void ClickedChoiseButton(Button clickedButton)
    {
        payButton.interactable=true;
        switch (clickedButton.name)
        {
            case "red":
                redChoiseButton.image.color = new Color32(212,154,154,255);
                blueChoiseButton.image.color = Color.white;
                yellowChoiseButton.image.color = Color.white;

                modifiedStats = new BaseCaracterStats();
                modifiedStats.RedHearts -= 1; 
                break;
            case "blue":
                redChoiseButton.image.color = Color.white;
                blueChoiseButton.image.color = new Color32(161, 202, 212, 255);
                yellowChoiseButton.image.color = Color.white;

                modifiedStats = new BaseCaracterStats();
                modifiedStats.BlueHearts -= 1; 
                break;
            case "yellow":
                redChoiseButton.image.color = Color.white;
                blueChoiseButton.image.color = Color.white;
                yellowChoiseButton.image.color = new Color32(239, 231, 94, 255);

                modifiedStats = new BaseCaracterStats();
                modifiedStats.YellowHearts -= 1; 
                break;
            default:
                break;
        }
    }

    public void payPrice()
    {
        payButton.interactable = false;
        modifiedStats.OiLife -= Message.OilToPay;
        Message.Stats.UpdateStats(modifiedStats,Messenger);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Messenger.Publish(new EndPayLighthouseMessage(modifiedStats,Message.MessageId));
    }

    public void cancelActivation()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Messenger.Publish(new EndPayLighthouseMessage(new BaseCaracterStats(), Message.MessageId));
    }

    public void Handle(StartPayLighthouseMessage message)
    {
        Message = message;
        //Initialize Values
        ActivationPriceText.text = "Choose (" + message.OilToPay + ")";
        //Reset colors
        yellowChoiseButton.image.color = Color.white;
        redChoiseButton.image.color = Color.white;
        blueChoiseButton.image.color = Color.white;
        //Set which buttons should be acivated
        blueChoiseButton.interactable = message.Stats.BlueHearts != 0;
        redChoiseButton.interactable = message.Stats.RedHearts != 0;
        yellowChoiseButton.interactable = message.Stats.YellowHearts != 0;
        //Show all the activation interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        
    }
   



}
