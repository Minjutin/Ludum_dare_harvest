using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This very spagetthis code will show you what you wanna choose.

public class Choser : MonoBehaviour
{

    float movement = 5;

    GameObject p1;
    GameObject p2;

    int p1Hovering; //current god
    int p2Hovering;

    int p1chosen = -1;
    int p2chosen = -1;

    [SerializeField] GameObject[] gods = new GameObject[4];

    private void Start()
    {
        p1 = GameObject.Find("Indicator1");
        p2 = GameObject.Find("Indicator2");

        p1Hovering = 0;
        p2Hovering = 0;

        p1.transform.position = new Vector3(gods[p1Hovering].transform.position.x, p1.transform.position.y, p1.transform.position.z);

        p2.transform.position = new Vector3(gods[p1Hovering].transform.position.x, p2.transform.position.y, p2.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //----------------------------PLAYER 1 -------------------------

        //If there is no god for player 1, you can move or lock a god
        if (p1chosen == -1)
        {
            //Player1 MOVEMENT
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveP1(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveP1(1);
            }

            //Player1 GET ITEM
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (p1Hovering != p2chosen)
                {
                    p1chosen = p1Hovering;
                }

            }
        }

        //If there is god, you can remove your choice by another button.
        else if(p1chosen != -1)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
            {
                p1chosen = -1;
            }
        }


        //-----------------------------PLAYER 2 ---------------------

        //If there is no god for player 2, you can move or lock a god
        if (p2chosen == -1)
        {
            //Move player
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveP2(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveP2(1);
            }

            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                if(p2Hovering != p1chosen)
                {
                    p2chosen = p2Hovering;
                }
                
            }
        }

        //If there is god, you can remove your choice by another button.
        else if (p2chosen != -1)
        {
            if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.RightShift))
            {
                p2chosen = -1;
            }
        }


        //Start game when both god are chosen
        if (p2chosen!=-1 && p1chosen !=-1)
        {
            Statics.p1God = editGod(p1chosen);
            Statics.p2God = editGod(p2chosen);
            SceneManager.LoadScene(1);
        }

    }

    public void MoveP1(int direction)
    {
        p1Hovering = p1Hovering + direction;

        //If too many right
        if (p1Hovering > 3)
            p1Hovering = 1;

        //If too many left
        if (p1Hovering < 0)
            p1Hovering = 3;

        p1.transform.position = new Vector3(gods[p1Hovering].transform.position.x, p1.transform.position.y, p1.transform.position.z);
    }

    public void MoveP2(int direction)
    {
        p2Hovering = p2Hovering + direction;

        //If too many right
        if (p2Hovering > 3)
            p2Hovering = 0;

        //If too many left
        if (p2Hovering < 0)
            p2Hovering = 3;

        p2.transform.position = new Vector3(gods[p2Hovering].transform.position.x, p2.transform.position.y, p2.transform.position.z);
    }

    public Enums.God editGod(int what)
    {
        switch (what)
        {
            case 0:
                return Enums.God.God3;
            case 1:
                return Enums.God.God1;
            case 2:
                return Enums.God.God2;
            case 3:
                return Enums.God.God4;
            default:
                Debug.LogError("There is no that many gods.");
                return Enums.God.God4;
        }
    }

}
