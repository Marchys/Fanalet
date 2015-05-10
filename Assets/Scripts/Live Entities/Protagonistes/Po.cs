using System;
using System.Collections;
using UnityEngine;

public class Po : Protas
{

    #region variables

    //Moviments
    private const float FactCanvi = 6f;
    private float _inputHoritzontal;
    private float _inputVertical;

    public float moveForce = 200f;
    public float maxSpeed = 5.5f;

    private Vector3 vector;

    //private float sprintSpeed;
    //variables dash
    private const bool DashUnlocked = true;
    private bool _dash = false;
    private bool _enDash = false;
    private bool _dashCooldown = false;
    private Vector2 _dashDir;
    private int _lastDashDir;
    //Temps 
    private float _tempLastDash = 0;
    private const float TempTapDash = 0.3f;
    private const float TempDashCooldown = 1.5f;
    private const float TempDash = 0.1f;

    //inputs   
    private bool _paradaActivaVer = false;
    private bool _paradaActivaHor = false;
    private int _horCheck = 0;
    private bool _horBlock = false;
    private int _verCheck = 0;
    private bool _verBlock = false;
    private bool _stopParadaVer = false;
    private bool _stopParadaHor = false;
    private bool _potActuar = false;
    //dispar
    public GameObject BolaFoc;
    private bool _disparant = false;
    private bool _onCooldownShot = false;
    private const float BolaSpeed = 15;
    private Vector2 _objectiuDis;
    private PolygonCollider2D _colDret;
    private BoxCollider2D _colEsq;
    private Animator _animatorPer;
    //boira
    private ParticleSystem _darkMist;
    //Gameobject Action
    private GameObject _actionGameObject;

    //definir variables maquina d'estats

    public enum State
    {
        Moving,
        Idl,
        Shoot,
        Dash,
    }

    //variables estats
    private Action _currentState;
    public State CurrentStateName;

    #endregion

    #region state machine

    public void SetState(State state)
    {
        CurrentStateName = state;
        switch (state)
        {
            case State.Moving:
                _animatorPer.SetInteger("estat_anim", 1);
                _currentState = Moving;
                break;
            case State.Idl:
                _animatorPer.SetInteger("estat_anim", 0);
                _currentState = Idl;
                break;
            case State.Shoot:
                _disparant = true;
                _animatorPer.SetInteger("estat_anim", 2);
                 Character.BaseSpeed += 0.3f;
                 var modifiedStats = new BaseCaracterStats();
                 modifiedStats.OiLife -= 1;
                 Character.UpdateStats(modifiedStats,Messenger);
                var bola =
                    Instantiate(BolaFoc, new Vector3(OwnTransform.position.x, OwnTransform.position.y, -0.85f),
                        Quaternion.identity) as GameObject;
                var tempDir = _objectiuDis - new Vector2(OwnTransform.position.x, OwnTransform.position.y);
                tempDir.Normalize();
                if (bola != null)
                {
                    bola.GetComponent<bola>().Shoot(tempDir,Character.Attack,4f,BolaSpeed);
                }
                else
                {
                    Debug.Log("La bola de foc prefab no hi es");
                }
                StartCoroutine(ShootCooldown());
                _currentState = Shoot;
                break;
            case State.Dash:
                _animatorPer.SetInteger("estat_anim", 3);
                StartCoroutine(Esperar_dash());
                _currentState = Dash;
                break;
            default:
                Debug.Log("unrecognized state");
                break;

        }
    }

    #endregion

    #region states

    private void Moving()
    {
        if (OwnRigidbody2D.velocity.magnitude < 0.35) SetState(State.Idl);
    }

    private void Idl()
    {
        if (OwnRigidbody2D.velocity.magnitude > 0.35) SetState(State.Moving);
    }

    private void Shoot()
    {
        if (_onCooldownShot) return;
        Character.BaseSpeed -= 0.3f;
        _disparant = false;
        SetState(State.Idl);
    }

    private void Dash()
    {
        _inputHoritzontal = _dashDir.x*Character.BaseSpeed;
        _inputVertical = _dashDir.y*Character.BaseSpeed;
        if (_enDash) return;
        SetState(State.Idl);
        _inputVertical = 0;
        _inputHoritzontal = 0;
        _dash = false;
    }

