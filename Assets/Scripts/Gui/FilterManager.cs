using UnityEngine;

public class FilterManager : MonoBehaviourEx, IHandle<BlurMessage>
{
    public GameObject BlurGameObject;

    public void Handle(BlurMessage message)
    {
       BlurGameObject.SetActive(message.Activated);
    }
}
