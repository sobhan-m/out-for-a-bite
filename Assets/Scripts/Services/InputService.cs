using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService
{
    public static Camera cam = Camera.main;

    public static Vector3 GetDifferenceFromMouse(Vector3 position)
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = position.z;
        return mousePosition - position;
    }
}
