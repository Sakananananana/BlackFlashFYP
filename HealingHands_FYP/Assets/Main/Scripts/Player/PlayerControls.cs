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

    //Movement
    private Rigidbody2D _rBody;
    public bool CanMove = true;
    private Vector2 _dashDir;

    //Stats
    public float MoveSpeed;
    public float DashForce;

    //Animation
    private Animator _anim;
    private bool _isFacingRight = true;
    

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
        _player = GetComponent<Player>();
        _rBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (CanMove)
        AnimatorSetFloat();
    }

    private void FixedUpdate()
    {
        if (CanMove) { Movement(); }

        Dashing(DashPerformed);
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

        //Movements
        _rBody.velocity = Vector2.zero;
        CanMove = false;
        
        //Animations
        _anim.SetBool("IsAttacking", AttackPerformed);

        //Controls
        StartCoroutine(AttackCooldown());
    }

    private void TakeDamageHandler(Vector2 dmgDir)
    { 
        CanMove = false;
        StartCoroutine(TakeDamageCooldown(dmgDir));
    }

    private void Movement()
    {
        //Movements
        _rBody.velocity = MoveDir * MoveSpeed;

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
            _rBody.velocity = _dashDir * DashForce;

            //Control
            StartCoroutine(DashCooldown());
        }
        else
        {
            //Movement
            _dashDir = LastMoveDir;
        }
    }

    private IEnumerator DashCooldown()
    {
        _inputReader.DashEvent -= DashHandler;

        yield return new WaitForSeconds(0.3f);
        DashPerformed = false;
        _anim.SetBool("IsDashing", false);
        CanMove = true;

        yield return new WaitForSeconds(2f);
        _inputReader.DashEvent += DashHandler;
    }

    private IEnumerator AttackCooldown()
    {
        //Attack Performed
        while (AttackPerformed)
        { 
            yield return null;
        }

        _anim.SetBool("IsAttacking", false);
        CanMove = true;
    }

    private IEnumerator TakeDamageCooldown(Vector2 dmgDir)
    {
        _rBody.velocity = dmgDir * 2f;  

        yield return new WaitForSeconds(0.2f);

        CanMove = true;
    }

    //Considered as Movements? Changing Direction of the Player
    private void CharacterFacing()
    {
        if (MoveDir.x < 0 && _isFacingRight || MoveDir.x > 0 && !_isFacingRight)
        { 
            _isFacingRight = !_isFacingRight;
            transform.Rotate(new Vector3(0, 180, 0));
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
