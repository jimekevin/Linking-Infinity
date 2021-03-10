using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform origin;

    private void LateUpdate()
    {
        var playerOffset = playerCamera.position - portal.position;
        var transformedPlayerOffset = origin.rotation * playerOffset;
        transform.position = origin.position + transformedPlayerOffset;
        transform.rotation = origin.rotation * playerCamera.rotation;
    }

    // Took me way too long to figure this out, code below shows a lot I tried. 
    // In the end it was way too easy ...

    /*private void LateUpdate()
    {
        // distance between camera position and portal position
        // angle a betweenn directional vector of (camera point - portal position) and normal of portal line (order here is fixed)
        // second portal: raycast

        // World positions
        var worldPortalPosition          = transform.root.TransformDirection(portal.position);
        var worldNewPlayerCameraPosition = transform.root.TransformDirection(playerCamera.position);
        var worldOriginPosition          = transform.root.TransformDirection(origin.position);
        var worldSecondCameraPosition    = transform.root.TransformDirection(transform.position);

        //var uv1 = (worldPortalPosition - worldNewPlayerCameraPosition).normalized;
        //var uv2 = (worldOriginPosition - worldSecondCameraPosition).normalized;
        //var angle = Quaternion.Angle(portal.rotation, origin.rotation);
        var angle = Quaternion.FromToRotation(playerCamera.position, portal.position);

        var playerOffset = playerCamera.position - portal.position;
        var transformedPlayerOffset = origin.rotation * playerOffset;
        transform.position = origin.position + transformedPlayerOffset;

        var angle2 = Quaternion.Angle(playerCamera.rotation, portal.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angle2, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * origin.forward;
        //transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        transform.rotation = origin.rotation * playerCamera.rotation;

        //Debug.Log(transform.rotation);
        //Debug.Log(worldPortalPosition);
        //Debug.Log(worldNewPlayerCameraPosition);
        //Debug.Log(worldOriginPosition);
        //Debug.Log(worldSecondCameraPosition);
        Debug.Log(playerOffset);
        Debug.Log(transformedPlayerOffset);
        Debug.Log("=============================");
        //Debug.Log(uv1);
        //Debug.Log(uv2);
        //Debug.Log(playerOffset);
        //Debug.Log(angle);
        //Debug.Log(transformedPlayerOffset);
        //Debug.Log(transform.position);
        //var transformedPlayerOffset = Quaternion.Euler() //transform.RotateAround(playerOffset, worldOriginPosition, angle);
        //transform.position = transform.InverseTransformPoint(worldOriginPosition + transformedPlayerOffset); // calc1()

        //Debug.Log(playerOffset);
        //Debug.Log(localPlayerOffset);
    //}

    //public Transform playerCamera;
    //public Transform portal;
    //public Transform otherPortal;

    // Update is called once per frame
    //void Update()
    //{
    //    Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
    //    transform.position = portal.position + playerOffsetFromPortal;

    //    float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

    //    Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
    //    Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
    //    transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    //}*/
}
