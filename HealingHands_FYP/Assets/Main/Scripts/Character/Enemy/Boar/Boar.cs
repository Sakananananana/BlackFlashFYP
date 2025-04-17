using System;
using UnityEngine;

public class Boar : AnimationController
{
    private float _angle;
    private SpriteRenderer _spriteRenderer;

    protected override void Awake()
    {
        base.Awake();

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Flip();
    }

    private void Flip()
    {
        if (_direction.x > 0)
        { _spriteRenderer.flipX = true; }

        if (_direction.x < 0)
        { _spriteRenderer.flipX = false; }
    }

    private void SetAnimationFloat()
    {
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
