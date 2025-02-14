using UnityEngine;
using UnityEngine.UI;

public class HealthPointUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Protagonist _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _player.HealthChange += SetUIHealth;
        _slider.value = 20;
    }

    private void OnDisable()
    {
        _player.HealthChange -= SetUIHealth;
    }

    private void SetUIHealth(float value)
    { 
        _slider.value -= value;
    }
}
