using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySlug : BaseEnemy
{
    #region variables
    bool rayxoc;

    GameObject target;
    bool patrullant_sub = false;
    Vector2 random_dir = new Vector2(0, 0);
    bool mirant_dreta = false;
    // gestio posicions
    Path pathList;
    bool pathListExists = false;
    int cont_llis = 1;
    Vector2 prota_pos;
    private Vector2 _lastPosition;
    bool closeToPlayer = false;
    // variables knockback
    Vector2 knockDirection;
    float knockStrenght;
    //state variables
    Vector2 targetDirection;
    Vector2 targetHeading;
    float variableSpeed;
    //state variables bool
    bool playerSeen = false;
    //distancia check position
    float disCheck;
    //type slug of enemy
    public SlugType slugyStats;
    public enum SlugType
    {
        StandardSlug,
        RedSlug,
        BlueSlug,
        YellowSlug
    }
    //definir variables maquina d'estats       
    public enum State
    {
        Patroll,
        Sleep,
        Attack,
        Chase,
        Knocked
    }
    private Action currentState;
    public State currentStateName;
    #endregion

    #region state machine
    public void FixedUpdate()
    {
        currentState();
    }

    public void setState(State state)
    {
        stateExit(currentStateName);
        currentStateName = state;
        switch (state)
        {
            case State.Patroll:
                character.CurrentSpeed = character.BaseSpeed;
                _lastPosition = new Vector2(0, 0);
                pathListExists = false;
                variableSpeed = 0;
                StartCoroutine("Temps_patrulla");
                currentState = Patroll;
                break;
            case State.Sleep:
                anim.SetInteger("Anim", 3);
                ownRigidbody2D.velocity = new Vector2(0, 0);
                currentState = () => { };
                break;
            case State.Attack:
                pathListExists = false;
                anim.SetInteger("Anim", 2);
                ownRigidbody2D.mass = 200000000;
                currentState = Attack;
                break;
            case State.Chase:
                character.CurrentSpeed = character.AgroSpeed;
                anim.SetInteger("Anim", 2);
                variableSpeed = 0;
                currentState = Chase;
                break;
            case State.Knocked:
                anim.SetInteger("Anim", 4);
                variableSpeed = 0;
                currentState = Knocked;
                break;
            default:
                Debug.Log("unrecognized state");
                break;

        }

    }

    public void stateExit(State state)
    {
        switch (state)
        {
            case State.Patroll:
                    StopCoroutine("Temps_patrulla");
                break;
            case State.Sleep:
                break;
            case State.Attack:
                ownRigidbody2D.mass = character.Mass;
                break;
            case State.Chase:
                break;
            case State.Knocked:
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
        if (patrullant_sub)
        {
            random_dir = new Vector2(0, 0);
        }else if (new Vector2(ownTransform.position.x, ownTransform.position.y) == _lastPosition)
        {
            Random_dir();
        }
        _lastPosition = ownTransform.position;
        ownRigidbody2D.velocity = random_dir * character.CurrentSpeed;
        if (ownRigidbody2D.velocity.x > 0 && !mirant_dreta) gir();
        else if (ownRigidbody2D.velocity.x < 0 && mirant_dreta) gir();
    }

    private void Attack()
    {
        ownRigidbody2D.velocity = new Vector2(0, 0);
    }
    private void Chase()
    {
        targetHeading = Que_target() - pos_mod_bi();
        targetDirection = targetHeading.normalized;
        //Debug.DrawRay(ownTransform.position, targetDirection*10, Color.green);     
        if (variableSpeed < character.CurrentSpeed) variableSpeed += 10 * Time.deltaTime;
        ownRigidbody2D.velocity = targetDirection * variableSpeed;
        if (ownRigidbody2D.velocity.x > 0 && !mirant_dreta) gir();
        else if (ownRigidbody2D.velocity.x < 0 && mirant_dreta) gir();
        //Alert!!! mayby need to be fixated update
    }
    private void Knocked()
    {
        if (knockStrenght > 0)
        {
            ownRigidbody2D.velocity = knockDirection * knockStrenght;
            knockStrenght -= 10f * Time.deltaTime;
        }
        else setState(State.Chase);

    }


    #endregion

    #region funcions inici
    new void Start()
    {
        switch (slugyStats)
        {
            case SlugType.StandardSlug:
                character = new EnemySlugStats();
                break;
            case SlugType.RedSlug:
                character = new EnemyRedSlugStats();
                break;
            case SlugType.BlueSlug:
                character = new EnemyBlueSlugStats();
                break;
            case SlugType.YellowSlug:
                character = new EnemyYellowSlugStats();
                break;
        }
        base.Start();
        disCheck = Random.Range(1F, 2F);
        Physics2D.IgnoreLayerCollision(9, 9, true);
        ownRigidbody2D.mass = character.Mass;
        target = prota;
        setState(State.Sleep);
    }
    #endregion

    #region funcions secundaries
    void Random_dir()
    {
        var temp_angle = Random.Range(90, 271);
        random_dir = new Vector2((float)Mathf.Cos(temp_angle), (float)Mathf.Sin(temp_angle));
    }

    IEnumerator Temps_patrulla()
    {
        while (true)
        {
            patrullant_sub = true;
            anim.SetInteger("Anim", 0);
            yield return new WaitForSeconds(Random.Range(0, 4));
            patrullant_sub = false;
            anim.SetInteger("Anim", 1);
            yield return new WaitForSeconds(Random.Range(3, 6));
        }
      
    }

    void gir()
    {
        mirant_dreta = !mirant_dreta;
        var theScale = ownTransform.localScale;
        theScale.x *= -1;
        ownTransform.localScale = theScale;
    }
    #endregion

    #region triggers
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Prota") return;
        if (currentStateName == State.Sleep)
        {
            playerSeen = true;
        }
        else if (currentStateName != State.Attack && currentStateName != State.Chase)
        {
            playerSeen = true;
            setState(State.Chase);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Prota")
        {
            if (currentStateName == State.Sleep)
            {
                playerSeen = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (currentStateName != State.Sleep)
        {
             if (other.gameObject.tag == "Prota" && currentStateName == State.Chase)
            {
                setState(State.Attack);
            }
 
        }
    }

    #endregion

    #region funcions apli
    Vector2 Que_target()
    {
        if (Vector2.Distance(prota_pos, target.transform.position) > disCheck) pathListExists = false;

        if (pathListExists)
        {
            if (pathList.vectorPath.Count > cont_llis)
            {
                if (Vector2.Distance(pos_mod_bi(), pathList.vectorPath[cont_llis]) > 0.1F)
                {
                    return pathList.vectorPath[cont_llis];
                }
                else
                {
                    cont_llis++;
                    //if (pathList.vectorPath.Count > cont_llis + 1)
                    //{
                    //    int layer_gu = 1 << 11;
                    //    RaycastHit2D hit = Physics2D.BoxCast(ownTransform.position, new Vector2(3.5f, 3.5f), 0f, new Vector2(pathList.vectorPath[cont_llis + 1].x, pathList.vectorPath[cont_llis + 1].y) - pos_mod_bi(), 8f, layer_gu);
                    //    if (hit.collider == null)
                    //    {
                    //        cont_llis += 1;
                    //    }
                    //}                

                }

            }
            else closeToPlayer = true;

        }
        else
        {
            pathList = seeker.StartPath(pos_mod_bi(), target.transform.position);
            prota_pos = target.transform.position;
            cont_llis = 1;
            //if (pathList.vectorPath.Count > cont_llis + 1)
            //{
            //    int layer_gu = 1 << 11;
            //    RaycastHit2D hit = Physics2D.BoxCast(ownTransform.position, new Vector2(3.5f, 3.5f), 0f, new Vector2(pathList.vectorPath[cont_llis + 1].x, pathList.vectorPath[cont_llis + 1].y) - pos_mod_bi(), 8f, layer_gu);
            //    if (hit.collider == null)
            //    {
            //        cont_llis += 1;
            //    }              
            //}                          
            closeToPlayer = false;
            pathListExists = true;
        }
        return target.transform.position;
    }

    public void switchZOrder(bool activate)
    {
        layerSorting.enabled = activate;
    }

    public void WakeUp()
    {
        seeker.pathCallback += OnPathComplete;
        setState(playerSeen ? State.Chase : State.Patroll);
    }

    public void Sleep()
    {
        if (seeker.pathCallback != null) seeker.pathCallback -= OnPathComplete;
        setState(State.Sleep);
        playerSeen = false;
    }

    Vector2 pos_mod_bi()
    {
        return !closeToPlayer ? new Vector2(ownTransform.position.x, ownTransform.position.y - 0.44f) : new Vector2(ownTransform.position.x, ownTransform.position.y);
    }

    public void Atac()
    {
        if (currentStateName != State.Sleep)
        {
            setState(State.Attack);
        }
    }

    public void Pers()
    {
        if (currentStateName == State.Attack)
        {
            setState(State.Chase);
        }
    }

    public void Impacte_mal(Impacte xoc)
    {

        if (currentStateName != State.Sleep)
        {
            knockDirection = xoc.Direccio;
            knockStrenght = xoc.Forsa;
            Mal(xoc.Mal);
            setState(State.Knocked);
        }


    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
    }

    public void OnDisable()
    {
        if (seeker.pathCallback != null) seeker.pathCallback -= OnPathComplete;
    }

    public override void FerMal()
    {
        if (currentStateName == State.Attack) ProtaG.Mal(character.Attack);
    }

    public override void Handle(StopMessage message)
    {
        
    }
   
    public override void Handle(PlayerDeathMessage message)
    {
        setState(State.Sleep);
        ownRigidbody2D.velocity = Vector2.zero;
    }
    #endregion



}

