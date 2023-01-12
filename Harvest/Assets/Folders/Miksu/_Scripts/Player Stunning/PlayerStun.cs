using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    #region Properties
    PlayerInventory playerInventory;
    PlayerInteraction playerInteraction;

    [Tooltip("How long the OTHER Player will remain stunned")]
    [SerializeField] float stunTime = 1f;

    [Header("Knockback Power")]
    [SerializeField] float stunKnockBackPower = 15f;

    #endregion

    #region Setup
    private void Start()
    {
        // Get references
        playerInteraction = GetComponent<PlayerInteraction>();
        playerInventory = playerInteraction.inventory;
    }
    #endregion

    #region Collision Checking
    private void OnCollisionEnter(Collision collision)
    {
        // Check if other is Player
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Collision with Player is happening
            CollideWithPlayer(collision.gameObject);
        }
    }

    private void CollideWithPlayer(GameObject otherPlayer)
    {
        // Check inventory size
        int otherItemsAmount = otherPlayer.GetComponent<PlayerInteraction>().inventory.ItemsAmount();
        int yourItemsAmount = playerInventory.ItemsAmount();

        // If it is higher than your inventory...
        if (yourItemsAmount <= otherItemsAmount)
        {
            // --> STUN the other Player
            StunOtherPlayer(otherPlayer);
        }
    }
    #endregion

    #region Stunning
    private void StunOtherPlayer(GameObject otherPlayer)
    {
        // Get their rb
        PlayerMovement pMove = otherPlayer.GetComponent<PlayerMovement>();

        // Calculate the HitDirection
        Vector3 hitDir = (otherPlayer.transform.position - transform.position).normalized;

        // Ram them
        pMove.GetRammed(hitDir, stunKnockBackPower);

        // Actually stun them
        otherPlayer.GetComponent<PlayerStun>().GetStunned(stunTime);
    }

    public void GetStunned(float stunDuration)
    {
        // Activate the Stun Timer
        StartCoroutine(StunTimer(stunDuration));
    }

    IEnumerator StunTimer(float stunDuration)
    {
        // Get playerInput
        PlayerInput input = playerInventory.GetComponent<PlayerInput>();

        Debug.Log("Begin stun");

        // Check if already stunLocked
        if (input.stunLocked != true)
        {
            // Stunlock them
            input.stunLocked = true;

            // Wait for the stunTime
            yield return new WaitForSeconds(stunDuration);

            // End the stunlock
            input.stunLocked = false;
            Debug.Log("END stun");
        }
    }
    #endregion
}
