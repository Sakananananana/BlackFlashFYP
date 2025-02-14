using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//Need to make this Singleton, since only one controllable char
public class Protagonist : MonoBehaviour, IDamageable
{
    [SerializeField]
    private SpriteRenderer _characterSprite;
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    public float AttackDamage;

    public Action<float> HealthChange;
    public Action<Vector2> TakeDamage;
    public Action DeathEvent;
    //this should be in UI related
    //[SerializeField] private GameObject _damageScreen;
  
    public bool _canTakeDamage;
    string _lvlName;

    void Awake()
    {
        
        _currentHealth = _maxHealth; 
    }

    public void RecieveDamage(float damage, Vector3 dmgDir)
    {
        if (_canTakeDamage == true) 
        { 
            _currentHealth -= damage;
            StartCoroutine(DamageRecieveCooldown());

            HealthChange?.Invoke(damage);
            TakeDamage?.Invoke(dmgDir);

            StartCoroutine(DamageFlash());
        }

        if (_currentHealth <= 0)
        {
            Death();
            
        }
    }

    public void Death()
    {
        DeathEvent.Invoke();
        gameObject.SetActive(false);
        //_damageScreen.SetActive(false);

        Invoke("LoadScene", 1.5f);
        
    }

    private void LoadScene()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene("Tutorial");
        }
        if (SceneManager.GetActiveScene().name == "Mountain01")
        {
            SceneManager.LoadScene("Village");
        }
    }

    private IEnumerator DamageFlash()
    {
        _characterSprite.color = Color.red;
        //_damageScreen.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        _characterSprite.color = Color.white;
        //_damageScreen.SetActive(false);

    }

    private IEnumerator DamageRecieveCooldown()
    {
        _canTakeDamage= false;

        yield return new WaitForSeconds(0.5f);
        _canTakeDamage = true;
    }

}
