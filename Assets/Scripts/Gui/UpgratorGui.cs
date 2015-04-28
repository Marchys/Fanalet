using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgratorGui : MonoBehaviourEx, IHandle<StartUpgradeGuiMessage>
{
    public GameObject AttackGameObject;
    public GameObject SpeedGameObject;
    public GameObject LifeGameObject;

    public GameObject RedHeartGameObject;
    public GameObject BlueHeartGameObject;
    public GameObject YellowHeartGameObject;

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
        if (_message.UpgradeStats.Attack == 0) AttackGameObject.SetActive(false);
        if (_message.UpgradeStats.MaxOiLife == 0) LifeGameObject.SetActive(false);
        if (_message.UpgradeStats.BaseSpeed == 0) SpeedGameObject.SetActive(false);

        //show price of upgrade
        if (_message.UpgradeStats.RedHearts == 0)
        {
            RedHeartGameObject.SetActive(false); 
        }
        if (_message.UpgradeStats.BlueHearts == 0)
        {
            BlueHeartGameObject.SetActive(false); 
        }
        if (_message.UpgradeStats.YellowHearts == 0)
        {
            YellowHeartGameObject.SetActive(false); 
        }

         RedHeartText.text = _message.UpgradeStats.RedHearts.ToString();
         YellowHeartText.text = _message.UpgradeStats.YellowHearts.ToString();
         BlueHeartText.text = _message.UpgradeStats.BlueHearts.ToString();

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
