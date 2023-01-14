using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrifice : MonoBehaviour
{
    [SerializeField] Animator animator;

    public enum animation
    {
        fruit, favourite, wanted, seed, manure
    }

    public void PlayAnimation(animation animation)
    {
        switch (animation)
        {
            case animation.fruit:
                animator.Play("Sacrifice_Float");
                break;

            case animation.favourite:
                animator.Play("Sacrifice_Float");

                break;

            case animation.wanted:
                animator.Play("Sacrifice_Float");

                break;

            case animation.seed:
                animator.Play("Sacrifice_Float");

                break;

            case animation.manure:
                animator.Play("Sacrifice_Float");

                break;

        }
    }

    private void Start()
    {
        PlayAnimation(animation.fruit);

        Destroy(gameObject, 3f);
    }
}
