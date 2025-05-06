using UnityEngine;
using System;

public class Damageable : MonoBehaviour
{
    [SerializeField] private BossHealthUIItem _bossHealthUIItem;
    [SerializeField] private HealthConfigSO _healthConfigSO;
    [SerializeField] public HealthSO _currentHealthSO;

    [Header("Broadcasting on...")]
    [SerializeField] public VoidEventChannelSO _updateHealthUI;
    [SerializeField] public BossEventChannelSO _updateBossUI;
    [SerializeField] public VoidEventChannelSO _deathEvent;

    [Header("Listening to...")]
    [SerializeField] private VoidEventChannelSO _onSceneReady;
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
        if (_updateBossUI != null && _bossHealthUIItem != null)
        {
            _bossHealthUIItem._healthSO = ScriptableObject.CreateInstance<HealthSO>();
            _bossHealthUIItem._voidEvent = ScriptableObject.CreateInstance<VoidEventChannelSO>();

            _bossHealthUIItem._healthSO.SetMaxHealth(_healthConfigSO.InitialHealth);
            _bossHealthUIItem._healthSO.SetCurrentHealth(_healthConfigSO.InitialHealth);

            _updateBossUI.RaiseEvent(_bossHealthUIItem);
        } 


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
