using UnityEngine;

public class Tiger : MonoBehaviour
{
    [SerializeField] private TransformAnchor _protagonist;
    [SerializeField] private Transform _attackOrigin;

    private Vector2 _direction;
    private float _angle;

    public bool IsLungeBiteHit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _direction = (_protagonist.Value.position - transform.position).normalized;
        _angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;


        SetAttackOrigin();
    }

    public void SetAttackOrigin()
    {
        if (45 >= _angle && _angle >= -45)
        { _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(0, 1.9f); }
        else if (135 >= _angle && _angle >= 45)
        { _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(0, 0); }
        else if (-135 <= _angle && _angle <= -45)
        { _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(0, 0); }
        else
        { _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(0, -1.9f); }
    }

    private void LungeBiteHandler() => IsLungeBiteHit = true;
    private void LungeBiteCancel() => IsLungeBiteHit = false;

}
