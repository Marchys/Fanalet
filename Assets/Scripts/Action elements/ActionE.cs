using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]

public abstract class ActionE : MonoBehaviourEx, IHandle<MinotaurChaseMessage>, IHandle<ProtaEntersStructureMessage>
{
    protected Animator EAnimator;
    protected bool minotaurChasing = false;
    protected bool Blocked = false;
    public  GameObject Prota = null;
    
    protected void Start()
    {
        EAnimator=GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Prota" && !minotaurChasing && !Blocked)
        {
            if (Prota == null) Prota = other.gameObject;
            EAnimator.SetInteger("animationState",1);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Prota" && !minotaurChasing && !Blocked)
        {
            EAnimator.SetInteger("animationState",0);
        }
    }

    public virtual void ExecuteAction(BaseCaracterStats stats)
    {
       EAnimator.SetInteger("animationState", 0);
    }

    public virtual void Handle(MinotaurChaseMessage message)
    {
        EAnimator.SetInteger("animationState", 0);
        minotaurChasing = true;
    }

    public void Handle(ProtaEntersStructureMessage message)
    {
        minotaurChasing = false;
    }
}
