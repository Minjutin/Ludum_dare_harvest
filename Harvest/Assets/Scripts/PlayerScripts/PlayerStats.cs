using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains all the stats like speed and points.


public class PlayerStats : MonoBehaviour
{

    // -------------------------- IMPORTANT VARIABLES --------------------------

    [Header("Different speeds. Levels go from fastest to slowest.")]
    [SerializeField] float[] speedLevels = new float[4];

    [Header("Current speed.")]
    [SerializeField] float speed;


    [Header("Points")]
    [SerializeField] int points;


    //Initializing 

    PlayerInventory inventory;
    public void Awake()
    {
        inventory = this.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame


    //------------------------ SPEED -----------------------------------------
    public void ChangeSpeed()
    {
        int speedLevel = inventory.itemsAmount();
        speed = speedLevels[speedLevel];

        //TODO change speed in the movement script to be speed.
    } //Change speed.
}
