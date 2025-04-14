using UnityEngine;
using UnityEngine.UI;

public class HealthPointUI : MonoBehaviour
{
    [SerializeField] private HealthSO _healthSO;
    [SerializeField] private Slider _slider;

    [SerializeField] private VoidEventChannelSO _healthChanges;


    void Awake()
    {
        if (_healthSO == null)
        {
            _healthSO = GetComponentInParent<Damageable>()._currentHealthSO;
            _healthChanges = GetComponentInParent<Damageable>()._updateHealthUI;
        }
    }

    void OnEnable()
    {
        _healthChanges.OnEventRaised += SetHealth;

        _slider.maxValue = _healthSO.MaxHealth;
        SetHealth();
    }

    private void OnDisable()
    {
        _healthChanges.OnEventRaised -= SetHealth;
    }

    private void SetHealth()
    {
        _slider.value = _healthSO.CurrentHealth;

        if (_healthSO.CurrentHealth <= 0)
            gameObject.SetActive(false);
    }
}
