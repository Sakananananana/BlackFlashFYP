using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public const string Coins = "Coins";
    public static int coins = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coins=PlayerPrefs.GetInt(Coins);
    }

    public static void UpdateCoins()
    {
        PlayerPrefs.SetInt("Coins", coins);
        coins=PlayerPrefs.GetInt(Coins);
        PlayerPrefs.Save();
    }
    
}
