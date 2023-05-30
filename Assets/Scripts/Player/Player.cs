using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;

    [Header("Speed setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2;

    [Header("Animation setup")]
    public float jumpScaleY = .75f;
    public float jumpScaleX = .35f;
    public float landingScaleX = 0.75f;
    public float landingScaleY = .35f;
    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;
    public int numberOfLoops = 2;
    public LoopType loopType = LoopType.Yoyo;



    private float _currentSpeed;
    private float _fallingThreshold = -5.0f;
    private bool _isRunning = false;
    private bool _isFalling = false;


    // Update is called once per frame
    void Update()
    {
        HandleJump();
        HandleMovement();
        CheckIfIsFalling();
    }

    private void HandleMovement()
    {
        CheckMovenmentSpeed();
        SetMovementDirection();
        AdjustFrictionForDeceleration();

    }

    private void AdjustFrictionForDeceleration()
    {
        if (myRigidBody.velocity.x > 0)
            myRigidBody.velocity -= friction;
        else if (myRigidBody.velocity.x < 0)
            myRigidBody.velocity += friction;
    }

    private void SetMovementDirection()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
        else if (Input.GetKey(KeyCode.RightArrow))
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
    }

    private void CheckMovenmentSpeed()
    {
        _currentSpeed = Input.GetKeyDown(KeyCode.LeftControl) ? speedRun : speed;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * forceJump;
            myRigidBody.transform.localScale = new Vector2(0.5f, 0.5f);
            DOTween.Kill(myRigidBody.transform);
            HandleScaleJump();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _isFalling)
            HandleLanding();
    }


    private void HandleScaleJump()
    {
        HandleAnimation(jumpScaleX, jumpScaleY, animationDuration);
    }

    private void HandleLanding()
    {
        HandleAnimation(landingScaleX, landingScaleY, animationDuration / 2);
    }

    private void HandleAnimation(float valueX, float valueY, float duration)
    {
        myRigidBody.transform.DOScaleY(valueY, duration).SetLoops(numberOfLoops, loopType);
        myRigidBody.transform.DOScaleX(valueX, duration).SetLoops(numberOfLoops, loopType);
    }


    private void CheckIfIsFalling()
    {
        _isFalling = myRigidBody.velocity.y < _fallingThreshold;
    }
}
