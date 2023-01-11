using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains all the stats like speed and points.


public class PlayerStats : MonoBehaviour
{

    // -------------------------- IMPORTANT VARIABLES --------------------------

    [Header("Current speed.")]
    [SerializeField] float speed;

    [Header("Current points.")]
    [SerializeField] int points;

    [Header("Wanted plant")]
    [SerializeField] Enums.FruitType wantedFruit;

    public int playerNumber;
    PlayerSpriteHolder playerSprite;

    public bool isWet;

    public Enums.God god;

    public GameObject UIelements;

    //Initializing 

    Constants constants;

    private void Start()
    {
        constants = FindObjectOfType<Constants>(); //Find constants
        endSprites = Resources.LoadAll("EndScenes", typeof(Sprite));
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

        if (!playerSprite) { Debug.LogWarning("Can't find PlayerSpriteHolder for " + gameObject.name); }

        //Set things
        playerSprite.SetPlayerSprites(god);
        switch (god) {
            case Enums.God.God1:
                UIelements = GameObject.Find("P1");
                break;
            case Enums.God.God2:
                UIelements = GameObject.Find("P2");
                break;
            case Enums.God.God3:
                UIelements = GameObject.Find("P3");
                break;
            case Enums.God.God4:
                UIelements = GameObject.Find("P4");
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

    //----------------------- POINTS ----------------------------------------

    public void GiveItem(InventoryItem item)
    {
        //If player gives god a fruit.
        if(item is Fruit)
        {
            Fruit fruit = item as Fruit;

            //Check the points you get from this fruit. ONLY ONE KIND OF POINTS CAN BE GIVEN.

            //Fruit is wanted.
            if (fruit.Type == wantedFruit) 
            {
                points += constants.wantedPoints;
                
            }

            //Fruits is god's favorite type
            else if (fruit.Type == Enums.FruitType.Fruit1 && god == Enums.God.God1
                || fruit.Type == Enums.FruitType.Fruit2 && god == Enums.God.God2
                || fruit.Type == Enums.FruitType.Fruit3 && god == Enums.God.God3
                || fruit.Type == Enums.FruitType.Fruit4 && god == Enums.God.God4
            )
            {
                points += constants.favoritePoints;
            }

            //Otherwise
            else
            {
                points += constants.fruitPoints;
            }
        }
        
        //If player gives god a seed.
        if(item is Seed)
        {
            points += constants.seedPoints;
        }

        //If player gives god poop.
        if(item is Manure)
        {
            points += constants.manurePoints;
        }

        //Make points so it's better
        if (points > constants.winPoints)
            points = constants.winPoints;
        if (points < 0)
            points = 0;
        //Let's display the pointsss.
        RectTransform rt = UIelements.transform.Find("Fill").GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2((300*points)/constants.winPoints, rt.sizeDelta.y);

        CheckIfWon();
    } //Give item to the God.

    Object[] endSprites;

    public void CheckIfWon()
    {
        if (points >= constants.winPoints)
        {
            Sprite spriteNow;

            switch (god)
            {
                case Enums.God.God1:
                    spriteNow = endSprites[0] as Sprite;  
                    break;
                case Enums.God.God2:
                    spriteNow = endSprites[1] as Sprite;
                    break;
                case Enums.God.God3:
                    spriteNow = endSprites[2] as Sprite;
                    break;
                case Enums.God.God4:
                    spriteNow = endSprites[3] as Sprite;
                    break;
                default:
                    spriteNow = endSprites[0] as Sprite;
                    Debug.LogError("There is no god");
                    break;
            }

            UIelements.transform.parent.parent.GetComponent<EndGame>().OpenEnding(spriteNow);
        }
    }

    //----------------------- POINTS ----------------------------------------


}
