using PlayerInputSystem;
using System;
using System.Collections;
using UnityEngine;

public class Protagonist : AnimationController
{
    //Controls
    [SerializeField] private InputReader _inputReader;
    [NonSerialized] public Vector2 MoveDir;
    [NonSerialized] public Vector2 MovementVector;
    [NonSerialized] public Vector2 LastMoveDir = Vector2.down;
    public bool DashPerformed = false;
    public bool WarpPerformed = false;
    public bool IsWarping = false;
    public bool AttackPerformed = false;

    
    [SerializeField] private BoolEventChannelSO _onCoinsBelowWarpCost;
    [SerializeField] private VoidEventChannelSO _onWarpPerformed;
    [SerializeField] private VoidEventChannelSO _onCombatEvent;
    [SerializeField] private SceneEventChannelSO _onSceneChange;
    private bool _inCombat = false;
    private bool _isCoinsBelowWarpCost = false;
    private GameSceneSO.GameSceneType _sceneType;

    [SerializeField] private CircleCollider2D _playerCollider;
    [SerializeField] private GameObject _attacker;
    [SerializeField] private GameObject _interactor;
    private Vector3 _attackDir;

    //Movement
    public bool CanMove = true;
    public bool CanDash = true;
    private Coroutine _currentCoroutine;

    //Stats
    public float MoveSpeed;
    public float DashForce;

    //Animation
    private SpriteRenderer _spRenderer;

    private void OnEnable()
    {
        if (CanDash)
        _inputReader.DashEvent += DashHandler;

        _inputReader.MoveEvent += MovementHandler;
        _inputReader.AttackEvent += AttackHandler;

        _onCoinsBelowWarpCost.OnEventRaised += CoinEnoughForWarp;
        _inputReader.StartWarpEvent += WarpingHandler;
        _onCombatEvent.OnEventRaised += OnCombatEventRaised;
        _onSceneChange.OnEventRaised += WarpActivationOnSceneChanges;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= MovementHandler;
        _inputReader.DashEvent -= DashHandler;
        _inputReader.AttackEvent -= AttackHandler;

        _onCoinsBelowWarpCost.OnEventRaised -= CoinEnoughForWarp;
        _inputReader.StartWarpEvent -= WarpingHandler;
        _onCombatEvent.OnEventRaised -= OnCombatEventRaised;
        _onSceneChange.OnEventRaised -= WarpActivationOnSceneChanges;
    }

    protected override void Awake()
    {
        base.Awake();
        _spRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        if (CanMove)
        {
            _direction = MoveDir;
            CharacterFacing();
            SetDirection();
        }   
    }

    private void FixedUpdate()
    {
        if (CanMove) 
            SetAttackDirection();
    }

    private void MovementHandler(Vector2 inputVector)
    {
        MoveDir = inputVector;

        if (inputVector != Vector2.zero)
            LastMoveDir = MoveDir;
    }

    private void OnCombatEventRaised()
    {
        _inCombat = !_inCombat;
        WarppableCheck();
    }

    private void WarpingHandler()
    {
        IsWarping = true;

        //prevent start again
        _inputReader.StartWarpEvent -= WarpingHandler;

        //subscribe to subsequent events
        _inputReader.WarpEvent += WarpHandler;
        _inputReader.CancelWarpEvent += WarpingCancelHandler;

    }

    private void WarpingCancelHandler()
    {
        IsWarping = false;

        //add back warp intiation event
        _inputReader.StartWarpEvent += WarpingHandler;

        //remove subsequent events
        _inputReader.WarpEvent -= WarpHandler;
        _inputReader.CancelWarpEvent -= WarpingCancelHandler;

    }

    private void WarpActivationOnSceneChanges(GameSceneSO.GameSceneType gameScene)
    { 
        _sceneType = gameScene;
        WarppableCheck();
    }

    private void CoinEnoughForWarp(bool val)
    { 
        _isCoinsBelowWarpCost = val;
        WarppableCheck();     
    }

    private void WarppableCheck()
    {
        if (_isCoinsBelowWarpCost == false &&
            _inCombat == false &&
            _sceneType == GameSceneSO.GameSceneType.Location_Dungeon)
        {
            _inputReader.StartWarpEvent += WarpingHandler;
        }
        else
        {
            _inputReader.StartWarpEvent -= WarpingHandler;
        }
        
    }

    private void WarpHandler()
    {
        WarpPerformed = true;
        _onWarpPerformed.RaiseEvent();
    }

    private void DashHandler() => DashPerformed = true;
    private void AttackHandler() => AttackPerformed = true;
    public void CancelAttackInput() => AttackPerformed = false;
    public void CancelDashInput() => DashPerformed = false;

    public IEnumerator DashCooldownTimer(float duration)
    {
        CanDash = false;
        _inputReader.DashEvent -= DashHandler;

        yield return new WaitForSeconds(duration);

        CanDash = true;
        _inputReader.DashEvent += DashHandler;

    }

    public void CoroutineTimer(IEnumerator coroutine)
    { 
        if(_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(coroutine);
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

        //to make sure attack happens in 4 directions only
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

        _attacker.transform.rotation = Quaternion.LookRotation(Vector3.forward, _attackDir);
        _interactor.transform.rotation = Quaternion.LookRotation(Vector3.forward, _attackDir);
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

    private void SetDirection()
    {
        //If _direction's Vec2 is equal to zero this will not be called!
        if (_direction != Vector2.zero)
        {
            if (_direction.y > 0 || (_direction.y > 0 && Mathf.Abs(_direction.x) > 0))
            { _dirUp = 1; }
            else
            { _dirUp = 0; }

            if (_direction.y < 0 || (_direction.y < 0 && Mathf.Abs(_direction.x) > 0))
            { _dirDown = 1; }
            else
            { _dirDown = 0; }

            if (_direction.x > 0.8)
            { _dirRight = 1; }
            else
            { _dirRight = 0; }

            if (_direction.x < -0.8)
            { _dirLeft = 1; }
            else
            { _dirLeft = 0; }

            _anim.SetFloat("Up", _dirUp);
            _anim.SetFloat("Down", _dirDown);
            _anim.SetFloat("Left", _dirLeft);
            _anim.SetFloat("Right", _dirRight);
        }
    }
}
