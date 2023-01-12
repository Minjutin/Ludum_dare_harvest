using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    #region Properties
    PlayerInventory playerInventory;
    PlayerInteraction playerInteraction;

    [Header("Knockback Power")]
    [SerializeField] float stunKnockBackPower = 15f;

    #endregion

    #region Setup
    private void Start()
    {
        // Get references
        playerInteraction = GetComponent<PlayerInteraction>();
        //playerInventory = playerInteraction.
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
        // TODO

        // If it is higher than your inventory...

        // --> STUN the other Player
        StunOtherPlayer(otherPlayer);
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

        Debug.Log(otherPlayer.name + " got stunned!");
    }
    #endregion
}
