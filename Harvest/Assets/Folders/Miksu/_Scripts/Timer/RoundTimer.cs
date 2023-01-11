using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTimer : MonoBehaviour
{
    #region Properties
    [Header("References")]
    [SerializeField] GameObject handle_Left;
    [SerializeField] GameObject handle_Right;
    [Space]
    [Tooltip("The angle the Handles start from at the beginning of the round")]
    [SerializeField] float startOffAngle = 90f;
    float currentAngle;
    [SerializeField]
    float clockUpdateFrequency = 0.1f;

    [Header("Round Timer")]
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
    }

    private void MoveHandles(float percentage)
    {
        currentAngle = startOffAngle * percentage;

        // Move Handles
        handle_Left.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, currentAngle));
        handle_Right.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -currentAngle));
    }
    #endregion

    #region Round Timing
    IEnumerator RoundCountdown()
    {
        // Get the right values



        while (roundTimeLeft > 0f)
        {
            // Reduce the roundtime
            roundTimeLeft -= clockUpdateFrequency;

            // Move the Moon and Start
            MoveHandles(GetPercentageLeft(roundTimeLeft, roundTime));

            yield return new WaitForSeconds(clockUpdateFrequency);
        }
    }

    private float GetPercentageLeft(float left, float original)
    {
        float percentage = left / original;

        return percentage;
    }
    #endregion
}
