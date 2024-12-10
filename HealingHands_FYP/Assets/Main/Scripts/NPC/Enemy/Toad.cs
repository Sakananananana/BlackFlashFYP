using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

public class Toad : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _enemyHealth = 20;

    //Need to change orientation based on character facing
    [SerializeField] private Transform _projectileOrigin;

    //Damage
    private bool _canTakeDamange = true;
    private bool _isAttacking = false;

    //Animation & Sprite
    private Animator _anim;
    private SpriteRenderer _sprRenderer;

    //Player Reference
    [SerializeField] GameObject _player;
    private Vector2 _direction;
    private float _angle;

    private void Awake()
    {
        _anim = GetComponent<Animator>();   
        _sprRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(AttackCycle());
    }

    private void FixedUpdate()
    {
        _direction = (_player.transform.position - transform.position).normalized;

        if (_isAttacking == false)
        {
            SetAnimationFloat();
            Flip();
            SetProjectileOrigin();
        }      
    }

    private void SetAnimationFloat()
    {
        _anim.SetFloat("XDir", _direction.x);
        _anim.SetFloat("YDir", _direction.y);
    }

    public void RecieveDamage(float damage, Vector3 dmgDir)
    {
        if (_canTakeDamange == true)
        {
            _canTakeDamange = false;
            Debug.Log("Cannot Take Damage" + _canTakeDamange);

            _enemyHealth -= damage;
            StartCoroutine(DamageRecieveCooldown());
        }

        if (_enemyHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void Flip()
    {
        if (_direction.x > 0)
        { _sprRenderer.flipX = true; }

        if (_direction.x < 0)
        { _sprRenderer.flipX = false; }
    }

    private void SetProjectileOrigin()
    {
        _angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;

        if (45 >= _angle && _angle >= -45)
        { _projectileOrigin.transform.position = (Vector2)transform.position + Vector2.up; }
        else if (135 >= _angle && _angle >= 45 )
        { _projectileOrigin.transform.position = (Vector2)transform.position + Vector2.right; }
        else if (-135 <= _angle && _angle <= -45)
        { _projectileOrigin.transform.position = (Vector2)transform.position + Vector2.left; }
        else
        { _projectileOrigin.transform.position = (Vector2)transform.position - Vector2.up; }
    }

    private IEnumerator AttackCycle()
    {
        while (_projectile != null)
        {
            _isAttacking = false;
            yield return new WaitForSeconds(3);

            _isAttacking = true;
            _anim.SetBool("IsAttacking", true);

            while (_isAttacking == true)
            {
                yield return null;
            }
            _anim.SetBool("IsAttacking", false);
        }
    }

    public void FireProjectile()
    {
        Quaternion angle = Quaternion.Euler(0, 0, 90);   
        var projectile = Instantiate(_projectile, _projectileOrigin.position, Quaternion.LookRotation(Vector3.forward, _direction) * angle);
    }

    public void FinishAttack()
    { 
        _isAttacking = false;
    }

    private IEnumerator DamageRecieveCooldown()
    {
        yield return new WaitForSeconds(1f);
        _canTakeDamange = true;
    }

}
