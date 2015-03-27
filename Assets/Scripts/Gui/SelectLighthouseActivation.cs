using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectLighthouseActivation : MonoBehaviourEx, IHandle<StartPayLighthouseMessage>
{

    //pay for lighthouse activation
    public Button redChoiseButton;
    public Button blueChoiseButon;
    public Button yellowChoiseButton;
    public Button payButton;
    private StartPayLighthouseMessage Message;

    public void ClickedChoiseButton(Button clickedButton)
    {
        payButton.interactable=true;
        switch (clickedButton.name)
        {
            case "red":
                redChoiseButton.image.color = Color.red;
                blueChoiseButon.image.color = new Color(118, 227, 225);
                yellowChoiseButton.image.color = new Color(253, 255, 141);
                break;
            case "blue":
                blueChoiseButon.image.color = Color.blue;
                redChoiseButton.image.color = new Color(255, 156, 156);
                yellowChoiseButton.image.color = new Color(253, 255, 141);
                break;
            case "yellow":
                yellowChoiseButton.image.color = Color.yellow;
                blueChoiseButon.image.color = new Color(118, 227, 225);
                redChoiseButton.image.color = new Color(255, 156, 156);
                break;
            default:
                break;
        }
    }

    public void payPrice()
    {
        Message.Stats.OiLife -= Message.OilToPay;
        Messenger.Publish(new UpdateGuiMessage(Message.Stats));
        Messenger.Publish(new EndPayLighthouseMessage());
    }

    public void Handle(StartPayLighthouseMessage message)
    {
        Message = message;
        if (message.Stats.BlueHearts == 0) redChoiseButton.interactable = false;
        if (message.Stats.RedHearts == 0) blueChoiseButon.interactable = false;
        if (message.Stats.YellowHearts == 0) yellowChoiseButton.interactable = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        
    }
   



}
