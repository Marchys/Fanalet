using System.Collections;
using System.Runtime.Remoting;
using Pathfinding;
using UnityEngine;

public abstract class Protas : MonoBehaviourEx, IVulnerable<int>, IHandle<StopMessage>, IHandle<ContinueMessage>, IHandle<PlayerDeathMessage>, IHandle<ProtaEntersStructureMessage>, IHandle<ProtaExitsStructureMessage>
{
    // Character public temoraly so cheats can se it
    public BaseProtagonistStats Character;
    private SpriteRenderer _spriteRend;
    protected bool knocked = false;
    private bool _immune = false;
    protected Transform OwnTransform;
    protected Rigidbody2D OwnRigidbody2D;
    public Punt2d Coor;
    public bool Activat = false;
    private bool _insideStructure = false;

    protected virtual void Start()
    {
        _spriteRend = GetComponent<SpriteRenderer>();
        OwnTransform = GetComponent<Transform>();
        OwnRigidbody2D = GetComponent<Rigidbody2D>();
        Coor = new Punt2d((int)(transform.position.x / 17), (int)(transform.position.y / 13));
        Messenger.Publish(new UpdateGuiMessage(Character));
    }

    public virtual void Mal(int damageAmount)
    {
        if (_immune) return;
        StopCoroutine("Flash_red");
        StartCoroutine(Flash_red());
        StartCoroutine(Knocked());
        var modifiedStats = new BaseCaracterStats();
        modifiedStats.Life -= damageAmount;
        Character.UpdateStats(modifiedStats,Messenger);
    }

    private void SwitchLifeWear()
    {
        if (_insideStructure || !Activat)
        {
           StopCoroutine("LifeWear"); 
        }else
        {
            StopCoroutine("LifeWear"); 
            StartCoroutine("LifeWear");
        }
    }

    private IEnumerator LifeWear()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            var modifiedStats = new BaseCaracterStats();
            modifiedStats.Life -= 1;
            Character.UpdateStats(modifiedStats, Messenger);
        }
        
    }
    
    private IEnumerator Flash_red()
    {
        var alCol = 0.2f;
        _spriteRend.color = new Color(1f, alCol, alCol, 1F);
        while (true)
        {
            if (alCol < 1f)
            {
                _spriteRend.color = new Color(1f, alCol += 1f * Time.deltaTime, alCol += 1f * Time.deltaTime, 1F);
            }
            yield return null;
        }
    }

    IEnumerator Knocked()
    {
        knocked = true;
        StartCoroutine(Immune());
        yield return new WaitForSeconds(0.3F);
        knocked = false;
    }

    IEnumerator Immune()
    {
        _immune = true;
        yield return new WaitForSeconds(0.3F);
        _immune = false;
    }

    public virtual void Handle(StopMessage message)
    {
        
        Activat = false;
        SwitchLifeWear();
    }

    public virtual void Handle(ContinueMessage message)
    {
        Activat = true;
        SwitchLifeWear();
    }


    public void Handle(PlayerDeathMessage message)
    {
        Destroy(gameObject);
    }

    public void Handle(ProtaEntersStructureMessage message)
    {
        _insideStructure = true;
        SwitchLifeWear();
    }

    public void Handle(ProtaExitsStructureMessage message)
    {
        _insideStructure = false;
        SwitchLifeWear();
    }
}
