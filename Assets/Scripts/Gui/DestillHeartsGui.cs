using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestillHeartsGui : MonoBehaviourEx,IHandle<StartDestilationMessage>
{
    // components gui
    public Image HeartActivated;
    public Button AcceptButton;
    public Text DigitOne;
    public Text DigiTwo;
    
    // sprites to change
    public Sprite RedHeartSprite;
    public Sprite BlueHeartSprite;
    public Sprite YellowHeartSprite;

    //values
    private int maxHeartValue;
    

    public void Handle(StartDestilationMessage message)
    {
        //Reset Values
        AcceptButton.interactable = false;
        //Set heart value
        switch (message.ActivationType)
        {
            case "red":
                maxHeartValue = message.StatsProtagonist.RedHearts;
                HeartActivated.sprite = RedHeartSprite;
                break;
            case "blue":
                maxHeartValue = message.StatsProtagonist.BlueHearts;
                HeartActivated.sprite = BlueHeartSprite;
                break;
            case "yellow":
                maxHeartValue = message.StatsProtagonist.YellowHearts;
                HeartActivated.sprite = YellowHeartSprite;
                break;

        }
        //Show all the destillation interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
