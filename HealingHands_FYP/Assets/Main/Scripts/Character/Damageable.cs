using UnityEngine;
using System;

public class Damageable : MonoBehaviour
{
    [SerializeField] private HealthConfigSO _healthConfigSO;
    [SerializeField] public HealthSO _currentHealthSO;

    [Header("Broadcasting on...")]
    [SerializeField] public VoidEventChannelSO _updateHealthUI;
    [SerializeField] public VoidEventChannelSO _deathEvent;

    [Header("Listening to...")]
    [SerializeField] private IntEventChannelSO _restoreHealth;

    public Vector2 HitDirection { get; set; }
    public bool GetHit { get; set; }
    public bool IsDead { get; set; }

    private void Awake()
    {
        if (_currentHealthSO == null && _updateHealthUI == null)
        {
            _currentHealthSO = ScriptableObject.CreateInstance<HealthSO>();
            _updateHealthUI = ScriptableObject.CreateInstance<VoidEventChannelSO>();

            _currentHealthSO.SetMaxHealth(_healthConfigSO.InitialHealth);
            _currentHealthSO.SetCurrentHealth(_healthConfigSO.InitialHealth);
        }

        if (_updateHealthUI != null)
        {
            if (_currentHealthSO.CurrentHealth <= 0)
            { Revive(); }

            _updateHealthUI.RaiseEvent(); 
        }
    }

    private void OnEnable()
    {
        if (_restoreHealth != null)
        { _restoreHealth.OnEventRaised += ReceiveHeal; }
    }

    private void OnDisable()
    {
        if (_restoreHealth != null)
        { _restoreHealth.OnEventRaised -= ReceiveHeal; }
    }

    public void ReceiveHeal(int amount)
    {
        if (IsDead)
            return; 

        _currentHealthSO.RestoreHealth(amount);
    }

    public void RecieveAttack(int damage, Vector2 dmgDir = default)
    {
        if (IsDead || GetHit)
            return;

        _currentHealthSO.InflictDamage(damage);
        HitDirection = dmgDir;
        GetHit = true;

        if (_updateHealthUI != null)
        { _updateHealthUI.RaiseEvent(); }

        if (_currentHealthSO.CurrentHealth <= 0)
        { 
            IsDead = true;

            if (_deathEvent != null)
                _deathEvent.RaiseEvent();
        }
    }

    public void Revive()
    {
        _currentHealthSO.SetCurrentHealth(_healthConfigSO.InitialHealth);

        if (_updateHealthUI != null)
        { _updateHealthUI.RaiseEvent(); }

        IsDead = false;
    }
}
