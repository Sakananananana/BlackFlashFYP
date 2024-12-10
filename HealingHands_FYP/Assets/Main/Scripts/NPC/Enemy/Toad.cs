using System.Collections;
using UnityEngine;

public class Toad : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _enemyHealth = 20;

    //Need to change orientation based on character facing
    [SerializeField] private Transform _projectileOrigin;

    //Damage
    private bool _canTakeDamange = true;

    //Animation & Sprite
    private Animator _anim;
    private SpriteRenderer _sprRenderer;

    //Player Reference
    [SerializeField] GameObject _player;
    private Vector2 _directionClamp;
    private Vector2 _direction;
    private Quaternion _rotation;

    void Start()
    {
        StartCoroutine(FireProjectile());
    }

    private void FixedUpdate()
    {

        _direction = (_player.transform.position - transform.position).normalized;
        _directionClamp = Vector2.left * _direction.x + Vector2.down * _direction.y;

        Debug.Log(Mathf.Atan2(_directionClamp.x, _directionClamp.y) * Mathf.Rad2Deg);
        
        Debug.Log(_direction);
        Debug.Log(_directionClamp);

        _rotation = Quaternion.LookRotation(Vector3.forward, _direction);
        _projectileOrigin.transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
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
        _sprRenderer.flipX = true;
        _sprRenderer.flipX = false;
    }

    private IEnumerator FireProjectile()
    {
        while (_projectile != null)
        {
            for (int i = 1; i <= 5; i++)
            {
                yield return new WaitForSeconds(0.3f);
                var projectile = Instantiate(_projectile, _projectileOrigin.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(3);
        }
    }

    private IEnumerator DamageRecieveCooldown()
    {
        yield return new WaitForSeconds(1f);
        _canTakeDamange = true;
    }

}
