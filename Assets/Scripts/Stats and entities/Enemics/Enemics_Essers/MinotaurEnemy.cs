using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Pathfinding;
using UnityEngine;

public class MinotaurEnemy : BaseEnemy, IHandle<ContinueMessage>, IHandle<ProtaEntersStructureMessage>
{

    #region variables
    GameObject target;
    bool ha_llis = false;
    int cont_llis = 1;
    Mapa_General_p script_mapa;
    bool mirant_dreta = false;
    List<Vector2> tar_lis_lluny;
    Path tar_lis_prop;
    bool aprop = false;
    Vector2 prota_pos;
    public Punt2d coor;
    Punt2d coorProta;
    //variabels estats
    public State currentStateName;
    Vector2 targetDirection;
    Vector2 targetHeading;
    float variableSpeed;
    int gestor;
    bool attackCoroutineEnded = false;
    private GameObject spriteMinotaur;
    public GameObject crashGround;
    private bool _deepsleep = false;
    public TextAsset MinotaurWakeUpMessage;
    //definir variables maquina d'estats    
    public enum State
    {
        Patroll,
        Sleep,
        Attack,
        Chase
    }
    private Action currentState;
    #endregion

    #region state machine
    public void FixedUpdate()
    {
        currentState();
    }

    public void setState(State state)
    {
        if (_deepsleep) return;
        StateExit(currentStateName);
        currentStateName = state;
        switch (state)
        {
            case State.Patroll:
                character.CurrentSpeed = character.BaseSpeed;
                ha_llis = false;
                variableSpeed = 0;
                currentState = Patroll;
                break;
            case State.Sleep:
                ownRigidbody2D.velocity = Vector2.zero;
                currentState = () => { };
                break;
            case State.Attack:
                StartCoroutine("AttackPhases");
                currentState = Attack;
                break;
            case State.Chase:
                Messenger.Publish(new GameEventsGuiMessage(new string[2] {"The Minotaur has found you!","hide inside a lighthouse!" }));
                character.CurrentSpeed = character.AgroSpeed;
                Messenger.Publish(new MinotaurChaseMessage());
                gestor = 0;
                variableSpeed = 0;
                currentState = Chase;
                break;
            default:
                Debug.Log("unrecognized state");
                break;

        }
    }

    public void StateExit(State state)
    {
        switch (state)
        {
            case State.Patroll:
                break;
            case State.Sleep:
                break;
            case State.Attack:
                break;
            case State.Chase:
                break;
            default:
                Debug.Log("unrecognized state");
                break;
        }
    }
    #endregion

    #region states

    private void Patroll()
    {
        targetHeading = Que_target_aleatori() - pos_mod_bi();
        targetDirection = targetHeading.normalized;
        anim.SetFloat("posX", targetDirection.x);
        anim.SetFloat("posY", targetDirection.y);
        if (variableSpeed < character.CurrentSpeed) variableSpeed += 10 * Time.deltaTime;
        ownRigidbody2D.velocity = targetDirection * variableSpeed;
    }

    private void Attack()
    {
        ownRigidbody2D.velocity = new Vector2(0, 0);
        if (attackCoroutineEnded)
        {
            attackCoroutineEnded = false;
            setState(State.Chase);
        }
    }

