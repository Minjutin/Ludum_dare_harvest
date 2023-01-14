using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statics
{
    public static Enums.God p1God = Enums.God.God1;
    public static Enums.God p2God = Enums.God.God4;

    //Key Codes Player 1
    public static KeyCode p1up = KeyCode.W;
    public static KeyCode p1down = KeyCode.S;
    public static KeyCode p1left = KeyCode.A;
    public static KeyCode p1right = KeyCode.D;
    public static KeyCode p1inventory = KeyCode.Q;
    public static KeyCode p1interaction = KeyCode.E;

    //Key Codes Player 2
    public static KeyCode p2up = KeyCode.UpArrow;
    public static KeyCode p2down = KeyCode.DownArrow;
    public static KeyCode p2left = KeyCode.LeftArrow;
    public static KeyCode p2right = KeyCode.RightArrow;
    public static KeyCode p2inventory = KeyCode.RightShift;
    public static KeyCode p2interaction = KeyCode.RightControl;
}
