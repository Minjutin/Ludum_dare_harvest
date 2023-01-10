using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    #region PROPERTIES
    Rigidbody rb;
    [HideInInspector]
    public PlayerSpriteHolder graphics;


    public float playerMoveSpeed = 5f;
    [SerializeField]
    private float maxMoveSpeed = 5f;


    [Header("Braking")]
    [SerializeField]
    private float brakeSpeed = 4f;
    [SerializeField]
    private float brakeDrag = 3f;

    #endregion


    #region SET-UP
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        graphics = GetComponentInChildren<PlayerSpriteHolder>();
    }
    #endregion

    private void FixedUpdate()
    {
        SetProperDrag();
    }

    // Called from PlayerInputs
    public void MovePlayer(Vector3 moveInput)
    {
        HandleMovement(moveInput);
    }


    #region MOVING

    private void HandleMovement(Vector3 direction)
    {
        // Move the Player
        AddMovementForce(direction, 1f);

        // Turn towards the direction
        //FaceMovementDirection(direction, dot);
    }

    private void AddMovementForce(Vector3 direction, float dot)
    {
        // DOT MODIFIER
        // -> Move faster when going the direction Player is facing
        // --> Less moving when turning
        float finalSpeed = playerMoveSpeed * Mathf.Max(0.4f, dot * dot);

        // Add Force to Player RB
        rb.AddForce(direction * finalSpeed, ForceMode.Force);

        //Reduce the velocity, if it is over the speed limit
        if (rb.velocity.magnitude >= maxMoveSpeed)
        {
            // Clamp Velocity's Magnitude to maxMoveSpeed
            rb.velocity = rb.velocity.normalized;
            rb.velocity = rb.velocity * maxMoveSpeed;
        }
    }
    #endregion




    #region TURNING
    // Turn towards the movement direction
    private void FaceMovementDirection(Vector3 direction)
    {
        // TODO

        // Change Graphics
    }
    #endregion

    // Makes the stopping snappier, less slidier
    private void SetProperDrag()
    {
        // Check if the speed is under the brake limit
        if (rb.velocity.magnitude <= brakeSpeed)
        {
            // Add Drag
            //Debug.Log("Braking");
            rb.drag = brakeDrag;
        }
        else
        {
            // Normal drag
            rb.drag = 0.1f;
            //Debug.Log("Speed: " + rb.velocity.magnitude);
        }
    }

    // TODO: IEnumerator for checking when
    //       Player ceases the movement input
    //       -> Put Drag up after a set time
}
