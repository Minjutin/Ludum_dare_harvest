using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        DisablePlayers();
    }

    // Disable unused gods in the start
    public void DisablePlayers()
    {

        if(Statics.p1God!=Enums.God.God1 && Statics.p2God != Enums.God.God1)
        {
            GameObject.Find("God1").SetActive(false);
            GameObject.Find("P1").SetActive(false);
        }

        if (Statics.p1God != Enums.God.God2 && Statics.p2God != Enums.God.God2)
        {
            GameObject.Find("God2").SetActive(false);
            GameObject.Find("P2").SetActive(false);
        }

        if (Statics.p1God != Enums.God.God3 && Statics.p2God != Enums.God.God3)
        {
            GameObject.Find("God3").SetActive(false);
            GameObject.Find("P3").SetActive(false);
        }

        if (Statics.p1God != Enums.God.God4 && Statics.p2God != Enums.God.God4)
        {
            GameObject.Find("God4").SetActive(false);
            GameObject.Find("P4").SetActive(false);
        }
    }
}
