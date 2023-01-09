using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Give this to the gamemanager or something so the variables can be edited.
public class Constants : MonoBehaviour
{

    [Header("Speed level speeds. Element 0=fastest, Element 3=slowest")]
    public float[] speedLevels = new float[4];
    
    [Header("Points")]
    public int wantedPoints = 6;
    public int favoritePoints = 3;
    public int fruitPoints =1, seedPoints, manurePoints=-5;

    [Header("Normal plant grow speed (as seconds)")]
    public float growSpeed = 5;

    [Header("Grow time multipler")]
    public float f1Multiple = 0.5f;


    public static float cameraRotation;

    private void Awake()
    {
        cameraRotation = Camera.main.transform.eulerAngles.x;
    }

}
