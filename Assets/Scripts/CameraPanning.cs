using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanning : MonoBehaviour
{
    public float mouseSensitivity = 0.5f;
    private Vector3 lastPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPosition = GetMousePosition();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = lastPosition - GetMousePosition();
            float sensitivity = mouseSensitivity/100f;
            transform.Translate(delta.x * sensitivity, delta.y * sensitivity, 0);
            lastPosition = GetMousePosition();
        }
    }

    private Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }
}
