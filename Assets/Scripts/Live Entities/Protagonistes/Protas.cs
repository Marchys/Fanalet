using System.Collections;
using System.Runtime.Remoting;
using UnityEngine;

public abstract class Protas : MonoBehaviourEx, IVulnerable<int>, IMort, IHandle<StopMessage>, IHandle<ContinueMessage>
{
    protected BaseCaracterStats Character;
    private SpriteRenderer _spriteRend;
    protected bool knocked = false;
    private bool _immune = false;
    protected CanvasShow GuiReference;
    protected Transform OwnTransform;
    protected Rigidbody2D OwnRigidbody2D;
    public Punt2d Coor;
    public bool Activat = false;


    protected void Start()
    {
        _spriteRend = GetComponent<SpriteRenderer>();
        OwnTransform = GetComponent<Transform>();
        OwnRigidbody2D = GetComponent<Rigidbody2D>();
        Coor = new Punt2d((int)(transform.position.x / 17), (int)(transform.position.y / 13));
        Messenger.Publish(new UpdateGuiMessage(Character));
    }

    public void Mal(int mals)
    {
        if (_immune) return;
        StopCoroutine("Flash_red");
        StartCoroutine(Flash_red());
        StartCoroutine(Knocked());
        Character.OiLife = Character.OiLife - mals;
        Messenger.Publish(new UpdateGuiMessage(Character));
        if (Character.OiLife <= 0) Mort();
    }

    IEnumerator Flash_red()
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

    public void Mort()
    {
        Destroy(gameObject);
    }

    public virtual void Handle(StopMessage message)
    {
        Activat = false;
    }

    public virtual void Handle(ContinueMessage message)
    {
        Activat = true;
    }
   
}
