using PlayerInputSystem;
using System;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //Controls
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Player _player;
    [NonSerialized] public Vector2 MoveDir;
    [NonSerialized] public Vector2 LastMoveDir = Vector2.down;
    public bool DashPerformed = false;
    public bool AttackPerformed = false;

    [SerializeField] private GameObject _meleeAim;
    [SerializeField] private PolygonCollider2D _meeleCollider;
    private Vector3 _attackDir;

    //Movement
    private Rigidbody2D _rBody;
    public bool CanMove = true;
    private Vector2 _dashDir;

    //Stats
    public float MoveSpeed;
    public float DashForce;

    //Animation
    private Animator _anim;
    private SpriteRenderer _spRenderer;

    private void OnEnable()
    {
        _inputReader.MoveEvent += MovementHandler;
        _inputReader.DashEvent += DashHandler;
        _inputReader.AttackEvent += AttackHandler;

        _player.TakeDamage += TakeDamageHandler;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= MovementHandler;
        _inputReader.DashEvent -= DashHandler;
        _inputReader.AttackEvent -= AttackHandler;

        _player.TakeDamage -= TakeDamageHandler;
    }

    private void Awake()
    {
        _spRenderer = GetComponent<SpriteRenderer>();
        _rBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (CanMove)
        AnimatorSetFloat();
    }

    private void FixedUpdate()
    {
        if (CanMove) 
        { 
            Movement();
            SetAttackDirection();
        }

        Dashing(DashPerformed);
        Attacking(AttackPerformed);
    }

    private void MovementHandler(Vector2 inputVector)
    { 
        MoveDir = inputVector;

        if (inputVector != Vector2.zero)
        { LastMoveDir = MoveDir; }
    }

    private void DashHandler()
    {
        DashPerformed = true;

        //Animation
        _anim.SetBool("IsDashing", DashPerformed);
    }

    private void AttackHandler()
    {
        AttackPerformed = true;
    }

    private void TakeDamageHandler(Vector2 dmgDir)
    { 
        CanMove = false;
        StartCoroutine(TakeDamageCooldown(dmgDir));
    }

    private void Movement()
    {
        //Movements
        _rBody.linearVelocity = MoveDir * MoveSpeed;

        //Movements? Because it change the direction
        CharacterFacing();

        //Animations
        _anim.SetBool("IsMoving", Mathf.Abs(MoveDir.magnitude) > 0.1);
    }

    private void Dashing(bool dashBool) 
    {
        if (dashBool)
        {
            CanMove = false;

            //Movement, only need this to run in fixed update
            _rBody.linearVelocity = _dashDir * DashForce;

            //Control
            StartCoroutine(DashCooldown());
        }
        else
        {
            //Movement
            _dashDir = LastMoveDir;
        }
    }

    private void Attacking(bool attackBool)
    {
        if (attackBool == true)
        {
            //Movements
            _rBody.linearVelocity = Vector2.zero;
            CanMove = false;


            //Animations
            _anim.SetBool("IsAttacking", attackBool);

            //Controls
            StartCoroutine(PauseMovement());
        }
    }

    private IEnumerator DashCooldown()
    {
        _inputReader.DashEvent -= DashHandler;

        //TODO: disable collider
        yield return new WaitForSeconds(0.3f);
        DashPerformed = false;
        _anim.SetBool("IsDashing", false);
        CanMove = true;

        yield return new WaitForSeconds(2f);
        _inputReader.DashEvent += DashHandler;
    }

    //When Attack Cannot Move
    private IEnumerator PauseMovement()
    {
        _meeleCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);

        //Attack Performed
        while (AttackPerformed)
        {
            if (_meeleCollider.enabled == true)
            {
                _meeleCollider.enabled = false;
            }

            yield return null;
        }
        _anim.SetBool("IsAttacking", false);
        CanMove = true;
    }

    private IEnumerator TakeDamageCooldown(Vector2 dmgDir)
    {
        //TODO: disable collider
        _rBody.linearVelocity = dmgDir * 5f;  

        yield return new WaitForSeconds(0.1f);

        CanMove = true;
    }

    private void SetAttackDirection()
    {
        if (MoveDir != Vector2.zero)
        {
            _attackDir = Vector3.left * MoveDir.x + Vector3.down * MoveDir.y;
        }
        else
        {
            _attackDir = Vector3.left * LastMoveDir.x + Vector3.down * LastMoveDir.y;
        }

        if (_attackDir.y <= -0.1f)
        {
            _attackDir.y = -1;
            _attackDir.x = 0;
        }
        else if (_attackDir.y >= 0.1f)
        {
            _attackDir.y = 1;
            _attackDir.x = 0;
        }
        _meleeAim.transform.rotation = Quaternion.LookRotation(Vector3.forward, _attackDir);
    }

    //Considered as Movements? Changing Direction of the Player
    private void CharacterFacing()
    {
        if (MoveDir.x < 0)
        {
            _spRenderer.flipX = true;
        }
        else if (MoveDir.x > 0 )
        { 
            _spRenderer.flipX = false;
        }
    }

    //Animations
    private void AnimatorSetFloat()
    {
        if (MoveDir != Vector2.zero)
        {
            _anim.SetFloat("XDir", MoveDir.x);
            _anim.SetFloat("YDir", MoveDir.y);
        }
        else if (MoveDir == Vector2.zero)
        {
            _anim.SetFloat("LastXDir", LastMoveDir.x);
            _anim.SetFloat("LastYDir", LastMoveDir.y);
        }
    }

    public void CancelAttackInput()
    {
        AttackPerformed = false;
    }

}
