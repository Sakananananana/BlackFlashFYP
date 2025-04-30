using System;
using UnityEngine;
using System.Collections;

public class Toad : MonoBehaviour
{
    //Move To Audio Script Later
    [SerializeField] private AudioChannelSO _audioChannelSO;
    [SerializeField] private AudioData _spitAudio;
    [SerializeField] private AudioConfiguration _audioConfig;

    [SerializeField] private GameObject _projectile;
    public Action<float> enemyHealthChange;

    //Need to change orientation based on character facing
    [SerializeField] private Transform _projectileOrigin;
    [NonSerialized] public Vector2 _direction;

    //Damage
    public bool CanTakeDamange = true;
    public bool IsAttacking = false;

    //Player Reference
    private GameObject _player;
    private float _angle;

    private void Awake()
    {

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

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        _direction = (_player.transform.position - transform.position).normalized;

        if (IsAttacking == false)
        {
            SetProjectileOrigin();
        }      
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
}
