using UnityEngine;
using System.Collections;
public class Damageable : MonoBehaviour
{
    [SerializeField] private HealthSO _currentHealthSO;

    [Header("Broadcasting on...")]
    [SerializeField] private VoidEventChannelSO _updateHealthUI = default;

    [Header("Listening to...")]
    [SerializeField] private IntEventChannelSO _restoreHealth;

    public bool GetHit { get; set; }
    public bool IsDead { get; set; }

    private void Awake()
    {
        if (_updateHealthUI != null)
        { _updateHealthUI.RaiseEvent(); }
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
        { return; }

        _currentHealthSO.RestoreHealth(amount);
    }

    public void RecieveAttack(int damage)
    {
        if (IsDead || GetHit)
        { return; }

        _currentHealthSO.InflictDamage(damage);

        if (_updateHealthUI != null)
        { _updateHealthUI.RaiseEvent(); }

        StartCoroutine(AttackRecieveCD());

        if (_currentHealthSO.CurrentHealth <= 0)
        { 
            IsDead = true;
            Death();
        }
    }

    public void Revive()
    {
        _currentHealthSO.SetCurrentHealth(9);

        if (_updateHealthUI != null)
        { _updateHealthUI.RaiseEvent(); }

        IsDead = false;
    }

    public void Death()
    { }

    private IEnumerator AttackRecieveCD()
    {
        GetHit = true;

        yield return new WaitForSeconds(0.5f);
        GetHit = false;
    }

}
