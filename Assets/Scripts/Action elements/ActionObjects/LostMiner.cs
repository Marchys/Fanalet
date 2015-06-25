using UnityEngine;
using System.Collections;
using System.Text;

public class LostMiner : ActionE, IHandle<LighthouseActivatedMessage>, IHandle<DialogueEndMessage>
{
    public TextAsset TextFileDialogue1;
    public TextAsset TextFileDialogue2;
    public TextAsset TextFileDialogue3;

    public GameObject OldManGameObject;
    public GameObject ShadowOldManGameObject;
    public GameObject ExitExplosion;
    public GameObject HideExplosion;

    private string[] _firstDialog;
    private string[] _secondDialog;
    private string[] _thirdDialog;

    private BaseCaracterStats protaStats;

    private bool _hasTools = false;
    private bool _allLighthousesActivated = false;

    private int _lighthousesActivated = 0;

    private int _idMessage = 0;

    new void Start()
    {
        base.Start();
        if (TextFileDialogue1 != null && TextFileDialogue2 != null && TextFileDialogue3 != null)
        {
            _firstDialog = Utils.Lines(TextFileDialogue1.text);
            _secondDialog = Utils.Lines(TextFileDialogue2.text);
            _thirdDialog = Utils.Lines(TextFileDialogue3.text);
        }
        else
        {
            _firstDialog[0] = "---";
            _secondDialog[0] = "---";
            _thirdDialog[0] = "---";
        }
    }

    public override void ExecuteAction(BaseProtagonistStats stats)
    {
        protaStats = stats;
        base.ExecuteAction(stats);
        if (MinotaurChasing) return;
        if (protaStats.OldTools) _hasTools = true;
        Messenger.Publish(new StopMessage());
        _idMessage = GetInstanceID();
        OldManGameObject.GetComponent<Animator>().SetInteger("OldManState",2);
        Messenger.Publish(new DialogueStartMessage(HandleDialogue(), _idMessage));
    }

    public void Handle(DialogueEndMessage message)
    {
        if (message.MessageId != _idMessage) return;
        if (!_allLighthousesActivated || !_hasTools)
        {
            OldManGameObject.GetComponent<Animator>().SetInteger("OldManState", 0);
            Messenger.Publish(new ContinueMessage());
            return;
        }
        //Present message win
        OldManGameObject.GetComponent<Animator>().SetInteger("OldManState", 1);
        StartCoroutine(WinCorutine());
        Instantiate(ExitExplosion, transform.position, Quaternion.identity);
    }

    IEnumerator WinCorutine()
    {
        yield return new WaitForSeconds(1f);
        transform.parent.gameObject.GetComponent<Renderer>().enabled = false;
        Prota.SetActive(false);
        Messenger.Publish(new WinGameMessage());
    }

    public void Handle(LighthouseActivatedMessage message)
    {
        
        _lighthousesActivated++;
        if (_lighthousesActivated >= 2)
        {
            _allLighthousesActivated = true;
        }
    }

    private string[] HandleDialogue()
    {
        if (_hasTools && _allLighthousesActivated)
        {
            return _secondDialog;
        }

        if (!_hasTools && _allLighthousesActivated)
        {
            return _thirdDialog;
        }

        if (_hasTools && !_allLighthousesActivated)
        {
            string[] tempString = {  2-_lighthousesActivated+" lighthouses remain to be activated" };
            return tempString;
        }

        if (!_hasTools && !_allLighthousesActivated)
        {
            return _firstDialog;
        }

        return _firstDialog;

    }

    public override void Handle(MinotaurChaseMessage message)
    {
        base.Handle(message);
        Instantiate(HideExplosion, new Vector2(transform.position.x,transform.position.y-1.87f), Quaternion.identity);
        OldManGameObject.SetActive(false);
        ShadowOldManGameObject.SetActive(false);
    }

    public override void Handle(ProtaEntersStructureMessage message)
    {
        base.Handle(message);
        OldManGameObject.SetActive(false);
        ShadowOldManGameObject.SetActive(false);
    }
}