    IEnumerator AttackPhases()
    {
        var tempMinSorting = spriteMinotaur.GetComponent<Sorting_sprites>();
        var tempMinRender = spriteMinotaur.GetComponent<SpriteRenderer>();
        var tempMinColl = spriteMinotaur.GetComponent<Collider2D>();
        if (targetDirection.y > 0) anim.SetInteger("attackPhase", 1);
        else anim.SetInteger("attackPhase", 2);
        ownRigidbody2D.mass = 200000;
        yield return new WaitForSeconds(1);
        anim.SetInteger("attackPhase", 3);
        yield return new WaitForSeconds(0.3f);
        tempMinSorting.enabled = true;
        tempMinRender.sortingLayerName = "Movim";
        tempMinRender.sortingOrder = 0;
        tempMinColl.enabled = true;
        Messenger.Publish(new CameraShakeMessage());
        var explosion = Instantiate(crashGround, new Vector3(ownTransform.position.x, ownTransform.position.y, -2f), Quaternion.identity) as GameObject;
        var explosionScript = explosion.GetComponent<explosionDamage>();
        explosionScript.protaG = ProtaG;
        explosionScript.damage = character.Attack;
        explosionScript.StartCoroutine("expansionBlastArea");
        yield return new WaitForSeconds(1);
        anim.SetInteger("attackPhase", 4);
        tempMinColl.enabled = false;
        tempMinRender.sortingLayerName = "porencima";
        tempMinRender.sortingOrder = 10;
        tempMinSorting.enabled = false;
        yield return new WaitForSeconds(1);
        anim.SetInteger("attackPhase", 5);
        ownRigidbody2D.mass = character.Mass;
        attackCoroutineEnded = true;
    }

    private void Chase()
    {
        var distanceToTarget = Vector3.Distance(ownTransform.position, target.transform.position);
        if (distanceToTarget > 10F)
        {
            if (gestor == 0 || gestor == 2)
            {
                ha_llis = false;
                gestor = 1;
            }
            targetHeading = Que_target_lluny() - pos_mod_bi();
        }
        else
        {
            if (gestor == 0 || gestor == 1)
            {
                ha_llis = false;
                gestor = 2;
            }
            if (distanceToTarget < 0.5F)
            {
                setState(State.Attack);
            }
            targetHeading = Que_target_aprop() - pos_mod_bi();
        }
        targetDirection = targetHeading.normalized;
        anim.SetFloat("posX", targetDirection.x);
        anim.SetFloat("posY", targetDirection.y);
        if (variableSpeed < character.CurrentSpeed) variableSpeed += 10 * Time.deltaTime;
        ownRigidbody2D.velocity = targetDirection * variableSpeed;
    }
    #endregion

    #region funcions inici
    protected override void Start()
    {
        character = Constants.Enemies.Minotaur;
        base.Start();
        spriteMinotaur = ownTransform.Find("MinotaurRo").gameObject;
        anim = spriteMinotaur.GetComponent<Animator>();
        coor = new Punt2d((int)(ownTransform.position.x / 17), (int)(ownTransform.position.y / 13));
        target = prota;
        ownRigidbody2D.mass = character.Mass;
        setState(State.Sleep);
        _deepsleep = true;
        script_mapa = GetComponent<Mapa_General_p>();
        seeker.pathCallback += OnPathComplete;
    }

    private IEnumerator StartSleepingRoutine()
    {
        yield return new WaitForSeconds(240f);
        if (_deepsleep)
        {
            _deepsleep = false;
            setState(State.Patroll);
            if (MinotaurWakeUpMessage != null)
            {
                Messenger.Publish(new GameEventsGuiMessage(Utils.Lines(MinotaurWakeUpMessage.text)));
            }
        }
    }
    #endregion

    #region funcions apli
    Vector2 Que_target_aleatori()
    {
        if (ha_llis)
        {
            if (tar_lis_lluny.Count > cont_llis)
            {
                if (Vector2.Distance(pos_mod_bi(), tar_lis_lluny[cont_llis]) > 0.1F)
                {
                    return tar_lis_lluny[cont_llis];
                }
                else
                {
                    cont_llis++;
                }

            }
            else
            {
                ha_llis = false;
                aprop = true;
            }

        }
        else
        {
            tar_lis_lluny = script_mapa.Ruta_a(coor);
            cont_llis = 0;
            //Debug.Log(tar_lis_lluny[cont_llis]);
            aprop = false;
            ha_llis = true;
            return tar_lis_lluny[cont_llis];
        }
        return target.transform.position;

    }

