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

    public static float FindDegreeFromMouse(Vector3 position)
    {
        Vector3 vectorToMouse = GetDifferenceFromMouse(position);
        float angleRadian = Mathf.Atan2(vectorToMouse.y, vectorToMouse.x);
        return angleRadian * Mathf.Rad2Deg;
    }
}
