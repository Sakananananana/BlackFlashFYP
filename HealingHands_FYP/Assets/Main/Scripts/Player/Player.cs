using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private SpriteRenderer _characterSprite;
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    public float AttackDamage;

    public Action<float> HealthChange;
    public Action<Vector2> TakeDamage;


    void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void RecieveDamage(float damage, Vector3 dmgDir)
    {
        _currentHealth -= damage;
        HealthChange?.Invoke(damage);
        TakeDamage?.Invoke(dmgDir);
        StartCoroutine(DamageFlash());
    }

    public void Death()
    {
        Destroy(gameObject, 1.5f);
    }

    private IEnumerator DamageFlash()
    {
        _characterSprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _characterSprite.color = Color.white;
    }

}
