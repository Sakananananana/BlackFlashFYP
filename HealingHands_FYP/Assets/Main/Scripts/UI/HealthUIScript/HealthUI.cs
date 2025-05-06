using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthSO _healthSO;
    [SerializeField] private Slider _slider;

    [SerializeField] private VoidEventChannelSO _healthChanges;
    [SerializeField] private GameObject _healthIcon;

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
        SetHealthIcon();
    }

    private void SetHealthIcon()
    {
        if (_healthIcon != null) 
        {
            if (0.5f > ((float)_healthSO.CurrentHealth / (float)_healthSO.MaxHealth))
            {
                _healthIcon.SetActive(true);
            }
            else
            {
                _healthIcon.SetActive(false);
            }
        }
    }
}
