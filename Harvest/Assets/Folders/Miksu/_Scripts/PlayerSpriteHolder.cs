using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteHolder : MonoBehaviour
{
    // Refs
    SpriteRenderer renderer;
    Animator animator;

    [Header("Bag Lad Animator")]
    //[SerializeField] List<GameObject> bagLad_animations;
    [SerializeField] RuntimeAnimatorController bagLad_animator;

    [Header("Ram Animator")]
    //[SerializeField] List<GameObject> ramGal_animations;
    [SerializeField] RuntimeAnimatorController ramGal_animator;

    [Header("Scare Crow Animator")]
    //[SerializeField] List<GameObject> scareCrow_animations;
    [SerializeField] RuntimeAnimatorController scareCrow_animator;

    [Header("Water Boi Animator")]
    //[SerializeField] List<GameObject> waterBoi_animations;
    [SerializeField] RuntimeAnimatorController waterBoi_animator;

    enum character
    {
        bagLad, ram, scareCrow, waterBoi
    }
    character currentCharacter;

    //List<GameObject> characterAnimations = new List<GameObject>();
    //enum aniState
    //{
    //    idle, walkBack, walkFront
    //}
    //List<GameObject> animationsInUse = new List<GameObject>();
    //GameObject currentAnimation;

    public void SetPlayerSprites(Enums.God godType)
    {
        animator = this.GetComponent<Animator>();

        switch (godType)
        {
            case Enums.God.God4:

                currentCharacter = character.bagLad;
                animator.runtimeAnimatorController = bagLad_animator;

                break;
            // =========================
            case Enums.God.God3:
                currentCharacter = character.ram;
                animator.runtimeAnimatorController = ramGal_animator;

                break;

            // =========================
            case Enums.God.God1:
                currentCharacter = character.scareCrow;
                animator.runtimeAnimatorController = scareCrow_animator;

                break;

            // =========================
            case Enums.God.God2:
                currentCharacter = character.waterBoi;
                animator.runtimeAnimatorController = waterBoi_animator;

                break;

            // =========================
            default:
                Debug.LogError("Invalid type of God");
                break;
        }

        //InitializeAnimations();

        //StartCoroutine(IdleAnimation());
    }

    //private void InitializeAnimations()
    //{
    //    int i = 0;
    //    foreach(GameObject animation in characterAnimations)
    //    {
    //        // Initialize and turn off
    //        GameObject newAnimation = Instantiate(characterAnimations[i], transform);

    //        animationsInUse.Add(newAnimation);

    //        // Turn them off
    //        newAnimation.SetActive(false);
    //        i++;
    //    }

    //    // Turn idle animation back on
    //    animationsInUse[0].SetActive(true);
    //}

    private void Awake()
    {
        // Get the SpriteRenderer
        //renderer = GetComponent<SpriteRenderer>();

        // TEST
        //SetPlayerSprites(Enums.God.God1);
    }


    //IEnumerator IdleAnimation()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(1f);

    //        // Cycle idle sprites
    //        //currentAnimations[Random.Range(0, ram.Length)];
    //    }
    //}


}
