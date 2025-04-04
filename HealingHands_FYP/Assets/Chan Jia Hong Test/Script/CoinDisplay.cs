using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    private Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string[] temp = text.text.Split("$");
        text.text = temp[0] + "$" + CoinManager.coins;
    }
}
