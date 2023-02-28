using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;

    public Vector2 friction = new Vector2(.1f, 0);

    public float speed;
    public float speedRun;
    public float forceJump = 2;

    private float _currentSpeed;

    // Update is called once per frame
    void Update()
    {
        HandleJump();
        HandleMovement();
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
            //myRigidBody.MovePosition(myRigidBody.position - velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position + velocity * Time.deltaTime);
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
            myRigidBody.velocity = Vector2.up * forceJump;
    }
}
