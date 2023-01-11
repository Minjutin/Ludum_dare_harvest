using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTimer : MonoBehaviour
{
    #region Properties
    [Header("References")]
    [SerializeField] GameObject handle_Left;
    [SerializeField] GameObject handle_Right;
    [SerializeField] GameObject leftIndicator;
    [SerializeField] GameObject rightIndicator;
    [Tooltip("Visualizes the Path the Star & Moon take. Use with SHORT round times.")]
    [SerializeField] bool debugDrawPath = false;
    [Space]
    [Header("Handles")]
    [Tooltip("The angle the Handles start from at the beginning of the round")]
    [SerializeField] float startOffAngle = 90f;
    float currentAngle;
    [Tooltip("How often the clock updates")]
    [SerializeField]
    float clockUpdateFrequency = 0.1f;
    [Header("Handle Length/Radius")]
    [SerializeField] float startRadius = 24f;
    [SerializeField] float endRadius = 24f;
    float currentRadius;

    [Header("Round Timer")]
    [Tooltip("Round time in seconds.")]
    [SerializeField] float roundTime = 180f;
    float roundTimeLeft;
    #endregion

    #region Setup
    private void Awake()
    {
        // Set the correct amount of time
        roundTimeLeft = roundTime;

        SetupHandles();

        // Start counting down
        StartCoroutine(RoundCountdown());
    }
    #endregion


    #region Handles
    private void SetupHandles()
    {
        // Set them to the start angles/positions
        handle_Left.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, startOffAngle));
        handle_Right.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -startOffAngle));

        currentAngle = startOffAngle;
        currentRadius = startRadius;
    }

    private void MoveHandles(float percentage)
    {
        // Update current angle
        currentAngle = startOffAngle * percentage;

        // Move Handles
        handle_Left.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
        handle_Right.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -currentAngle));
    }

    private void AdjustHandleLength(float percentage)
    {
        // Update current Radius
        float invertedPercentage = 1f - percentage;
        float difference = startRadius - endRadius;
        currentRadius = startRadius - (difference * invertedPercentage);


        // Adjust Handle Indicator Lengths (distance from the axle)
        leftIndicator.transform.localPosition = new Vector3(0f, currentRadius);
        rightIndicator.transform.localPosition = new Vector3(0f, currentRadius);
    }
    #endregion

    #region Round Timing
    IEnumerator RoundCountdown()
    {
        // The clock runs until the time has run out
        while (roundTimeLeft > 0f)
        {
            // Reduce the roundtime
            roundTimeLeft -= clockUpdateFrequency;

            // Move the Moon and Start
            float percentageLeft = GetPercentageLeft(roundTimeLeft, roundTime);
            MoveHandles(percentageLeft);
            AdjustHandleLength(percentageLeft);


            // Debug Draw Handles for better visualization
            if (debugDrawPath) { DebugDrawPath(percentageLeft); }

            yield return new WaitForSeconds(clockUpdateFrequency);
        }

        // Time has run out, end round
        EndRound();
    }

    private float GetPercentageLeft(float left, float original)
    {
        float percentage = left / original;

        return percentage;
    }
    #endregion

    #region Round ENDING
    private void EndRound()
    {
        Debug.Log("Round has ended.");

        // TODO: Functionality
    }
    #endregion

    private void DebugDrawPath(float percentage)
    {
        // Don't draw it every frame
        // --> Draw if every 5 percentage of the way
        float testValue = Mathf.RoundToInt(percentage * 100f);

        if (testValue % 10 == 0)
        {
            Instantiate(rightIndicator, rightIndicator.transform.position, rightIndicator.transform.rotation);
        }
    }
}
