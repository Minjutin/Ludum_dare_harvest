using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{

    [Header("Current points.")]
    [SerializeField] int points;

    PlayerStats stats;

    Constants constants;
    public GameObject UIelements;
    GameObject bubble;

    Object[] fruitPics;

    [Header("Wanted plant")]
    [SerializeField] Enums.FruitType wantedFruit;

    private void Start()
    {
        endSprites = Resources.LoadAll("EndScenes", typeof(Sprite));
        
        stats = this.GetComponent<PlayerStats>();
        constants = FindObjectOfType<Constants>();

        fruitPics = Resources.LoadAll("InventorySprites",typeof(Sprite));
    }

    #region POINTS
    public void GiveItem(InventoryItem item)
    {
        //If player gives god a fruit.
        if (item is Fruit)
        {
            Fruit fruit = item as Fruit;

            //Check the points you get from this fruit. ONLY ONE KIND OF POINTS CAN BE GIVEN.

            //Fruit is wanted.
            if (fruit.Type == wantedFruit)
            {
                points += constants.wantedPoints;
                NewWanted();

            }

            //Fruits is god's favorite type
            else if (fruit.Type == Enums.FruitType.Fruit1 && stats.god == Enums.God.God1
                || fruit.Type == Enums.FruitType.Fruit2 && stats.god == Enums.God.God2
                || fruit.Type == Enums.FruitType.Fruit3 && stats.god == Enums.God.God3
                || fruit.Type == Enums.FruitType.Fruit4 && stats.god == Enums.God.God4
            )
            {
                points += constants.favoritePoints;
            }

            //Otherwise
            else
            {
                points += constants.fruitPoints;
            }
        }

        //If player gives god a seed.
        if (item is Seed)
        {
            points += constants.seedPoints;
        }

        //If player gives god poop.
        if (item is Manure)
        {
            points += constants.manurePoints;
        }

        //Make points so it's better
        if (points > constants.winPoints)
            points = constants.winPoints;
        if (points < 0)
            points = 0;
        //Let's display the pointsss.
        RectTransform rt = UIelements.transform.Find("Fill").GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2((300 * points) / constants.winPoints, rt.sizeDelta.y);

        CheckIfWon();
    } //Give item to the God.

    public void InitializePoints(Enums.God godIs)
    {
        GameObject goGod;

        switch (godIs)
        {
            case Enums.God.God1:
                goGod = GameObject.Find("God1");
                break;
            case Enums.God.God2:
                goGod = GameObject.Find("God2");
                break;
            case Enums.God.God3:
                goGod = GameObject.Find("God3");
                break;
            case Enums.God.God4:
                goGod = GameObject.Find("God4");
                break;
            default:
                Debug.LogError("There is no god");
                goGod = GameObject.Find("God1");
                break;
        }
        bubble = goGod.transform.Find("Graphics").Find("SpeechBubble").Find("Item").gameObject;
        NewWanted();
    }

    public void NewWanted()
    {
        int randFruit = Random.Range(0, 4);
        Sprite fruitSprite = fruitPics[randFruit] as Sprite;

        switch (randFruit)
        {
            case 0:
                wantedFruit = Enums.FruitType.Fruit1;
                break;
            case 1:
                wantedFruit = Enums.FruitType.Fruit2;
                break;
            case 2:
                wantedFruit = Enums.FruitType.Fruit3;
                break;
            case 3:
                wantedFruit = Enums.FruitType.Fruit4;
                break;
            default:
                Debug.Log(randFruit + " is not a correct fruit number.");
                break;

        }
        Debug.Log("New wanted is " + wantedFruit);
        bubble.GetComponent<SpriteRenderer>().sprite = fruitSprite;
    }
    #endregion

    Object[] endSprites;
    public void CheckIfWon()
    {
        if (points >= constants.winPoints)
        {
            Sprite spriteNow;

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

            UIelements.transform.parent.parent.GetComponent<EndGame>().OpenEnding(spriteNow);
        }
    }
}