    #endregion

    #region funcions inicials

    private new void Start()
    {
        Character = new BasePoStats();
        base.Start();
        _animatorPer = gameObject.GetComponent<Animator>();
        SetState(State.Idl);
        _colDret = GetComponent<PolygonCollider2D>();
        _colEsq = GetComponent<BoxCollider2D>();
        _darkMist = OwnTransform.Find("blackMist").GetComponent<ParticleSystem>();
        _darkMist.enableEmission = false;
    }

    #endregion

    #region inputs mov i shot

    private void Update()
    {
        _currentState();
        if (_dash || !Activat) return;
        if (Input.GetButton("right"))
        {
            if (!_horBlock)
            {
                switch (_horCheck)
                {
                    case 0:
                        _horCheck = 1;
                        if (_paradaActivaHor) _stopParadaHor = true;
                        if (_inputHoritzontal < 1) _inputHoritzontal += FactCanvi*Time.deltaTime;
                        _animatorPer.SetFloat("MovX", 1);
                        _colEsq.enabled = false;
                        _colDret.enabled = true;
                        break;
                    //ownTransform.Rotate(Vector2.up, 180.0f);
                    case 1:
                        if (_inputHoritzontal < 1) _inputHoritzontal += FactCanvi*Time.deltaTime;
                        break;
                    default:
                        _horBlock = true;
                        if (!_paradaActivaHor) StartCoroutine(Parada(1));
                        break;
                }
            }
        }
        if (Input.GetButton("left"))
        {
            if (!_horBlock)
            {
                switch (_horCheck)
                {
                    case 0:
                        _horCheck = 2;
                        if (_paradaActivaHor) _stopParadaHor = true;
                        if (_inputHoritzontal > -1) _inputHoritzontal -= FactCanvi*Time.deltaTime;
                        _animatorPer.SetFloat("MovX", -1);
                        _colDret.enabled = false;
                        _colEsq.enabled = true;
                        break;
                    //ownTransform.Rotate(Vector2.up, -180.0f);
                    case 2:
                        if (_inputHoritzontal > -1) _inputHoritzontal -= FactCanvi*Time.deltaTime;
                        break;
                    default:
                        _horBlock = true;
                        if (!_paradaActivaHor) StartCoroutine(Parada(0));
                        break;
                }
            }
        }
        if (Input.GetButtonUp("right"))
        {
            if (_horBlock)
            {
                _horCheck = 0;
                _horBlock = false;
                if (_paradaActivaHor) _stopParadaHor = true;
            }
            else
            {
                _horCheck = 0;
                StartCoroutine(Parada(0));
            }

        }
        if (Input.GetButtonUp("left"))
        {
            if (_horBlock)
            {
                _horCheck = 0;
                _horBlock = false;
                if (_paradaActivaHor) _stopParadaHor = true;
            }
            else
            {
                _horCheck = 0;
                StartCoroutine(Parada(1));
            }
        }

        if (Input.GetButton("up"))
        {
            if (!_verBlock)
            {
                if (_verCheck == 0)
                {
                    _verCheck = 1;
                    if (_paradaActivaVer) _stopParadaVer = true;
                    if (_inputVertical < 1) _inputVertical += FactCanvi*Time.deltaTime;
                }
                else if (_verCheck == 1)
                {
                    if (_inputVertical < 1) _inputVertical += FactCanvi*Time.deltaTime;
                }
                else
                {
                    _verBlock = true;
                    if (!_paradaActivaVer) StartCoroutine(Parada(2));
                }
            }
        }
        if (Input.GetButton("down"))
        {
            if (!_verBlock)
            {
                if (_verCheck == 0)
                {
                    _verCheck = 2;
                    if (_paradaActivaVer) _stopParadaVer = true;
                    if (_inputVertical > -1) _inputVertical -= FactCanvi*Time.deltaTime;
                }
                else if (_verCheck == 2)
                {
                    if (_inputVertical > -1) _inputVertical -= FactCanvi*Time.deltaTime;
                }
                else
                {
                    _verBlock = true;
                    if (!_paradaActivaVer) StartCoroutine(Parada(3));
                }
            }
        }
        if (Input.GetButtonUp("up"))
        {
            if (_verBlock)
            {
                _verCheck = 0;
                _verBlock = false;
                if (_paradaActivaVer) _stopParadaVer = true;
            }
            else
            {
                _verCheck = 0;
                StartCoroutine(Parada(3));
            }

        }
        if (Input.GetButtonUp("down"))
        {
            if (_verBlock)
            {
                _verCheck = 0;
                _verBlock = false;
                if (_paradaActivaVer) _stopParadaVer = true;
            }
            else
            {
                _verCheck = 0;
                StartCoroutine(Parada(2));
            }
        }

        if (Input.GetButtonDown("disparar"))
        {
            if (!_disparant && !_onCooldownShot && !knocked)
            {
                RaycastHit xoc;
                Ray raig = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(raig, out xoc) && xoc.transform.name == "Det_mouse_input")
                {
                    //objectiu_dis = new Vector2(xoc.ownTransform.position.x,xoc.ownTransform.position.y);
                    _objectiuDis = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    SetState(State.Shoot);
                }

            }
        }

