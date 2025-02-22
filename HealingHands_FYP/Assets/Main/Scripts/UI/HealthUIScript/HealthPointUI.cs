using UnityEngine;
using UnityEngine.UI;

public class HealthPointUI : MonoBehaviour
{
    [SerializeField] private HealthSO _protagonistHealth;
    [SerializeField] private Slider _slider;

    [SerializeField] private FloatEventChannelSO _onProtagonistHealthChange;
    [SerializeField] private VoidEventChannelSO _healthChanges;

    void OnEnable()
    {
        //_healthChanges.OnEventRaised += SetHealth;
        _onProtagonistHealthChange.OnEventRaised += SetUIHealth;
        _slider.value = 20;
    }

    private void OnDisable()
    {
        _onProtagonistHealthChange.OnEventRaised -= SetUIHealth;
    }

    private void SetUIHealth(float value)
    { 
        _slider.value -= value;
    }

    //private void SetHealth(float He)
    //{
    //    _slider.value = _protagonistHealth.CurrentHealth;
    //}
}
