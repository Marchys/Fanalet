using UnityEngine;
using System.Collections;

public class DarkTrader : ActionE, IHandle<DialogueEndMessage>, IHandle<EndBlackShopMessage>
{
    public GameObject DarkTraderGirl;
    public GameObject DarkTraderMachine;
    public TextAsset TextFileDialogue1;
    public TextAsset TextFileDialogue2;
    private string[] _firstDialog;
    private string[] _secondDialog;
    private int _idMessage = 0;
    private bool _bought = false;
    private BaseProtagonistStats protaStats;

    new void Start()
    {
        base.Start();
        if (TextFileDialogue1 != null && TextFileDialogue2 != null)
        {
            _firstDialog = Utils.Lines(TextFileDialogue1.text);
            _secondDialog = Utils.Lines(TextFileDialogue2.text);
        }
        else
        {
            _firstDialog[0] = "---";
            _secondDialog[0] = "---";
        }
    }
    public override void ExecuteAction(BaseProtagonistStats stats)
    {
        protaStats = stats;
        base.ExecuteAction(stats);
        Messenger.Publish(new StopMessage());
        _idMessage = GetInstanceID();
        DarkTraderGirl.GetComponent<Animator>().SetInteger("TomboleraState", 2);
        Messenger.Publish(new DialogueStartMessage(!_bought ? _firstDialog : _secondDialog, _idMessage));
    }

    public override void Handle(MinotaurChaseMessage message)
    {

    }


    public void Handle(DialogueEndMessage message)
    {
        if (message.MessageId != _idMessage) return;
        DarkTraderGirl.GetComponent<Animator>().SetInteger("TomboleraState", 0);
        if (_bought)
        {
            Messenger.Publish(new ContinueMessage());
        }
        else
        {
            Messenger.Publish(new StartBlackShopMessage(_idMessage, protaStats));
        }

    }

    public void Handle(EndBlackShopMessage message)
    {
        if (message.MessageId != _idMessage) return;
        if (message.Sucess)
        {
            _bought = true;
            StartCoroutine(HitButtonSequence());
        }
        Messenger.Publish(new ContinueMessage());
    }

    IEnumerator HitButtonSequence()
    {
        DarkTraderGirl.GetComponent<Animator>().SetInteger("TomboleraState", 1);
        yield return new WaitForSeconds(0.6f);
        DarkTraderMachine.GetComponent<Animator>().SetInteger("TombolaState", 1);
        yield return new WaitForSeconds(0.4f);
        DarkTraderGirl.GetComponent<Animator>().SetInteger("TomboleraState", 0);
        GameObject tempItem = Instantiate(ItemDictionary.Generar["LilBear"], new Vector2(DarkTraderMachine.transform.position.x, DarkTraderMachine.transform.position.y-2.5f), Quaternion.identity) as GameObject;
        if (tempItem != null)
        {
            bool thing = true;
            foreach (Transform t in tempItem.transform)
            {
                if (thing)
                {
                    t.GetComponent<Animator>().SetInteger("NormalItem",1);
                    thing = false;
                }
               
            }
        }
        yield return new WaitForSeconds(0.1f);
        DarkTraderMachine.GetComponent<Animator>().SetInteger("TombolaState", 0);
        Messenger.Publish(new ContinueMessage());
    }
}
