using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilsClass
{
    public static Vector3 GetMouseWorldPosition(int distanceFromCamera)
    {
        Vector3 searchPlane = Input.mousePosition + new Vector3(0, 0, distanceFromCamera);
        Vector3 vec = GetMousePositionWithZ(searchPlane, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMousePositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
