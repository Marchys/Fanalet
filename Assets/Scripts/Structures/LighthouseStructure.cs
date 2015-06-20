using System;
using UnityEngine;
using System.Collections;

public class LighthouseStructure : MonoBehaviourEx, IHandle<EndTransitionGuiMessage>
{

    private Boolean _activated = false;
    private Animator ownAnimator;
    public GameObject LighthouseInterior;
    public Light LeftEyeLight;
    public Light RightEyeLight;
    public int LighthouseNumber = 0;

    private Vector2 _targetLocation;
    private int _idMessage;
    private GameObject _prota;
    private bool _firstTransition = true;

    private void Start()
    {
        _idMessage = GetInstanceID();
        ownAnimator = GetComponent<Animator>();
    }

    public void ActivateLighthouse(BaseCaracterStats typeActivation, int lighthousesActivated)
    {
        if (typeActivation.RedHearts != 0)
        {
            LeftEyeLight.color = Color.red;
            RightEyeLight.color = Color.red;
        }
        else if (typeActivation.BlueHearts != 0)
        {
            LeftEyeLight.color = Color.blue;
            RightEyeLight.color = Color.blue;
        }
        else if (typeActivation.YellowHearts != 0)
        {
            LeftEyeLight.color = Color.yellow;
            RightEyeLight.color = Color.yellow;
        }
        Messenger.Publish(new LighthouseActivatedMessage(LighthouseNumber));
        LighthouseInterior.GetComponentInChildren<Furnance>().SetLighthousetype(typeActivation);
        LighthouseInterior.GetComponentInChildren<LightUpgrader>().LighthousesActivated = lighthousesActivated;

        ownAnimator.SetBool("Activated", true);
        Messenger.Publish(new CameraShakeMessage());
        _activated = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_activated && other.CompareTag("Prota"))
        {
            Messenger.Publish(new StopMessage());
            _targetLocation = new Vector2(LighthouseInterior.transform.position.x + 11.75f, LighthouseInterior.transform.position.y - 2.75f);
            _prota = other.gameObject;
            _firstTransition = true;
            Messenger.Publish(new StartTransitionGuiMessage(Constants.GuiTransitions.HoleTransition, Constants.GuiTransitions.In, _idMessage));
        }
    }

    public void Handle(EndTransitionGuiMessage message)
    {
        if (message.MessageId != _idMessage) return;
        if (_firstTransition)
        {
            _prota.transform.position = _targetLocation;
            Camera.main.transform.position = new Vector2(_targetLocation.x, _targetLocation.y - 1.25f);
            Messenger.Publish(new ProtaEntersStructureMessage());
            StartCoroutine(ShowAgain());
            _firstTransition = false;
        }
        else
        {
            Messenger.Publish(new ContinueMessage());
        }

    }

    IEnumerator ShowAgain()
    {
        yield return new WaitForSeconds(0.7f);
        Messenger.Publish(new StartTransitionGuiMessage(Constants.GuiTransitions.HoleTransition, Constants.GuiTransitions.Out, _idMessage));
    }
}
