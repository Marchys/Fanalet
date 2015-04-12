using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectOilGui : MonoBehaviourEx, IHandle<StartTakeOil>
{

    public Text oilText;
    private StartTakeOil _message;

    public void Handle(StartTakeOil message)
    {
        Messenger.Publish(new BlurMessage(true));
        _message = message;
        oilText.text = message.OilDestilated.ToString();
        //Show all the destillation interface
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void Collect()
    {
        BaseCaracterStats modifiedStats = new BaseCaracterStats();
        //substract hearts introduced
        switch (_message.ActivationType)
        {
            case "red":
                modifiedStats.OiLife += _message.OilDestilated;
                break;
            case "blue":
                modifiedStats.OiLife += _message.OilDestilated;
                break;
            case "yellow":
                modifiedStats.OiLife += _message.OilDestilated;
                break;
        }
        _message.StatsProtagonist.UpdateStats(modifiedStats, Messenger);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Messenger.Publish(new EndTakeOil(_message.MessageId, true));
        Messenger.Publish(new BlurMessage(false));
    }

    public void Cancel()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        Messenger.Publish(new EndTakeOil(_message.MessageId, false));
        Messenger.Publish(new BlurMessage(false));
    }
}
