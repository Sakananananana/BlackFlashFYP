using UnityEngine;

public class Boar : MonoBehaviour
{
    [SerializeField] private TransformAnchor _protagonist;
    [SerializeField] private Transform _attackOrigin;

    public bool IsDizzy = false;
    public bool IsAttackHit = false;
    public bool IsAttacking = false;
    public bool IsPreparingAttack = false;

    private Ram_Attack _attack;
    private Vector2 _direction;
    private float _angle;
    

    private void Awake()
    {
       _attack = GetComponentInChildren<Ram_Attack>();
    }

    private void OnEnable()
    {
        _attack.IsCollidedWithTarget += AttackHit;
    }

    private void OnDisable()
    {
        _attack.IsCollidedWithTarget -= AttackHit;
    }

    private void Update()
    {
        _direction = (_protagonist.Value.position - transform.position).normalized;
        _angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;

        if (!IsAttacking)
        SetAttackOrigin();
    }

    private void SetAttackOrigin()
    {
        if (45 >= _angle && _angle >= -45)
        { _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(0, 0.45f); }
        else if (135 >= _angle && _angle >= 45)
        { _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(0.3f, -0.18f); }
        else if (-135 <= _angle && _angle <= -45)
        { _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(-0.3f, -0.18f); }
        else
        { _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(0, -0.45f); }
    }

    public void AttackHandler() => IsAttacking = true;
    public void CancelAttackInput() => IsAttacking = false; 

    public void AttackHit() => IsAttackHit = true;
    public void CancelAttackHit() => IsAttackHit = false;

    public void IsPreparingAtk() => IsPreparingAttack = true;
    public void CancelAttackPrepare() => IsPreparingAttack = false;
}
