using UnityEngine;

public class RoomExit : MonoBehaviour
{
    private Collider2D _collider;

    [Header("Room Exits Direction")]
    [SerializeField] private Direction _exitDirection;

    //[Header("Broadcasting on...")]
    //[SerializeField] private VoidEventChannelSO _onRoomExit;
    //[SerializeField] private VoidEventChannelSO _onRoomEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //_onRoomExit.RaiseEvent();
            //_collider = other;
            //wait for fade reach top only teleport to new room
            other.transform.position += TeleportDirection(_exitDirection);
        }
    }

    private void Teleport()
    {
        _collider.transform.position += TeleportDirection(_exitDirection);
        _collider = null;
    }

    private Vector3 TeleportDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Top: return new Vector3(0, 5, 0);
            case Direction.Left: return new Vector3(-5, 0, 0);
            case Direction.Right: return new Vector3(5, 0, 0);
            case Direction.Bottom: return new Vector3(0, -5, 0);
            default: return Vector3.zero;
        }
    }
}
