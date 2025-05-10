using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private GameObject _fillArea;
    [SerializeField] private TMP_Text _bossTitle;

    [SerializeField] private Slider _bossHPBar;

    [SerializeField] private VoidEventChannelSO _protagonistDeathEvent;
    [SerializeField] private VoidEventChannelSO _bossHealthUIUpdate;
    [SerializeField] private BossEventChannelSO _bossUIRequest;
    private HealthSO _currentBossHealthSO;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _protagonistDeathEvent.OnEventRaised += HideBossHealthUI;
        _bossUIRequest.OnEventRaised += SetBossHealthUI;
        _bossHealthUIUpdate.OnEventRaised += SetHealth;
    }

    private void OnDisable()
    {
        _protagonistDeathEvent.OnEventRaised -= HideBossHealthUI;
        _bossUIRequest.OnEventRaised -= SetBossHealthUI;
        _bossHealthUIUpdate.OnEventRaised -= SetHealth;
    }

    private void SetBossHealthUI(BossHealthUIItem boss)
    {
        _fillArea.SetActive(true);
        _bossTitle.gameObject.SetActive(true);

        _bossTitle.text = boss.BossTitle;
        _currentBossHealthSO = boss._healthSO;

        _bossHPBar.maxValue = _currentBossHealthSO.MaxHealth;
        SetHealth();
    }

    private void SetHealth()
    {
        _bossHPBar.value = _currentBossHealthSO.CurrentHealth;

        if (_currentBossHealthSO.CurrentHealth <= 0)
        {
            _fillArea.SetActive(false);
            _bossTitle.gameObject.SetActive(false);
        }
    }

    private void HideBossHealthUI()
    {
        _fillArea.SetActive(false);
        _bossTitle.gameObject.SetActive(false);
    }
}
