using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3[] _cameraPos;


    private void Awake()
    {
        _cameraPos[0] = new Vector3(0, 0, -10);
        _cameraPos[1] = new Vector3(0, 10, -10); 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_camera.transform.position == _cameraPos[0])
            {
                _camera.transform.position = _cameraPos[1];
            }
            else
            {
                _camera.transform.position = _cameraPos[0];
            }
        }
    }
}
