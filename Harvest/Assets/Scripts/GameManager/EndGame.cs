using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject endImage;

    PlayerStats stats1;
    PlayerStats stats2;

    Object[] endSprites;
    Sprite spriteNow;

    private void Start()
    {
        endSprites = Resources.LoadAll("EndScenes", typeof(Sprite));
        stats1 = GameObject.Find("Player Controller").GetComponent<PlayerStats>();
        stats2 = GameObject.Find("Player Controller2").GetComponent<PlayerStats>();
    }

    //Open the ending screen.
    public void EndEverything()
    {
        //TODO stop game
        TurnOffPlayers();
        TurnOffRams();

        StartCoroutine(End());
    }

    IEnumerator End()
    {
        //TODO stop everything
        

        yield return new WaitForSeconds(3f);
        CheckWhoWon();
        endImage.GetComponent<Image>().sprite = spriteNow;
        endImage.SetActive(true);
    }

    public void CheckWhoWon()
    {
        PlayerStats stats;
        
        if(stats1.points.GetPoints() > stats2.points.GetPoints())
        {
            stats = stats1;
        }
        else
        {
            stats = stats2;
        }

        switch (stats.god)
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
    }

    #region Do End Show
    private void TurnOffPlayers()
    {
        // Find all Players
        //PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
        PlayerInput[] players = FindObjectsOfType<PlayerInput>();

        // Turn them off
        //foreach (PlayerMovement p in players)
        foreach (PlayerInput p in players)
        {
            // Turn off PlayerInput
            p.stunLocked = true;
        }

    }

    private void TurnOffRams()
    {
        // Find all rams
        Ram[] rams = FindObjectsOfType<Ram>();

        if (rams != null)
        {
            foreach(Ram r in rams)
            {
                // Turn off
                r.ramIsActive = false;
            }
        }
    }
    #endregion
}
