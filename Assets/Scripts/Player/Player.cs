using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Runtime.CompilerServices;

public enum PlayerAnimationState
{
    JumpUp,
    JumpDown,
    IsRunning,
    Landing
}

public class Player : MonoBehaviour
{

    public HealthBase healthBase;
    public Rigidbody2D myRigidBody;

    [Header("Speed setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;


    [Header("Animation Player")]
    public string boolRun = "IsRunning";
    public string triggerDeath = "Death";
    

    public Animator animator;

    private float _currentSpeed;
    private float _fallingThreshold = -5.0f;
    private bool _isFalling = false;


    

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        animator.SetTrigger(triggerDeath);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
        CheckIfIsFalling();
    }

    #region Movement
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
            FlipCharacter(-1f, .1f);
            HandleAnimationBool(PlayerAnimationState.IsRunning, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            FlipCharacter(1f, .1f);
            HandleAnimationBool(PlayerAnimationState.IsRunning, true);
        }
        else
        {
            HandleAnimationBool(PlayerAnimationState.IsRunning, false);
        }
    }

    private void CheckMovenmentSpeed()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.speed = 2;
            _currentSpeed = speedRun;
        }
        else
        {
            animator.speed = 1;
            _currentSpeed = speed;
        }

    }

    private void FlipCharacter(float direction, float duration)
    {
        if (myRigidBody.transform.localScale.x != direction)
        {
            myRigidBody.transform.DOScaleX(direction, duration);
        }
    }
    #endregion


    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleAnimationBool(PlayerAnimationState.JumpUp, false);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * forceJump;
            HandleAnimationBool(PlayerAnimationState.JumpUp, true);
        }
    }

    private void CheckIfIsFalling()
    {
        _isFalling = myRigidBody.velocity.y < _fallingThreshold;
        if (_isFalling)
        {
            HandleAnimationBool(PlayerAnimationState.JumpUp, false);
        }
    }

    private void HandleAnimationBool(PlayerAnimationState state, bool flag)
    {
        animator.SetBool(state.ToString(), flag);
    }


    public void DestroyMe() 
    {
        Destroy(gameObject);
    }

}