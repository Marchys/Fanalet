using System;
using UnityEngine;
using System.Collections;

public class LighthouseStructure : MonoBehaviourEx
{

    private Boolean _activated = false;
    private Animator ownAnimator;
    public GameObject LighthouseInterior;
    public Light LeftEyeLight;
    public Light RightEyeLight;


    private void Start()
    {
        ownAnimator = GetComponent<Animator>();
    }

    public void ActivateLighthouse(BaseCaracterStats typeActivation)
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
        LighthouseInterior.GetComponentInChildren<Furnance>().SetLighthousetype(typeActivation);
        ownAnimator.SetBool("Activated", true);
        Messenger.Publish(new CameraShakeMessage());
        _activated = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_activated && other.CompareTag("Prota"))
        {
            Vector2 targetLocation = new Vector2(LighthouseInterior.transform.position.x + 11.75f, LighthouseInterior.transform.position.y - 2.75f); ;
            Camera.main.transform.position = targetLocation;
            other.transform.position = targetLocation;
            Messenger.Publish(new ProtaEntersLighthouseMessage());
        }
    }

}
