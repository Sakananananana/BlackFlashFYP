using UnityEngine;

public class BuynSale : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buy()
    {
        CoinManager.coins -= 50;
        CoinManager.UpdateCoins();
    }
    public void Sale()
    {
        CoinManager.coins += 50;
        CoinManager.UpdateCoins();
    }
}
