using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Toad _toad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _toad.enemyHealthChange += SetUIHealth;
        _slider.value = 20;
    }

    private void OnDisable()
    {
        _toad.enemyHealthChange -= SetUIHealth;
    }

    private void SetUIHealth(float value)
    {
        _slider.value -= value;
    }
}
