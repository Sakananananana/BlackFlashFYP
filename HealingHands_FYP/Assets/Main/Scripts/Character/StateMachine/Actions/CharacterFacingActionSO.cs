using UnityEngine;

[CreateAssetMenu(fileName = "CharacterFacingActionSO", menuName = "Scriptable Objects /State Machine /Actions /CharacterFacingActionSO")]
public class CharacterFacingActionSO : StateActionSO
{
    public TransformAnchor PlayerAnchor;
    protected override StateAction CreateAction() => new CharacterFacingAction();
}

public class CharacterFacingAction : StateAction
{
    Vector2 _direction;

    private float _dirUp;
    private float _dirDown;
    private float _dirLeft;
    private float _dirRight;
    private float _angle;

    private Animator _anim;
    private Transform _actor;
    private SpriteRenderer _sprRenderer;
    private TransformAnchor _protagonist; 

    public override void OnStateEnter(StateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        _actor = stateMachine.transform;
        _anim = stateMachine.GetComponent<Animator>();
        _sprRenderer = stateMachine.GetComponent<SpriteRenderer>();
        _protagonist = ((CharacterFacingActionSO)OriginSO).PlayerAnchor;
        
    }

    public override void OnUpdate()
    {
        if (_protagonist.isSet)
        {
            _direction = (_protagonist.Value.position - _actor.position).normalized;
            _angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;

            FlipCharacter();
            SetCharacterDirection();
        }
    }

    private void FlipCharacter()
    {
        if (_direction.x > 0)
        { _sprRenderer.flipX = true; }

        if (_direction.x < 0)
        { _sprRenderer.flipX = false; }
    }

    private void SetCharacterDirection()
    {
        if (_direction != Vector2.zero)
        {
            if (45 >= _angle && _angle >= -45)
            { _dirUp = 1; }
            else { _dirUp = 0; }

            if (-135 >= _angle && _angle >= -180 || 135 <= _angle && _angle <= 180)
            { _dirDown = 1; }
            else { _dirDown = 0; }

            if (135 >= _angle && _angle >= 45)
            { _dirRight = 1; }
            else { _dirRight = 0; }

            if (-135 <= _angle && _angle <= -45)
            { _dirLeft = 1; }
            else { _dirLeft = 0; }

            _anim.SetFloat("Up", _dirUp);
            _anim.SetFloat("Down", _dirDown);
            _anim.SetFloat("Left", _dirLeft);
            _anim.SetFloat("Right", _dirRight);
        }
    }

    public override void OnFixedUpdate() { }
    public override void OnStateExit() { base.OnStateExit(); }
}
