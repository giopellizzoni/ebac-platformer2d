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

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    private float _currentSpeed;
    private float _fallingThreshold = -5.0f;
    private bool _isFalling = false;

    private Animator _currentPlayer;

    [Header("Jump Collision Check")]
    public Collider2D collider2d;
    public float distToGround;
    public float spaceToGround = .1f;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);
        _currentPlayer.GetComponentInChildren<GunBase>().playerSideReference = transform;
        _currentPlayer.GetComponent<PlayerDestroyHelper>().player = this;

        if (collider2d != null)
        {
            distToGround = collider2d.bounds.extents.y;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }


    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        HandleJump();
        HandleMovement();
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
            myRigidBody.velocity += soPlayerSetup.friction;
        else if (myRigidBody.velocity.x < 0)
            myRigidBody.velocity -= soPlayerSetup.friction;
    }

    private void SetMovementDirection()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
            FlipCharacter(-1f, soPlayerSetup.playerSwipeDuration);
            HandleAnimation(PlayerAnimationState.IsRunning, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            FlipCharacter(1f, soPlayerSetup.playerSwipeDuration);
            HandleAnimation(PlayerAnimationState.IsRunning, true);
        }
        else
        {
            HandleAnimation(PlayerAnimationState.IsRunning, false);
        }
    }

    private void CheckMovenmentSpeed()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentPlayer.speed = 2;
            _currentSpeed = soPlayerSetup.speedRun;
        }
        else
        {
            _currentPlayer.speed = 1;
            _currentSpeed = soPlayerSetup.speed;
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
        HandleAnimation(PlayerAnimationState.JumpUp, false);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            myRigidBody.velocity = Vector2.up * soPlayerSetup.forceJump;
            myRigidBody.transform.localScale = Vector2.one;
            DOTween.Kill(myRigidBody.transform);
            HandleScaleJump();

            HandleAnimation(PlayerAnimationState.JumpUp, true);
            PlayJumpVFX();
        }
    }

    private void PlayJumpVFX()
    {
        VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.Jump, transform.position);
    }

    private void HandleScaleJump()
    {
        myRigidBody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigidBody.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    private void CheckIfIsFalling()
    {
        _isFalling = myRigidBody.velocity.y < _fallingThreshold;
        if (_isFalling)
        {
            HandleAnimation(PlayerAnimationState.JumpUp, false);
        }
    }

    private void HandleAnimation(PlayerAnimationState state, bool flag)
    {
        _currentPlayer.SetBool(state.ToString(), flag);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}