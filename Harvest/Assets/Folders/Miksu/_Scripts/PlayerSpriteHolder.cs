using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteHolder : MonoBehaviour
{
    // Refs
    SpriteRenderer renderer;


    [Header("Bag Lad Sprites")]
    [SerializeField] Sprite[] bagLad;

    [Header("Ram Sprites")]
    [SerializeField] Sprite[] ram;

    [Header("Scare Crow Sprites")]
    [SerializeField] Sprite[] crow;

    [Header("Water Boi Sprites")]
    [SerializeField] Sprite[] waterBoi;

    enum character
    {
        bagLad, ram, scareCrow, waterBoi
    }
    character currentCharacter;
    Sprite[] currentSprites;

    public void SetPlayerSprites(Enums.God godType)
    {
        switch (godType)
        {
            case Enums.God.God1:

                currentCharacter = character.bagLad;
                currentSprites = bagLad;

                break;
            // =========================
            case Enums.God.God2:
                currentCharacter = character.ram;
                currentSprites = ram;

                break;

            // =========================
            case Enums.God.God3:
                currentCharacter = character.scareCrow;
                currentSprites = crow;

                break;

            // =========================
            case Enums.God.God4:
                currentCharacter = character.waterBoi;
                currentSprites = waterBoi;

                break;

            // =========================
            default:
                Debug.LogError("Invalid type of God");
                break;
        }

        //StartCoroutine(IdleAnimation());
    }

    private void Awake()
    {
        // Get the SpriteRenderer
        renderer = GetComponent<SpriteRenderer>();
    }


    IEnumerator IdleAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            // Cycle idle sprites
            renderer.sprite = ram[Random.Range(0, ram.Length)];
        }
    }


}
