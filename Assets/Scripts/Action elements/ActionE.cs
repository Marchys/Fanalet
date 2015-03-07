using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]

public abstract class ActionE : MonoBehaviourEx
{
    private Animator _eAnimator;
    
    protected void Start()
    {
        _eAnimator=GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Prota")
        {
            _eAnimator.SetInteger("animationState",1);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Prota")
        {
            _eAnimator.SetInteger("animationState",0);
        }
    }

    public virtual void ExecuteAction()
    {
        _eAnimator.SetInteger("animationState", 0);
    }
}
