using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _bossTitle;
    [SerializeField] private Slider _bossHPBar;

    [SerializeField] private BossEventChannelSO _bossUIRequest;
    private VoidEventChannelSO _healthChanges;
    private HealthSO _bossHealthSO;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _bossUIRequest.OnEventRaised += SetBossHealthUI;
    }

    private void OnDisable()
    {
        _bossUIRequest.OnEventRaised -= SetBossHealthUI;
        _healthChanges.OnEventRaised -= SetHealth;
    }

    private void SetBossHealthUI(BossHealthUIItem boss)
    { 
        _bossTitle.text = boss.BossTitle;
        _healthChanges = boss._voidEvent;
        _bossHealthSO = boss._healthSO;

        _healthChanges.OnEventRaised += SetHealth;

        _bossHPBar.maxValue = _bossHealthSO.MaxHealth;
        SetHealth();
        _bossHPBar.gameObject.SetActive(true);
    }

    private void SetHealth()
    {
        _bossHPBar.value = _bossHealthSO.CurrentHealth;

        if (_bossHealthSO.CurrentHealth <= 0)
            _bossHPBar.gameObject.SetActive(false);
    }

}
