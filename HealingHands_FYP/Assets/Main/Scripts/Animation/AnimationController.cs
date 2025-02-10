using UnityEngine;

public class AnimationController : MonoBehaviour
{
    //Directions
    private Vector2 _direction;

    private float _dirUp;
    private float _dirDown;
    private float _dirLeft;
    private float _dirRight;

    //Animator Reference
    private Animator _anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_direction != Vector2.zero)
        {
            if (_direction.y > 0)
            { _dirUp = _direction.y; }
            else
            { _dirUp = 0; }

            if (_direction.y < 0)
            { _dirDown = Mathf.Abs(_direction.y); }
            else
            { _dirDown = 0; }

            if (_direction.x > 0)
            { _dirRight = _direction.x; }
            else
            { _dirRight = 0; }

            if (_direction.x < 0)
            { _dirLeft = Mathf.Abs(_direction.x); }
            else
            { _dirLeft = 0; }

            _anim.SetFloat("Up", _dirUp);
            _anim.SetFloat("Down", _dirDown);
            _anim.SetFloat("Left", _dirLeft);
            _anim.SetFloat("Right", _dirRight);
        }
        else
        { }
        
    }
}
