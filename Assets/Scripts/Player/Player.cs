using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public enum PlayerAnimationState
{
    Idle,
    JumpUp,
    JumpDown,
    Landing
}

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

    [Header("Animation Player")]
    public string boolRun = "IsRunning";
    public Animator animator;

    private float _currentSpeed;
    private float _fallingThreshold = -5.0f;
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
        {
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
            FlipCharacter(-.5f, .1f);
            animator.SetBool(boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            FlipCharacter(.5f, .1f);
            animator.SetBool(boolRun, true);
        }
        else
        {
            animator.SetBool(boolRun, false);
        }
    }

    private void CheckMovenmentSpeed()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.speed = 2;
            _currentSpeed = speedRun;
        } else {
            animator.speed = 1;
            _currentSpeed = speed;
        }
         
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * forceJump;
            myRigidBody.transform.localScale = new Vector2(0.5f, 0.5f);
            DOTween.Kill(myRigidBody.transform);
            HandleAnimation(PlayerAnimationState.JumpUp);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _isFalling)
        {
            HandleAnimation(PlayerAnimationState.Landing);
            HandleAnimation(PlayerAnimationState.Idle);

        }
    }

    private void HandleAnimation(PlayerAnimationState animationState)
    {
        var state = Enum.GetName(typeof(PlayerAnimationState), animationState);
        animator.SetTrigger(state);
    }

    private void FlipCharacter(float direction, float duration) 
    {
        if (myRigidBody.transform.localScale.x != direction)
        {
            myRigidBody.transform.DOScaleX(direction, duration);
        }
    }

    private void CheckIfIsFalling()
    {
        _isFalling = myRigidBody.velocity.y < _fallingThreshold;
        if (_isFalling)
        {
            HandleAnimation(PlayerAnimationState.JumpDown);
        }
    }
}
