using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    PlayMenuSounds sound;
    private void Start()
    {
        sound = Camera.main.GetComponent<PlayMenuSounds>();

        p1 = GameObject.Find("Indicator1");
        p2 = GameObject.Find("Indicator2");

        p1Hovering = 1;
        p2Hovering = 1;

        MoveP1(-1);
        MoveP2(-1);

    }

    // Update is called once per frame
    void Update()
    {
        //----------------------------PLAYER 1 -------------------------

        //If there is no god for player 1, you can move or lock a god
        if (p1chosen == -1)
        {
            //Player1 MOVEMENT
            if (Input.GetKeyDown(Statics.p1left))
            {
                MoveP1(-1);
            }
            else if (Input.GetKeyDown(Statics.p1right))
            {
                MoveP1(1);
            }

            //Player1 LOCK YOUR CHOICE
            if (Input.GetKeyDown(Statics.p1interaction) || Input.GetKeyDown(Statics.p1inventory))
            {
                sound.PlayChoose();

                if (p1Hovering != p2chosen)
                {
                    p1chosen = p1Hovering;
                    p1.SetActive(false);
                }

            }
        }

        //If there is god, you can remove your choice by another button.
        else if(p1chosen != -1)
        {

            if (Input.GetKeyDown(Statics.p1interaction) || Input.GetKeyDown(Statics.p1inventory))
            {
                sound.PlayChoose();

                p1chosen = -1;
                p1.SetActive(true);
            }
        }


        //-----------------------------PLAYER 2 ---------------------

        //If there is no god for player 2, you can move or lock a god
        if (p2chosen == -1)
        {
            //Move player
            if (Input.GetKeyDown(Statics.p2left))
            {
                MoveP2(-1);
            }
            else if (Input.GetKeyDown(Statics.p2right))
            {
                MoveP2(1);
            }

            if (Input.GetKeyDown(Statics.p2interaction) || Input.GetKeyDown(Statics.p2inventory))
            {
                sound.PlayChoose();

                if (p2Hovering != p1chosen)
                {
                    p2chosen = p2Hovering;
                    p2.SetActive(false);
                }
                
            }
        }

        //If there is god, you can remove your choice by another button.
        else if (p2chosen != -1)
        {

            if (Input.GetKeyDown(Statics.p2interaction) || Input.GetKeyDown(Statics.p2inventory))
            {
                sound.PlayChoose();

                p2chosen = -1;
                p2.SetActive(true);
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
        if (p1Hovering!= p2Hovering){
            gods[p1Hovering].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }

        p1Hovering = p1Hovering + direction;

        //If too many right
        if (p1Hovering > 3)
            p1Hovering = 0;

        //If too many left
        if (p1Hovering < 0)
            p1Hovering = 3;

        p1.transform.position = new Vector3(gods[p1Hovering].transform.position.x-8, p1.transform.position.y, p1.transform.position.z);
        gods[p1Hovering].GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    public void MoveP2(int direction)
    {
        if (p1Hovering != p2Hovering)
        {
            gods[p2Hovering].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }

        p2Hovering = p2Hovering + direction;

        //If too many right
        if (p2Hovering > 3)
            p2Hovering = 0;

        //If too many left
        if (p2Hovering < 0)
            p2Hovering = 3;

        p2.transform.position = new Vector3(gods[p2Hovering].transform.position.x+10, p2.transform.position.y, p2.transform.position.z);
        gods[p2Hovering].GetComponent<Image>().color = new Color(1, 1, 1, 1);
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
