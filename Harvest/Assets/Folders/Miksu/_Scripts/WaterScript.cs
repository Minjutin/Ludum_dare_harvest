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

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player;
        player = other.GetComponent<PlayerMovement>();  // TODO: Change to PlayerWetter etc.
        if (player)
        {
            // Get wet
            //Debug.Log("Splish splash!");
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