        if (Input.GetButtonDown("accio"))
        {
            if (_potActuar) _actionGameObject.GetComponent<ActionE>().ExecuteAction(Character);
        }
        //input_horitzontal=Input.GetAxis("Horizontal");
        //input_vertical = Input.GetAxis("Vertical");

        #endregion

        #region dash

        if (!_dashCooldown && DashUnlocked && !_disparant)
        {

            if (Input.GetButtonDown("right"))
            {
                if (_lastDashDir != 1)
                {
                    _tempLastDash = Time.time;
                    _lastDashDir = 1;
                }
                else
                {
                    if ((Time.time - _tempLastDash) < TempTapDash)
                    {
                        _dashDir = Vector2.right;
                        SetState(State.Dash);
                        _dash = true;

                    }
                    _tempLastDash = Time.time;
                }

            }

            else if (Input.GetButtonDown("left"))
            {
                if (_lastDashDir != 2)
                {
                    _tempLastDash = Time.time;
                    _lastDashDir = 2;
                }
                else
                {
                    if ((Time.time - _tempLastDash) < TempTapDash)
                    {
                        _dashDir = -Vector2.right;
                        SetState(State.Dash);
                        _dash = true;

                    }
                    _tempLastDash = Time.time;
                }
            }
            else if (Input.GetButtonDown("up"))
            {
                if (_lastDashDir != 3)
                {
                    _tempLastDash = Time.time;
                    _lastDashDir = 3;
                }
                else
                {
                    if ((Time.time - _tempLastDash) < TempTapDash)
                    {
                        _dashDir = Vector2.up;
                        SetState(State.Dash);
                        _dash = true;

                    }
                    _tempLastDash = Time.time;
                }
            }
            else if (Input.GetButtonDown("down"))
            {
                if (_lastDashDir != 4)
                {
                    _tempLastDash = Time.time;
                    _lastDashDir = 4;
                }
                else
                {
                    if ((Time.time - _tempLastDash) < TempTapDash)
                    {
                        _dashDir = -Vector2.up;
                        SetState(State.Dash);
                        _dash = true;

                    }
                    _tempLastDash = Time.time;
                }
            }
        }
        vector = new Vector2(_inputHoritzontal, _inputVertical);
    }

    # endregion

    #region funcions secundàries

   private IEnumerator Esperar_dash()
    {
        _enDash = true;
        yield return new WaitForSeconds(TempDash);
        _enDash = false;
        _dashCooldown = true;
        yield return new WaitForSeconds(TempDashCooldown);
        _dashCooldown = false;
    }

    private IEnumerator ShootCooldown()
    {
        _onCooldownShot = true;
        yield return new WaitForSeconds(Character.AttackCadence);
        _onCooldownShot = false;
    }

    private IEnumerator Parada(int id)
    {
        // 0 Horitzontal reducció cap a 0
        // 1 Horitzontal augment cap a 0
        // 2 Vertical reducció cap a 0
        // 3 Vertical augment cap a 0
        var tempCanvi = FactCanvi;
        switch (id)
        {
            case 0:
                //_paradaActivaHor = true;
                //while (_inputHoritzontal > 0)
                //{
                //    _inputHoritzontal -= tempCanvi*Time.deltaTime;
                //    if (_stopParadaHor)
                //    {
                //        _stopParadaHor = false;
                //        _paradaActivaHor = false;
                //        _inputHoritzontal = 0;
                //        yield break;
                //    }
                //    yield return null;
                //}
                _inputHoritzontal = 0;
                _paradaActivaHor = false;
                break;
            case 1:
                //_paradaActivaHor = true;
                //while (_inputHoritzontal < 0)
                //{
                //    _inputHoritzontal += tempCanvi*Time.deltaTime;
                //    if (_stopParadaHor)
                //    {
                //        _stopParadaHor = false;
                //        _paradaActivaHor = false;
                //        _inputHoritzontal = 0;
                //        yield break;
                //    }
                //    yield return null;
                //}
                _inputHoritzontal = 0;
                _paradaActivaHor = false;
                break;

            case 2:
                //_paradaActivaVer = true;
                //while (_inputVertical > 0)
                //{
                //    _inputVertical -= tempCanvi*Time.deltaTime;
                //    if (_stopParadaVer)
                //    {
                //        _stopParadaVer = false;
                //        _paradaActivaVer = false;
                //        _inputVertical = 0;
                //        yield break;
                //    }
                //    yield return null;
                //}
                _inputVertical = 0;
                _paradaActivaVer = false;
                break;

            case 3:
                //_paradaActivaVer = true;
                //while (_inputVertical < 0)
                //{
                //    _inputVertical += tempCanvi*Time.deltaTime;
                //    if (_stopParadaVer)
                //    {
                //        _stopParadaVer = false;
                //        _paradaActivaVer = false;
                //        _inputVertical = 0;
                //        yield break;
                //    }
                //    yield return null;
                //}
                _inputVertical = 0;
                _paradaActivaVer = false;
                break;

            default:
                Debug.Log("Error_parada");
                break;
        }
        yield return null;
    }

    #endregion

    #region triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Accio")
        {
            _actionGameObject = other.gameObject;
            _potActuar = true;
        }
        if (other.gameObject.tag == "Trigg" && Activat)
        {
            Coor = other.GetComponent<Trigg_ele>().coor;
            Messenger.Publish(new EnterAreaMessage(Coor));
        }
        if (other.gameObject.tag == "sub_mino")
        {
            _darkMist.enableEmission = true;
            //guiReference.setFade("black");
        }
        if (other.CompareTag("Item"))
        {
            Character.UpdateStats(other.GetComponent<Item>().itemStatsModified,Messenger);
           other.gameObject.GetComponent<Item>().Collected();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Accio")
        {
            _actionGameObject = null;
            _potActuar = false;
        }
        if (other.gameObject.tag == "sub_mino")
        {
            _darkMist.enableEmission = false;
            //guiReference.setFade("white");
        }
    }

    #endregion

    #region Updates

    private void FixedUpdate()
    {
        // Move senteces
        //rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, input_horitzontal * vel_caminar, 0.8f),
        //Mathf.Lerp(0, input_vertical * vel_caminar, 0.8f));
        //OwnRigidbody2D.velocity = vector.normalized*(Character.BaseSpeed+2f);
        OwnRigidbody2D.velocity = Vector2.ClampMagnitude(OwnRigidbody2D.velocity, maxSpeed);
        OwnRigidbody2D.AddForce(vector.normalized*moveForce);
        // OwnRigidbody2D.AddForce((vector.normalized*moveForce) * OwnRigidbody2D.mass / Time.fixedDeltaTime);
        //OwnRigidbody2D.velocity = Vector2.ClampMagnitude(OwnRigidbody2D.velocity, maxSpeed);
        //OwnRigidbody2D.velocity = new Vector2(_inputHoritzontal*Character.BaseSpeed, _inputVertical*Character.BaseSpeed);
    }

    #endregion

    #region Messages
    public override void Handle(StopMessage message)
    {
        _inputHoritzontal = 0;
        _inputVertical = 0;
        base.Handle(new StopMessage());
    }

    public override void Handle(ContinueMessage message)
    {
        base.Handle(new ContinueMessage());
        _verCheck = 0;
        _horCheck = 0;
    }
    #endregion
}
