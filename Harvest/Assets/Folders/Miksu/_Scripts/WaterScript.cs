using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    #region Properties
    [Header("Collision Size")]
    [SerializeField] float sphereRadius = 5f;
    SphereCollider collider;

    #endregion

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
        collider.radius = sphereRadius;
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerWet player;
        player = other.GetComponent<PlayerWet>();  // TODO: Change to PlayerWetter etc.
        if (player)
        {
            player.GetWet();

            // Lower Player Sprite into the water
            player.GetInLake();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerWet player;
        player = other.GetComponent<PlayerWet>();
        if (player)
        {
            // Raise Player Sprites off the water
            player.GetOutOfLake();
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the Sphere of set radius
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);

        // Draw center
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
