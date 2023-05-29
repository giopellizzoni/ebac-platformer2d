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
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float landingScaleX = 1.5f;
    public float landingScaleY = .7f;
    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;

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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _currentSpeed = speedRun;
        }
        else
        {
            _currentSpeed = speed;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
        }

        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity -= friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity += friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * forceJump;
            myRigidBody.transform.localScale = Vector2.one;
            DOTween.Kill(myRigidBody.transform);
            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        myRigidBody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidBody.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _isFalling) {
            HandleLanding();
        }
    }

    private void HandleLanding()
    {
        myRigidBody.transform.DOScaleY(landingScaleY, animationDuration).SetLoops(2, LoopType.Yoyo);
        myRigidBody.transform.DOScaleX(landingScaleX, animationDuration).SetLoops(2, LoopType.Yoyo);
    }

    private void CheckIfIsFalling() {
        Console.WriteLine($"{myRigidBody.velocity.y}");
        if (myRigidBody.velocity.y < _fallingThreshold)
        {
            _isFalling = true;
        }
        else {
            _isFalling = false;
        }
    }
}
