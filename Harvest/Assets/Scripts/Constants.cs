using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Give this to the gamemanager or something so the variables can be edited.
public class Constants : MonoBehaviour
{

    [Header("Different speeds. Levels go from fastest to slowest.")]
    public float[] speedLevels = new float[4];


    [Header("Different points. Levels go from biggest to smallest")]
    public int wantedPoints,favoritePoints, fruitPoints, seedPoints, manurePoints;
}
