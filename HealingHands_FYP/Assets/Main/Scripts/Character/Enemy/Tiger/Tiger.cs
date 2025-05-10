using UnityEngine;

public class Tiger : MonoBehaviour
{
    [SerializeField] private TransformAnchor _protagonist;
    [SerializeField] private Transform _attackOrigin;

    private Vector2 _direction;
    private float _angle;

    public bool IsLunging = false;
    public bool IsLungeBiteHit = false;
    public bool IsSingleBite = false;
    public bool IsMultipleBite = false;

    // Update is called once per frame
    void Update()
    {
        _direction = (_protagonist.Value.position - transform.position).normalized;
        _angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;

        if (IsLunging == false)
        SetAttackOrigin();
    }

    public void SetAttackOrigin()
    {
        if (45 >= _angle && _angle >= -45)
        { 
            _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(0, 3.2f);
            _attackOrigin.transform.rotation = Quaternion.Euler(0f, 0f, 0);
        }
        else if (135 >= _angle && _angle >= 45)
        { 
            _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(2.5f, -0.7f); 
            _attackOrigin.transform.rotation = Quaternion.Euler(0f, 0f, 25f);
        }
        else if (-135 <= _angle && _angle <= -45)
        { 
            _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(-2.5f, -0.7f);
            _attackOrigin.transform.rotation = Quaternion.Euler(0f, 0f, -25f);
        }
        else
        { 
            _attackOrigin.transform.position = (Vector2)transform.position + new Vector2(0, -3.2f);
            _attackOrigin.transform.rotation = Quaternion.Euler(0f, 0f, 0);
        }
    }

    public void LungeBiteHandler() => IsLungeBiteHit = true;
    public void LungeBiteCancel() => IsLungeBiteHit = false;

    public void LungingHandler () => IsLunging = true;
    public void LungingCancel() => IsLunging = false;

    public void IsSingleBiteHandler() => IsSingleBite = true;
    public void IsSingleBiteCancel() => IsSingleBite = false;

    public void IsMultipleBiteHandler() => IsMultipleBite = true;
    public void IsMultipleBiteCancel() => IsMultipleBite = false;
}
