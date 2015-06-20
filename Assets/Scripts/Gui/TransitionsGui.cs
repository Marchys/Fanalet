using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionsGui : MonoBehaviourEx, IHandle<StartTransitionGuiMessage>
{

    public GameObject HoleTransitionGui;
    private Material _holeTransitionGuiMaterial;
    private IEnumerator _Routine;

    private float _holeRadius = 0;

    private int _currentTransitionMessageId = 0;

    void Start()
    {
        _holeTransitionGuiMaterial = HoleTransitionGui.GetComponent<Image>().material;
        _holeTransitionGuiMaterial.SetFloat("_Radius", 0);
    }

    public void Handle(StartTransitionGuiMessage message)
    {
        StopAllCoroutines();
        _currentTransitionMessageId = message.MessageId;
        switch (message.Transition)
        {
            case Constants.GuiTransitions.HoleTransition:
                switch (message.TransitionPhase)
                {
                    case Constants.GuiTransitions.In:
                         _Routine = InHoleTransition();
                        break;
                    case Constants.GuiTransitions.Out:
                        _Routine = OutHoleTransition();
                        break;
                }
                break;
            default:
                Debug.Log("target transition was not found");
                break;
        }
        if(_Routine != null)StartCoroutine(_Routine);
    }

    IEnumerator OutHoleTransition()
    {
        while (_holeRadius < 1.5f)
        {
            _holeRadius += 0.05f;
            _holeTransitionGuiMaterial.SetFloat("_Radius", _holeRadius);
            yield return new WaitForSeconds(0.01f);
        }
        Messenger.Publish(new EndTransitionGuiMessage(_currentTransitionMessageId));
    }

    IEnumerator InHoleTransition()
    {
        while (_holeRadius > 0)
        {
            _holeRadius -= 0.05f;
            if (_holeRadius < 0) _holeRadius = 0;
            _holeTransitionGuiMaterial.SetFloat("_Radius", _holeRadius);
            yield return new WaitForSeconds(0.01f);
        }
        Messenger.Publish(new EndTransitionGuiMessage(_currentTransitionMessageId));
    }
}
