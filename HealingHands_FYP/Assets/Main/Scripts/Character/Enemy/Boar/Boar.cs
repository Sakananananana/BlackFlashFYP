using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Boar : AnimationController
{
    private GameObject _player;
    private float _angle;
    private SpriteRenderer _spriteRenderer;

    protected override void Awake()
    {
        base.Awake();

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Flip();
        //SetAnimationFloat();
    }

    private void Flip()
    {
        if (_direction.x > 0)
        { _spriteRenderer.flipX = true; }

        if (_direction.x < 0)
        { _spriteRenderer.flipX = false; }
    }

    public void SetAnimationFloat()
    {
        _direction = (_player.transform.position - transform.position).normalized;
        //If _direction's Vec2 is equal to zero this will not be called!
        if (_direction != Vector2.zero)
        {
            if (45 >= _angle && _angle >= -45)
            { _dirUp = 1; }
            else { _dirUp = 0; }

            if (-135 >= _angle && _angle >= -180 || 135 <= _angle && _angle <= 180)
            { _dirDown = 1; }
            else { _dirDown = 0; }

            if (135 >= _angle && _angle >= 45)
            { _dirRight = 1; }
            else { _dirRight = 0; }

            if (-135 <= _angle && _angle <= -45)
            { _dirLeft = 1; }
            else { _dirLeft = 0; }

            _anim.SetFloat("Up", _dirUp);
            _anim.SetFloat("Down", _dirDown);
            _anim.SetFloat("Left", _dirLeft);
            _anim.SetFloat("Right", _dirRight);
        }
    }
}
