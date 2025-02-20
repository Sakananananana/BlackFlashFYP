using UnityEngine;
using UnityEngine.UI;

public class HealthPointUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private FloatEventChannelSO _onProtagonistHealthChange;

    void OnEnable()
    {
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
}
