using UnityEngine;

public class Crow : MonoBehaviour
{
    [SerializeField] private TransformAnchor _protagonist;
    [SerializeField] private Transform _attackOrigin;
    private Vector2 _direction;
    private float _angle;

    public bool IsAttacking;


    void Update()
    {
        _direction = (_protagonist.Value.position - transform.position).normalized;
        _angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;

        SetAttackOrigin();
    }

    private void SetAttackOrigin()
    {
        if (45 >= _angle && _angle >= -45)
        { _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(0, 0.65f); }
        else if (135 >= _angle && _angle >= 45)
        { _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(0.85f, -0.3f); }
        else if (-135 <= _angle && _angle <= -45)
        { _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(-0.85f, -0.3f); }
        else
        { _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(0, -0.55f); }
    }

    public void AttackHandler() => IsAttacking = true;
    public void AttackCancel() => IsAttacking = false;
}
