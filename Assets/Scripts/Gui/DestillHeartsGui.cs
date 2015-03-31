using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestillHeartsGui : MonoBehaviourEx, IHandle<StartGuiDestilationMessage>
{
    // components gui
    public Image HeartActivated;
    public Button AcceptButton;
    public Text HeartQuantityText;

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
        HeartQuantityText.text = _currentHearts / 10 + " " + _currentHearts % 10;
    }


    // digit == 1  FirstDigit digit == 2 SecondDigit
    public void ChangeNumber(int amount)
    {
        _currentHearts = _currentHearts + amount;
        _currentHearts = _currentHearts.LimitToRange(0, _maxHeartValue);
        HeartQuantityText.text = _currentHearts / 10 + " " + _currentHearts % 10;
        AcceptButton.interactable = _currentHearts > 0;
    }

    public void Handle(StartGuiDestilationMessage message)
    {
        Message = message;
        //Reset Values
        AcceptButton.interactable = false;
        //Set heart value
        switch (message.ActivationType)
        {
            case "red":
                _maxHeartValue = message.StatsProtagonist.RedHearts;
                HeartActivated.sprite = RedHeartSprite;
                break;
            case "blue":
                _maxHeartValue = message.StatsProtagonist.BlueHearts;
                HeartActivated.sprite = BlueHeartSprite;
                break;
            case "yellow":
                _maxHeartValue = message.StatsProtagonist.YellowHearts;
                HeartActivated.sprite = YellowHeartSprite;
                break;

        }
        //Set digit values
        _currentHearts = 0;
        HeartQuantityText.text = _currentHearts / 10 + " " + _currentHearts % 10;
        //Show all the destillation interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void IntroduceHearts()
    {
        AcceptButton.interactable = false;
        BaseCaracterStats modifiedStats = new BaseCaracterStats();
        //substract hearts introduced
        switch (Message.ActivationType)
        {
            case "red":
                modifiedStats.RedHearts -=  _currentHearts;
                break;
            case "blue":
                modifiedStats.BlueHearts -= _currentHearts;
                break;
            case "yellow":
                modifiedStats.YellowHearts -= _currentHearts;
                break;
        }
        Message.StatsProtagonist.UpdateStats(modifiedStats, Messenger);
        Messenger.Publish(new EndGuiDestilationMessage(Message.MessageId,modifiedStats));
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

    }

    public void Cancel()
    {
        AcceptButton.interactable = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Messenger.Publish(new EndGuiDestilationMessage(Message.MessageId,new BaseCaracterStats()));
    }


}
