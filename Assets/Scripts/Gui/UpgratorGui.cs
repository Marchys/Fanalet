using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgratorGui : MonoBehaviourEx, IHandle<StartUpgradeGuiMessage>
{
    public Text ExplanatoryText;

    public Text RedHeartText;
    public Text BlueHeartText;
    public Text YellowHeartText;

    public Button UpgradeButton;

    private StartUpgradeGuiMessage _message;

    public void Handle(StartUpgradeGuiMessage message)
    {
        _message = message;
        Messenger.Publish(new BlurMessage(true));
        //Show all the interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        UpgradeButton.interactable = true;
        // show things that will change with the upgrade
        ExplanatoryText.text = "This upgrade contains:";
        if (_message.UpgradeStats.Attack != 0) ExplanatoryText.text += "\n<color=" + Constants.Colors.RedHeart + ">+ More attack </color>";
        if (_message.UpgradeStats.MaxLife != 0) ExplanatoryText.text += "\n<color=" + Constants.Colors.YellowHeart + ">+ More capacity</color>";
        if (_message.UpgradeStats.BaseSpeed != 0) ExplanatoryText.text += "\n<color=" + Constants.Colors.BlueHeart + ">+ More speed </color>";

        RedHeartText.text = "X" + Mathf.Abs(_message.UpgradeStats.RedHearts);
        YellowHeartText.text = "X" + Mathf.Abs(_message.UpgradeStats.YellowHearts);
        BlueHeartText.text = "X" + Mathf.Abs(message.UpgradeStats.BlueHearts);

        if ((_message.UpgradeStats.RedHearts + _message.ProtaStats.RedHearts) < 0 ||
            (_message.UpgradeStats.BlueHearts + _message.ProtaStats.BlueHearts) < 0 ||
            (_message.UpgradeStats.YellowHearts + _message.ProtaStats.YellowHearts) < 0)
        {
            UpgradeButton.interactable = false;
        }

    }

    public void Upgrade()
    {
        _message.ProtaStats.UpdateStats(_message.UpgradeStats, Messenger);
        //hide all the interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
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
        Messenger.Publish(new BlurMessage(false));
        Messenger.Publish(new EndUpgradeGuiMessage(_message.MessageId, false));
    }

}
