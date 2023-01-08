using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Give this to the gamemanager or something so the variables can be edited.
public class Constants : MonoBehaviour
{

    [Header("Speed level speeds. Element 0=fastest, Element 3=slowest")]
    public float[] speedLevels = new float[4];


    [Header("Points")]
    public int wantedPoints;
    public int favoritePoints;
    public int fruitPoints, seedPoints, manurePoints;
}
