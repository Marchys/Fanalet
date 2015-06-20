using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestillHeartsGui : MonoBehaviourEx, IHandle<StartGuiDestilationMessage>
{
    // components gui
    public Image HeartActivated;
    public Button AcceptButton;
    public Text TypeOfHearts;
    public Text FirstDigit;
    public Text SecondDigit;

    // sprites to change
    public Sprite RedHeartSprite;
    public Sprite BlueHeartSprite;
    public Sprite YellowHeartSprite;

    //values
    private int _maxHeartValue;
    private int _currentHearts;

    //message
    private StartGuiDestilationMessage Message;

    public void Start()
    {
        _maxHeartValue = 25;
        _currentHearts = 0;
        FirstDigit.text = (_currentHearts / 10).ToString();
        SecondDigit.text = (_currentHearts % 10).ToString();
        TypeOfHearts.text = "How many <color=" + Constants.Colors.RedHeart + ">red hearts</color> do you want to destill?";
    }


    // digit == 1  FirstDigit digit == 2 SecondDigit
    public void ChangeNumber(int amount)
    {
        _currentHearts = _currentHearts + amount;
        _currentHearts = _currentHearts.LimitToRange(0, _maxHeartValue);
        FirstDigit.text = (_currentHearts / 10).ToString();
        SecondDigit.text = (_currentHearts % 10).ToString();
        AcceptButton.interactable = _currentHearts > 0;
    }

    public void IntroduceHearts()
    {
        AcceptButton.interactable = false;
        BaseCaracterStats modifiedStats = new BaseCaracterStats();
        //substract hearts introduced
        switch (Message.ActivationType)
        {
            case "red":
                modifiedStats.RedHearts -= _currentHearts;
                break;
            case "blue":
                modifiedStats.BlueHearts -= _currentHearts;
                break;
            case "yellow":
                modifiedStats.YellowHearts -= _currentHearts;
                break;
        }
        Message.StatsProtagonist.UpdateStats(modifiedStats, Messenger);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Messenger.Publish(new BlurMessage(false));
        Messenger.Publish(new EndGuiDestilationMessage(Message.MessageId, modifiedStats));
    }

    public void Cancel()
    {
        AcceptButton.interactable = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Messenger.Publish(new BlurMessage(false));
        Messenger.Publish(new EndGuiDestilationMessage(Message.MessageId, new BaseCaracterStats()));
    }

    public void Handle(StartGuiDestilationMessage message)
    {
        Messenger.Publish(new BlurMessage(true));
        Message = message;
        //Reset Values
        AcceptButton.interactable = false;
        //Set heart value
        switch (message.ActivationType)
        {
            case "red":
                _maxHeartValue = message.StatsProtagonist.RedHearts;
                HeartActivated.sprite = RedHeartSprite;
                TypeOfHearts.text = "How many <color=" + Constants.Colors.RedHeart + ">" + message.ActivationType + " hearts</color> do you want to destill?";
                break;
            case "blue":
                _maxHeartValue = message.StatsProtagonist.BlueHearts;
                HeartActivated.sprite = BlueHeartSprite;
                TypeOfHearts.text = "How many <color=" + Constants.Colors.BlueHeart + ">" + message.ActivationType + " hearts</color> do you want to destill?";
                break;
            case "yellow":
                _maxHeartValue = message.StatsProtagonist.YellowHearts;
                HeartActivated.sprite = YellowHeartSprite;
                TypeOfHearts.text = "How many <color=" + Constants.Colors.YellowHeart + ">" + message.ActivationType + " hearts</color> do you want to destill?";
                break;

        }
        //Set digit values
        _currentHearts = 0;
        FirstDigit.text = (_currentHearts / 10).ToString();
        SecondDigit.text = (_currentHearts % 10).ToString();
        //Show all the destillation interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

}
