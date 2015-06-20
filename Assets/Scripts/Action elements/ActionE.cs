using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]

public abstract class ActionE : MonoBehaviourEx, IHandle<MinotaurChaseMessage>, IHandle<ProtaEntersStructureMessage>
{
    protected Animator EAnimator;
    protected bool MinotaurChasing = false;
    protected bool Blocked = false;
    public  GameObject Prota = null;
    
    protected void Start()
    {
        EAnimator=GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Prota" && !MinotaurChasing && !Blocked)
        {
            if (Prota == null) Prota = other.gameObject;
            EAnimator.SetInteger("animationState",1);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Prota" && !MinotaurChasing && !Blocked)
        {
            EAnimator.SetInteger("animationState",0);
        }
    }

    public virtual void ExecuteAction(BaseProtagonistStats stats)
    {
        EAnimator.SetInteger("animationState", 0);
    }

    public virtual void Handle(MinotaurChaseMessage message)
    {
        EAnimator.SetInteger("animationState", 0);
        MinotaurChasing = true;
    }

    public virtual void Handle(ProtaEntersStructureMessage message)
    {
        MinotaurChasing = false;
    }
}
