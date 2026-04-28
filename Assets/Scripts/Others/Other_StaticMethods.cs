using UnityEngine;
using UnityEngine.InputSystem;

public static class StaticMethods
{
    public static Vector3 GetMouseWorldPosition2D()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = 0f;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public static Vector3 GetMouseWorldPosition3D()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out float dist))
            return ray.GetPoint(dist);

        return Vector3.zero;
    }
}
