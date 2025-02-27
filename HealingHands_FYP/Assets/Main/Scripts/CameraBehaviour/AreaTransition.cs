using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

public class AreaTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _mapBoundary; //remove later
    [SerializeField] private BoxCollider2D _mapBoundaryBox;

    [Header("Broadcasting On...")]
    [SerializeField] private ColliderEventChannelSO _onNewRoomEntered;
    public Vector3 movePlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            _onNewRoomEntered.RaiseEvent(_mapBoundaryBox);
            other.transform.position += movePlayer;
        }
    }
}
