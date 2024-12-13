using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Vector3 _pos;
    private int num = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && num == 0)
        {
            var spawnEnemy = Instantiate(_enemy, _pos, Quaternion.identity);
            num++;
        }
    }
}
