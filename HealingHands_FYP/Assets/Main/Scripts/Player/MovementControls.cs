using PlayerInputSystem;
using System.Xml.Serialization;
using UnityEngine;

public class MovementControls : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;
    private Rigidbody2D _rBody;
    private Vector2 _moveDir;

    //Will be moving to stats later
    public float MoveSpeed;
    public float DashForce;


    private void OnEnable()
    {
        _inputReader.MoveEvent += MovementHandler;
        _inputReader.DashEvent += DashHandler;
        _inputReader.AttackEvent += AttackHandler;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= MovementHandler;
        _inputReader.DashEvent -= DashHandler;
        _inputReader.AttackEvent -= AttackHandler;
    }

    private void Awake()
    {
        _rBody= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
    }

    private void MovementHandler(Vector2 inputVector)
    { 
        _moveDir = inputVector;
        //_moveDir = new Vector3 (inputVector.x, inputVector.y, 0);
    }

    private void DashHandler()
    { 
        
    }

    private void AttackHandler()
    { 
    
    }

    private void Movement()
    {
        _rBody.velocity = _moveDir * MoveSpeed;
    }
}
