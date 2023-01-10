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
    private float brakeDrag = 8f;
    [SerializeField]
    private float moveDrag = 0.5f;
    [SerializeField]
    [Range(0.05f, 0.3f)]
    private float brakeAfterTime = 0.05f;
    private float timeSinceLastMoveInput = 0f;


    #endregion


    #region SET-UP
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        graphics = GetComponentInChildren<PlayerSpriteHolder>();

        StartCoroutine(MoveInputChecker());
    }
    #endregion

    // Called from PlayerInputs
    public void MovePlayer(Vector3 moveInput)
    {
        HandleMovement(moveInput);

        // Set the time for MoveInputChecker coroutine
        timeSinceLastMoveInput = 0f;
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

    #region DRAG
    // Makes the stopping snappier, less slidier

    IEnumerator MoveInputChecker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);

            // If there has been enough time from last move input
            if (timeSinceLastMoveInput < brakeAfterTime)
            {
                // Set Free Drag
                rb.drag = moveDrag;

                // Increment timer
                timeSinceLastMoveInput += Time.fixedDeltaTime;
            }
            else // If TOO MUCH time has passed
            {
                // Set heavy drag
                rb.drag = brakeDrag;
            }
        }
    }

    #endregion
}
