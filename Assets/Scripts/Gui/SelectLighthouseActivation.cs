using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectLighthouseActivation : MonoBehaviourEx, IHandle<StartPayLighthouseMessage>
{

    //Octopus
    public GameObject OctopusGameObject;
    //pay for lighthouse activation
    //Dialogue1
    public GameObject Dialogue1GameObject;
    public Button RedChoiseButton;
    public Button BlueChoiseButton;
    public Button YellowChoiseButton;
    public Button SelectButton;
    //Dialogue2
    public GameObject Dialogue2GameObject;
    public Text PriceBodyText;
    public Text PriceButtonText;
    private StartPayLighthouseMessage Message;
    BaseCaracterStats modifiedStats;

    public void ClickedChoiseButton(Button clickedButton)
    {
        SelectButton.interactable = true;
        switch (clickedButton.name)
        {
            case "red":
                RedChoiseButton.image.color = new Color32(212, 154, 154, 255);
                BlueChoiseButton.image.color = Color.white;
                YellowChoiseButton.image.color = Color.white;

                modifiedStats = new BaseCaracterStats();
                modifiedStats.RedHearts -= 1;
                break;
            case "blue":
                RedChoiseButton.image.color = Color.white;
                BlueChoiseButton.image.color = new Color32(161, 202, 212, 255);
                YellowChoiseButton.image.color = Color.white;

                modifiedStats = new BaseCaracterStats();
                modifiedStats.BlueHearts -= 1;
                break;
            case "yellow":
                RedChoiseButton.image.color = Color.white;
                BlueChoiseButton.image.color = Color.white;
                YellowChoiseButton.image.color = new Color32(239, 231, 94, 255);

                modifiedStats = new BaseCaracterStats();
                modifiedStats.YellowHearts -= 1;
                break;
            default:
                break;
        }
    }


    public void SelectHeart()
    {
        SelectButton.interactable = false;
        modifiedStats.Life -= Message.OilToPay;
        foreach (Transform child in Dialogue1GameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        PriceBodyText.text = Message.OilToPay+" flames.";
        PriceButtonText.text = Message.OilToPay.ToString();
        foreach (Transform child in Dialogue2GameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void PayPrice()
    {
        Message.StatsProtagonist.UpdateStats(modifiedStats, Messenger);
        OctopusGameObject.SetActive(false);
        foreach (Transform child in Dialogue1GameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in Dialogue2GameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        Messenger.Publish(new BlurMessage(false));
        Messenger.Publish(new EndPayLighthouseMessage(modifiedStats, Message.MessageId));
    }

    public void CancelActivation()
    {
        OctopusGameObject.SetActive(false);
        foreach (Transform child in Dialogue1GameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in Dialogue2GameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        Messenger.Publish(new BlurMessage(false));
        Messenger.Publish(new EndPayLighthouseMessage(new BaseCaracterStats(), Message.MessageId));
    }

    public void Handle(StartPayLighthouseMessage message)
    {
        Messenger.Publish(new BlurMessage(true));
        Message = message;
        //set button to default
        SelectButton.interactable = false;
        //Reset colors
        YellowChoiseButton.image.color = Color.white;
        RedChoiseButton.image.color = Color.white;
        BlueChoiseButton.image.color = Color.white;
        //Set which buttons should be acivated
        BlueChoiseButton.interactable = message.StatsProtagonist.BlueHearts != 0;
        RedChoiseButton.interactable = message.StatsProtagonist.RedHearts != 0;
        YellowChoiseButton.interactable = message.StatsProtagonist.YellowHearts != 0;
        //Show all the activation interface
        OctopusGameObject.SetActive(true);
        foreach (Transform child in Dialogue1GameObject.transform)
        {
            child.gameObject.SetActive(true);
        }

    }




}
