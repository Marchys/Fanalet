using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectOilGui : MonoBehaviourEx, IHandle<StartTakeOil>
{

    public Text oilText;
    private StartTakeOil _message;

    public void Handle(StartTakeOil message)
    {
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
        Messenger.Publish(new EndTakeOil(_message.MessageId, true));
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void Cancel()
    {
        Messenger.Publish(new EndTakeOil(_message.MessageId, false));
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
