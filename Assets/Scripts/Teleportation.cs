using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform playerCamera;
    public Transform player;
    public Transform receiver;
    public float rotation;

    private bool playerIsOverlapping = false;

    // Update is called once per frame
    void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = playerCamera.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // Player moved accross the portal, let's teleport him
            if (dotProduct > 0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += rotation;
                playerCamera.Rotate(0, rotationDiff, 0, Space.World);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                positionOffset.y -= 1.36144f; // camera height
                player.position = receiver.position + positionOffset;

                playerIsOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }
}
