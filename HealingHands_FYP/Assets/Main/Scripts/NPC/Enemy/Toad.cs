using System;
using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class Toad : AnimationController, IDamageable
{
    //Move To Audio Script Later
    [SerializeField] private AudioChannelSO _audioChannelSO;
    [SerializeField] private AudioData _spitAudio;
    [SerializeField] private AudioConfiguration _audioConfig;

    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _enemyHealth = 20;
    public Action<float> enemyHealthChange;

    //Need to change orientation based on character facing
    [SerializeField] private Transform _projectileOrigin;

    //Damage
    private bool _canTakeDamange = true;
    private bool _isAttacking = false;

    //Animation & Sprite
    private SpriteRenderer _sprRenderer;

    //Player Reference
    private GameObject _player;
    private float _angle;

    //private CinemachineImpulseSource _impulseSource;

    protected override void Awake()
    {
        base.Awake();
        
        _sprRenderer = GetComponent<SpriteRenderer>();
        //_impulseSource = GetComponent<CinemachineImpulseSource>();

        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

    }

    private void Start()
    {
        StartCoroutine(AttackCycle());
    }

    protected override void Update()
    {
        base.Update();
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
        ////If _direction's Vec2 is equal to zero this will not be called!
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

    public void RecieveDamage(float damage, Vector3 dmgDir)
    {
        if (_canTakeDamange == true)
        {
            _canTakeDamange = false;

            _enemyHealth -= damage;
            enemyHealthChange?.Invoke(damage);

            StartCoroutine(DamageRecieveCooldown());
            StartCoroutine(DamageFlash());
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

    

    public void FireProjectile()
    {
        PlayFireProjectileAudio();
        Quaternion angle = Quaternion.Euler(0, 0, 90);   
        var projectile = Instantiate(_projectile, _projectileOrigin.position, Quaternion.LookRotation(Vector3.forward, _direction) * angle);
        projectile.transform.SetParent(transform);
    }

    public void FinishAttack()
    { 
        _isAttacking = false;
    }

    public void PlayFireProjectileAudio() => _audioChannelSO.OnAudioPlayRequested(_spitAudio, _audioConfig, transform.position);

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

    private IEnumerator DamageRecieveCooldown()
    {
        yield return new WaitForSeconds(0.8f);
        _canTakeDamange = true;
    }

    private IEnumerator DamageFlash()
    {
        _sprRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _sprRenderer.color = Color.white;
    }

}
