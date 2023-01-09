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

    public bool isWet;

    public Enums.God god;

    //Initializing 

    Constants constants;

    public void Awake()
    {
        constants = FindObjectOfType<Constants>(); //Find constants
        isWet = false;

        //Fetch god from the statics.
        switch (playerNumber)
        {
            case 1:
                god = Statics.p1God;
                break;
            case 2:
                god = Statics.p2God;
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

        Debug.Log("Points now = "+points);
    } //Give item to the God.
}
