using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    GameObject player;

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
        
        stats = this.GetComponent<PlayerStats>();
        constants = FindObjectOfType<Constants>();

        fruitPics = Resources.LoadAll("InventorySprites",typeof(Sprite));

        player = GetComponent<PlayerInput>().player;
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
                StartCoroutine(NewWanted());

                GameObject seed = Instantiate(Resources.Load(GetFruit(fruit.Type)), GetRandomPos(player), Quaternion.identity) as GameObject;
                seed.GetComponent<Sacrifice>().PlayAnimation(Sacrifice.animation.fruit);
            }

            //Fruits is god's favorite type
            else if (fruit.Type == Enums.FruitType.Fruit1 && stats.god == Enums.God.God1
                || fruit.Type == Enums.FruitType.Fruit2 && stats.god == Enums.God.God2
                || fruit.Type == Enums.FruitType.Fruit3 && stats.god == Enums.God.God3
                || fruit.Type == Enums.FruitType.Fruit4 && stats.god == Enums.God.God4
            )
            {
                points += constants.favoritePoints;
                GameObject seed = Instantiate(Resources.Load(GetFruit(fruit.Type)), GetRandomPos(player), Quaternion.identity) as GameObject;
                seed.GetComponent<Sacrifice>().PlayAnimation(Sacrifice.animation.fruit);
            }

            //Otherwise
            else
            {
                points += constants.fruitPoints;
                GameObject seed = Instantiate(Resources.Load(GetFruit(fruit.Type)), GetRandomPos(player), Quaternion.identity) as GameObject;
                seed.GetComponent<Sacrifice>().PlayAnimation(Sacrifice.animation.fruit);
            }
        }

        //If player gives god a seed.
        if (item is Seed)
        {
            points += constants.seedPoints;
            GameObject seed = Instantiate(Resources.Load("Sacrifice/Sac_Seed"), GetRandomPos(player), Quaternion.identity) as GameObject;
            seed.GetComponent<Sacrifice>().PlayAnimation(Sacrifice.animation.seed);
        }

        //If player gives god poop.
        if (item is Manure)
        {
            points += constants.manurePoints;
            GameObject manure = Instantiate(Resources.Load("Sacrifice/Sac_Manure"), GetRandomPos(player), Quaternion.identity) as GameObject;
            manure.GetComponent<Sacrifice>().PlayAnimation(Sacrifice.animation.manure);
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

        StartCoroutine(NewWanted());
    }

    public int GetPoints()
    {
        return points;
    }

    IEnumerator NewWanted()
    {
        yield return new WaitForSeconds(0.2f);
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
        //Debug.Log("New wanted is " + wantedFruit);
        bubble.GetComponent<SpriteRenderer>().sprite = fruitSprite;
    }
    #endregion

    public void CheckIfWon()
    {
        if (points >= constants.winPoints)
        {
            UIelements.transform.parent.parent.GetComponent<EndGame>().EndEverything();
        }
    }


    #region Helper Functions
    private Vector3 GetRandomPos(GameObject p)
    {
        Vector3 pos = p.transform.position;

        // Randomize the placement a little bit
        pos = new Vector3(pos.x + Random.Range(-1f, 1f),
                            pos.y + Random.Range(1f, 2f),
                              pos.z + Random.Range(0f, 1f));

        return pos;
    }

    private string GetFruit(Enums.FruitType fruit)
    {
        if (fruit == Enums.FruitType.Fruit1)
        {
            return "Sacrifice/Sac_Fruit 1";
        }
        else if (fruit == Enums.FruitType.Fruit2)
        {
            return "Sacrifice/Sac_Fruit 2";
        }
        else if (fruit == Enums.FruitType.Fruit3)
        {
            return "Sacrifice/Sac_Fruit 3";
        }
        else if (fruit == Enums.FruitType.Fruit4)
        {
            return "Sacrifice/Sac_Fruit 4";
        }

        return null;
    }
    #endregion
}