    Vector2 Que_target_aprop()
    {
        if (Vector2.Distance(prota_pos, target.transform.position) > 2F) ha_llis = false;

        if (ha_llis)
        {
            if (tar_lis_prop.vectorPath.Count > cont_llis)
            {
                if (Vector2.Distance(pos_mod_bi(), tar_lis_prop.vectorPath[cont_llis]) > 0.1F)
                {
                    return tar_lis_prop.vectorPath[cont_llis];
                }
                else
                {
                    cont_llis++;
                }

            }
            else aprop = true;

        }
        else
        {
            tar_lis_prop = seeker.StartPath(pos_mod_bi(), target.transform.position);
            prota_pos = target.transform.position;
            cont_llis = 1;
            aprop = true;
            ha_llis = true;
        }
        return target.transform.position;

    }

    Vector2 Que_target_lluny()
    {
        while (true)
        {

            if (Vector2.Distance(prota_pos, target.transform.position) > 20F) ha_llis = false;
            if (ha_llis)
            {
                if (tar_lis_lluny.Count > cont_llis)
                {
                    if (Vector2.Distance(pos_mod_bi(), tar_lis_lluny[cont_llis]) > 0.1F)
                    {
                        return tar_lis_lluny[cont_llis];
                    }
                    else
                    {
                        cont_llis++;
                    }
                }
                else
                {
                    ha_llis = false;
                }

            }
            else
            {
                tar_lis_lluny = script_mapa.Ruta_a(coor, prota.GetComponent<Protas>().Coor);
                prota_pos = target.transform.position;
                cont_llis = 0;
                ha_llis = true;

                //Debug.Log(tar_lis_lluny.Count);
                if (tar_lis_lluny.Count > 1)
                {
                    if (Vector2.Distance(tar_lis_lluny[cont_llis], tar_lis_lluny[cont_llis + 1]) > Vector2.Distance(pos_mod_bi(), tar_lis_lluny[cont_llis + 1]))
                    {
                        //Debug.Log("compleix");
                        cont_llis++;
                    }
                    return tar_lis_lluny[cont_llis];
                }

            }
            return target.transform.position;
        }

    }

    Vector2 pos_mod_bi()
    {
        return !aprop ? new Vector2(ownTransform.position.x, ownTransform.position.y + 2f) : new Vector2(ownTransform.position.x, ownTransform.position.y);
    }

    void gir()
    {
        mirant_dreta = !mirant_dreta;
        var theScale = ownTransform.localScale;
        theScale.x *= -1;
        ownTransform.localScale = theScale;
    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
    }

    public void OnDisable()
    {
        if (seeker != null) seeker.pathCallback -= OnPathComplete;
    }

    #endregion

    #region triggers i colliders
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Prota" && _deepsleep)
        {
            _deepsleep = false;
            setState(State.Chase);
        }
        if (other.gameObject.tag == "Prota" && currentStateName == State.Patroll)
        {
            _deepsleep = false;
            setState(State.Chase);
        }
        if (other.CompareTag("LighthouseArea"))
        {
            Messenger.Publish(new MinotaurEnterLighthouseAreaMessage(int.Parse(other.name)));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("LighthouseArea"))
        {
            Messenger.Publish(new MinotaurExitLighthouseAreaMessage(int.Parse(other.name)));
        }
    }


    #endregion

    #region Messages
    public override void Handle(StopMessage message)
    {
        setState(State.Sleep);
    }

    public void Handle(ContinueMessage message)
    {
        if (_deepsleep)
        {
            StartCoroutine(StartSleepingRoutine());
        }
        setState(State.Patroll);
    }


    public void Handle(ProtaEntersStructureMessage message)
    {
        setState(State.Patroll);
    }

    public override void Handle(PlayerDeathMessage message)
    {
        setState(State.Sleep);
    }
    #endregion
}
