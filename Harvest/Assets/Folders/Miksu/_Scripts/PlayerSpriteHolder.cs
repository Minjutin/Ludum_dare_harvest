using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteHolder : MonoBehaviour
{
    // Refs
    SpriteRenderer renderer;


    [Header("Bag Lad Animations")]
    [SerializeField] List<GameObject> bagLad_animations;

    [Header("Ram Sprites")]
    [SerializeField] List<GameObject> ramGal_animations;

    [Header("Scare Crow Sprites")]
    [SerializeField] List<GameObject> scareCrow_animations;

    [Header("Water Boi Sprites")]
    [SerializeField] List<GameObject> waterBoi_animations;

    enum character
    {
        bagLad, ram, scareCrow, waterBoi
    }
    character currentCharacter;
    List<GameObject> characterAnimations = new List<GameObject>();
    enum aniState
    {
        idle, walkBack, walkFront
    }
    List<GameObject> animationsInUse = new List<GameObject>();
    GameObject currentAnimation;

    public void SetPlayerSprites(Enums.God godType)
    {
        switch (godType)
        {
            case Enums.God.God1:

                currentCharacter = character.bagLad;
                characterAnimations = bagLad_animations;

                break;
            // =========================
            case Enums.God.God2:
                currentCharacter = character.ram;
                characterAnimations = ramGal_animations;

                break;

            // =========================
            case Enums.God.God3:
                currentCharacter = character.scareCrow;
                characterAnimations = scareCrow_animations;

                break;

            // =========================
            case Enums.God.God4:
                currentCharacter = character.waterBoi;
                characterAnimations = waterBoi_animations;

                break;

            // =========================
            default:
                Debug.LogError("Invalid type of God");
                break;
        }

        InitializeAnimations();

        //StartCoroutine(IdleAnimation());
    }

    private void InitializeAnimations()
    {
        int i = 0;
        foreach(GameObject animation in characterAnimations)
        {
            // Initialize and turn off
            GameObject newAnimation = Instantiate(characterAnimations[i], transform);

            animationsInUse.Add(newAnimation);

            // Turn them off
            newAnimation.SetActive(false);
            i++;
        }

        // Turn idle animation back on
        animationsInUse[0].SetActive(true);
    }

    private void Awake()
    {
        // Get the SpriteRenderer
        //renderer = GetComponent<SpriteRenderer>();

        // TEST
        //SetPlayerSprites(Enums.God.God1);
    }


    IEnumerator IdleAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            // Cycle idle sprites
            //currentAnimations[Random.Range(0, ram.Length)];
        }
    }


}
