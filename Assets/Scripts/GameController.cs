using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GameController : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public CharacterController controller;
    public XRNode inputSourceLeft;
    public XRNode inputSourceRight;
    public float[] zoomDecelerationFactors;
    public GameObject[] zoomOuterTriggers;
    public GameObject[] zoomInnerTriggers;

    // Start is called before the first frame update
    void Start()
    {
        var allGamepads = Gamepad.all;
        var gamepad = Gamepad.current;
        Camera.main.enabled = true;
    }

    void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return; // No gamepad connected.
        }

        // Zoom in speed calculation

        float velocity = moveSpeed;

        for (int i = 0; i < zoomInnerTriggers.Length; ++i)
        {
            var zoomInnerCollider = zoomInnerTriggers[i].GetComponent<SphereCollider>();
            var zoomOuterCollider = zoomOuterTriggers[i].GetComponent<SphereCollider>();
            
            var minDistance = zoomInnerCollider.radius * zoomInnerCollider.transform.lossyScale.x;
            var maxDistance = zoomOuterCollider.radius * zoomOuterCollider.transform.lossyScale.x;
            var distance = Vector3.Distance(zoomOuterCollider.bounds.center, Camera.main.transform.position);

            var isInside = zoomOuterCollider.bounds.Contains(Camera.main.transform.position);
            if (isInside && distance <= maxDistance)
            {
                var factor = Mathf.Pow(distance / maxDistance, 2);
                var minFactor = Mathf.Pow(minDistance / maxDistance, 2);
                factor = Mathf.Clamp(factor, minFactor, 1.0f) * zoomDecelerationFactors[i];
                velocity *= factor;
            }
        }

        // Get controller stick values (either gamepad or XR device)
        var deviceLeft = InputDevices.GetDeviceAtXRNode(inputSourceLeft);
        var deviceRight = InputDevices.GetDeviceAtXRNode(inputSourceRight);
        Vector2 inputAxisLeft, inputAxisRight;
        var foundXrLeft = deviceLeft.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out inputAxisLeft);
        var foundXrRight = deviceRight.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out inputAxisRight);
        var ls = foundXrLeft ? new Vector2(inputAxisLeft.x, inputAxisLeft.y) : gamepad.leftStick.ReadValue();
        var rs = foundXrRight ? new Vector2(inputAxisRight.x, inputAxisRight.y) : gamepad.rightStick.ReadValue();

        // Adjust camera accordingly
        // VR uses continuous turn provider but only allows 2D, 3D does not work with this snippet unfortunately
        if (!foundXrRight)
        {
            var rotateValue = new Vector3(rs.y, rs.x * -1, 0) * 2.0f;
            Camera.main.transform.eulerAngles -= rotateValue;
        }

        // Move the player in camera direction
        controller.transform.position += Camera.main.transform.forward * ls.y * velocity * Time.deltaTime + Camera.main.transform.right * ls.x * velocity * Time.deltaTime;
    }
}
