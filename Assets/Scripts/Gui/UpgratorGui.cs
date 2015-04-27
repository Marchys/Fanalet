using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgratorGui : MonoBehaviourEx, IHandle<StartUpgradeGuiMessage>
{
    public Text AttackText;
    public Text SpeedText;
    public Text LifeText;

    public Image RedHeartImage;
    public Image BlueHeartImage;
    public Image YellowHeartImage;

    public Text RedHeartText;
    public Text BlueHeartText;
    public Text YellowHeartText;

    public Button UpgradeButton;

    private StartUpgradeGuiMessage _message;

    public void Handle(StartUpgradeGuiMessage message)
    {
        _message = message;
        Messenger.Publish(new BlurMessage(true));

        // show things that will change with the upgrade
        if (_message.UpgradeStats.Attack != 0) AttackText.enabled = true;
        if (_message.UpgradeStats.MaxOiLife != 0) SpeedText.enabled = true;
        if (_message.UpgradeStats.BaseSpeed != 0) LifeText.enabled = true;

        //show price of upgrade
        if (_message.UpgradeStats.RedHearts != 0)
        {
            RedHeartImage.enabled = true;
            RedHeartText.enabled = true;
            RedHeartText.text = _message.UpgradeStats.RedHearts.ToString();
        }
        if (_message.UpgradeStats.BlueHearts != 0)
        {
            BlueHeartImage.enabled = true;
            BlueHeartText.enabled = true;
            BlueHeartText.text = _message.UpgradeStats.RedHearts.ToString();
        }
        if (_message.UpgradeStats.YellowHearts != 0)
        {
            YellowHeartImage.enabled = true;
            BlueHeartText.enabled = true;
            YellowHeartText.text = _message.UpgradeStats.RedHearts.ToString();
        }

        if ((_message.UpgradeStats.RedHearts + _message.ProtaStats.RedHearts) < 0 ||
            (_message.UpgradeStats.BlueHearts + _message.ProtaStats.BlueHearts) < 0 ||
            (_message.UpgradeStats.YellowHearts + _message.ProtaStats.YellowHearts) < 0)
        {
            UpgradeButton.interactable = false;
        }
            //Show all the interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        Messenger.Publish(new BlurMessage(false));
    }

    public void Upgrade()
    {
        _message.ProtaStats.UpdateStats(_message.UpgradeStats, Messenger);
        //hide all the interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Reset();
        Messenger.Publish(new BlurMessage(false));
        Messenger.Publish(new EndUpgradeGuiMessage(_message.MessageId, true));
    }

    public void Cancel()
    {
        //hide all the  interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Reset();
        Messenger.Publish(new BlurMessage(false));
        Messenger.Publish(new EndUpgradeGuiMessage(_message.MessageId, false));
    }

    private void Reset()
    {
        AttackText.enabled = false;
        SpeedText.enabled = false;
        LifeText.enabled = false;

        RedHeartImage.enabled = false;
        BlueHeartImage.enabled = false;
        YellowHeartImage.enabled = false;

        RedHeartText.enabled = false;
        BlueHeartText.enabled = false;
        YellowHeartText.enabled = false;

        UpgradeButton.interactable = true;
    }

}
