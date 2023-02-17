using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    [SerializeField] CharacterController charCont;
    private float gravity = -9.8f;
    private float yVelocity = 0.0f;
    private float groundedYVelocity = -4.0f;
    private float jumpHeight = 0.35f;
    private float jumpTime = 0.5f;
    private float initialJumpVelocity;


    private float speed = 9.0f;
    private float numJumps;
    private float jumpMax = 2;
    // Start is called before the first frame update
    void Start()
    {
        numJumps = jumpMax;
        float timeToApex = jumpTime / 2.0f;
        gravity = (-2 * jumpHeight) / Mathf.Pow(timeToApex, 2);

        initialJumpVelocity = (2 * jumpHeight) / timeToApex;
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horiz,0.0f, vert);
        yVelocity += gravity * Time.deltaTime;
        if(charCont.isGrounded && yVelocity < 0f)
        {
            numJumps = jumpMax;
            yVelocity = groundedYVelocity;
        }
        if(Input.GetButtonDown("Jump") && numJumps > 0)
        {
            yVelocity = initialJumpVelocity;
            numJumps -= 1;
        }
        //Limit Diagonal movement
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        //Apply Gravity
        movement.y = yVelocity;
        //Take speed into account
        movement *= speed;
        //Make processor independent
        movement *= Time.deltaTime;
        //Convert local to global Coordinates
        movement = transform.TransformDirection(movement);

        charCont.Move(movement);
        /*transform.Translate(movement * speed * Time.deltaTime, Space.Self);*/
    }
}
