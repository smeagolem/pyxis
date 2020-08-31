using UnityEngine;
using System;

public class ClickPointSystem : MonoBehaviour
{
    public new Camera camera;
    public static event Action<Vector3> pointClicked;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                pointClicked?.Invoke(hit.point);
            }
        }
    }
}
