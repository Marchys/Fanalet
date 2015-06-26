using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransitionsGui : MonoBehaviourEx, IHandle<StartTransitionGuiMessage>
{

    public GameObject HoleTransitionGui;
    private Material _holeTransitionGuiMaterial;

    public GameObject NormalTransitionGui;
    private Image NormalTransitionGuiImage;

    private IEnumerator _Routine;

    private float _holeRadius = 0;
    private int _currentTransitionMessageId = 0;

    void Start()
    {
        if (_holeTransitionGuiMaterial == null)
        {
            _holeTransitionGuiMaterial = HoleTransitionGui.GetComponent<Image>().material;
            NormalTransitionGuiImage = NormalTransitionGui.GetComponent<Image>();
            _holeTransitionGuiMaterial.SetFloat("_Radius", 0);
        }
        StartCoroutine(thuigny());
    }

    IEnumerator thuigny()
    {
        while (true)
        {
            Debug.Log(NormalTransitionGuiImage.color);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void Handle(StartTransitionGuiMessage message)
    {
        if (_holeTransitionGuiMaterial == null)
        {
            _holeTransitionGuiMaterial = HoleTransitionGui.GetComponent<Image>().material;
            NormalTransitionGuiImage = NormalTransitionGui.GetComponent<Image>();
            _holeTransitionGuiMaterial.SetFloat("_Radius", 0);
        }
        if (_Routine != null)
        {
            StopCoroutine(_Routine);
            _Routine = null;
        }
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

            case Constants.GuiTransitions.NormalTransition:
                switch (message.TransitionPhase)
                {
                    case Constants.GuiTransitions.In:
                        _Routine = InNormalTransition();
                        break;
                    case Constants.GuiTransitions.Out:
                        _Routine = OutNormalTransition();
                        break;
                }
                break;
            default:
                Debug.Log("target transition was not found");
                break;
        }
        if (_Routine != null) StartCoroutine(_Routine);
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

    IEnumerator OutNormalTransition()
    {
        Color tempColor = NormalTransitionGuiImage.color;
        while (tempColor.a > 0)
        {
            tempColor.a -= 0.05f;
            NormalTransitionGuiImage.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
        Messenger.Publish(new EndTransitionGuiMessage(_currentTransitionMessageId));
    }

    IEnumerator InNormalTransition()
    {
        Color tempColor = NormalTransitionGuiImage.color;
        while (tempColor.a < 1)
        {
            tempColor.a += 0.05f;
            NormalTransitionGuiImage.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
        Messenger.Publish(new EndTransitionGuiMessage(_currentTransitionMessageId));
    }
}
