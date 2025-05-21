using UnityEngine;

[CreateAssetMenu(fileName = "HealthSO", menuName = "Scriptable Objects/HealthSO")]
public class HealthSO : ScriptableObject
{
    [Tooltip("The initial health")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    //Use to apply get hit effect
    [SerializeField] private SpriteRenderer _characterSprite;

    public void SetMaxHealth(int newValue)
    {
        _maxHealth = newValue;
    }

    public void SetCurrentHealth(int newValue)
    {
        _currentHealth = newValue;
    }

    public void InflictDamage(int DamageValue)
    {
        _currentHealth -= DamageValue;
    }

    public void RestoreHealth(int HealthValue)
    {
        _currentHealth += HealthValue;
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
        if(_currentHealth+HealthValue > _maxHealth)
        {
            _currentHealth = 20;
            _currentHealth = _maxHealth;
        }
    }
}
