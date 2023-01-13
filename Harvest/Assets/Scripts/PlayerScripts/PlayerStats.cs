using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains all the stats like speed and points.


public class PlayerStats : MonoBehaviour
{

    // -------------------------- IMPORTANT VARIABLES --------------------------

    [Header("Current speed.")]
    [SerializeField] float speed;

    public int playerNumber;
    PlayerSpriteHolder playerSprite;

    public bool isWet;

    public Enums.God god;

    //Initializing 

    Constants constants;
    public PlayerPoints points;

    private void Start()
    {
        constants = FindObjectOfType<Constants>(); //Find constants
        points = this.GetComponent<PlayerPoints>();

        isWet = false;

        // Set the Correct god & graphics for Player
        switch (playerNumber)
        {
            case 1:
                god = Statics.p1God;
                playerSprite = GetComponent<PlayerInput>().playerMovement.graphics;
                break;
            case 2:
                god = Statics.p2God;
                playerSprite = GetComponent<PlayerInput>().playerMovement.graphics;
                break;
            default:
                Debug.Log("Player number " + playerNumber + " is invalid.");
                break;
        }

        points.InitializePoints(god);

        if (!playerSprite) { Debug.LogWarning("Can't find PlayerSpriteHolder for " + gameObject.name); }

        //Set things
        playerSprite.SetPlayerSprites(god);
        switch (god) {
            case Enums.God.God1:
                points.UIelements = GameObject.Find("P1");
                break;
            case Enums.God.God2:
                points.UIelements = GameObject.Find("P2");
                break;
            case Enums.God.God3:
                points.UIelements = GameObject.Find("P3");
                break;
            case Enums.God.God4:
                points.UIelements = GameObject.Find("P4");
                break;
            default:
                Debug.Log("There is no god");
                break;

        }
    }

    // Update is called once per frame


    //------------------------ SPEED -----------------------------------------
    public void ChangeSpeed(int speedLevel)
    {
        speed = constants.speedLevels[speedLevel];

        //TODO change speed in the movement script to be speed.
    } //Change speed.

}
