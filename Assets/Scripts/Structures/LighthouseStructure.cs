using UnityEngine;
using System.Collections;

public class LighthouseStructure : MonoBehaviourEx
{

    private Animator ownAnimator;
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
        ownAnimator.SetBool("Activated", true);
        Messenger.Publish(new CameraShakeMessage());
    }
}
