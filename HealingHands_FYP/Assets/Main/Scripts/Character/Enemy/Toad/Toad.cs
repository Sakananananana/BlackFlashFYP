using System;
using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class Toad : AnimationController
{
    //Move To Audio Script Later
    [SerializeField] private AudioChannelSO _audioChannelSO;
    [SerializeField] private AudioData _spitAudio;
    [SerializeField] private AudioConfiguration _audioConfig;

    [SerializeField] private GameObject _projectile;
    public Action<float> enemyHealthChange;

    //Need to change orientation based on character facing
    [SerializeField] private Transform _projectileOrigin;

    //Damage
    public bool CanTakeDamange = true;
    public bool IsAttacking = false;

    //Animation & Sprite
    private SpriteRenderer _sprRenderer;

    //Player Reference
    private GameObject _player;
    private float _angle;

    protected override void Awake()
    {
        base.Awake();
        
        _sprRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
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

        if (IsAttacking == false)
        {
            SetAnimationFloat();
            Flip();
            SetProjectileOrigin();
        }      
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

    private void SetAnimationFloat()
    {
        //If _direction's Vec2 is equal to zero this will not be called!
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

    public void PlayFireProjectileAudio() => _audioChannelSO.OnAudioPlayRequested(_spitAudio, _audioConfig, transform.position);
    public void FireProjectile()
    {
        PlayFireProjectileAudio();
        Quaternion angle = Quaternion.Euler(0, 0, 90);   
        var projectile = Instantiate(_projectile, _projectileOrigin.position, Quaternion.LookRotation(Vector3.forward, _direction) * angle);
        projectile.transform.SetParent(transform);
    }

    public void FinishAttack()
    { 
        IsAttacking = false;
    }

    private IEnumerator AttackCycle()
    {
        while (_projectile != null)
        {
            yield return new WaitForSeconds(3);
            IsAttacking = true;
        }
    }

    private IEnumerator DamageRecieveCooldown()
    {
        yield return new WaitForSeconds(0.8f);
        CanTakeDamange = true;
    }

    private IEnumerator DamageFlash()
    {
        _sprRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _sprRenderer.color = Color.white;
    }

}
