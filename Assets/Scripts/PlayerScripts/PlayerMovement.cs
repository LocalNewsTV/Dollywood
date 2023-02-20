using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController cc;

    private float speed = 9.0f;         // XZ movement speed
    private float rotationSpeed = 720f; // rotation sensitivity

    private float gravity = -9.81f;     // default gravity (this will change)
    private float yVelocity = 0f;       // current y Velocity
    private float yVelocityWhenGrounded = -4f;  // this ensures cc.isGrounded will work 

    private float jumpHeight = 3.0f;    // the height of our jump in units
    private float jumpTime = 0.5f;      // the time of our jump in seconds
    private float initialJumpVelocity;  // upward velocity for jumping (precalculated)

    private void Start()
    {
        // calculate gravity & initial jump velocity required for our jump
        float timeToApex = jumpTime / 2.0f;
        gravity = (-2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    void Update()
    {
        // determine XZ movement vector
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizInput, 0, vertInput);

        // ensure diagonal movement doesn't exceed horiz/vert movement speed
        movement = Vector3.ClampMagnitude(movement, 1.0f);

        // convert from local to global coordinates
        movement = transform.TransformDirection(movement);
        movement *= speed;

        // calculate yVelocity and add it to the player's movement vector
        yVelocity += gravity * Time.deltaTime;

        // if we are on the ground and we were falling
        if( cc.isGrounded && yVelocity < 0.0)
        {
            yVelocity = yVelocityWhenGrounded;
        }
        // give upward y Velocity if we jumped
        if(Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            yVelocity = initialJumpVelocity;
        }
        movement.y = yVelocity;

        movement *= Time.deltaTime; // make all movement processor independent
        cc.Move(movement);  // move the player (using the character controller)

        // rotate the player
        Vector3 rotation = Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
        transform.Rotate(rotation);
    }
}
