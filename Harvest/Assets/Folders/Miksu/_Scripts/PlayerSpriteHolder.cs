using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteHolder : MonoBehaviour
{
    // Refs
    SpriteRenderer renderer;


    [Header("Bag Lad Animations")]
    [SerializeField] GameObject[] bagLad_animations;

    [Header("Ram Sprites")]
    [SerializeField] GameObject[] ramGal_animations;

    [Header("Scare Crow Sprites")]
    [SerializeField] GameObject[] scareCrow_animations;

    [Header("Water Boi Sprites")]
    [SerializeField] GameObject[] waterBoi_animations;

    enum character
    {
        bagLad, ram, scareCrow, waterBoi
    }
    character currentCharacter;
    GameObject[] characterAnimations;
    enum aniState
    {
        idle, walkBack, walkFront
    }
    GameObject[] animationsInUse;
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

            animationsInUse[i] = newAnimation;

            // Turn them off
            newAnimation.SetActive(false);
            Debug.Log("Cycle");
            i++;
        }

        // Turn idle animation back on
        animationsInUse[2].SetActive(true);
    }

    private void Awake()
    {
        // Get the SpriteRenderer
        //renderer = GetComponent<SpriteRenderer>();

        // TEST
        SetPlayerSprites(Enums.God.God1);
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
