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


}
