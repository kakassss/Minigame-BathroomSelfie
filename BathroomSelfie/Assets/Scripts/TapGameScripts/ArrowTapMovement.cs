using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTapMovement : MonoBehaviour
{
    public static ArrowTapMovement current;

    [HideInInspector] public Vector3 difference;
    [SerializeField] private Camera mainCamera;

    private Vector3 firstPos;
    private Vector3 currentPos;

    private void Update()
    {
        GetMousePos();
        GetDifference();
    }
    private void GetMousePos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = mainCamera.WorldToViewportPoint(Input.mousePosition);
        }
    }
    private void GetDifference()
    {
        if (Input.GetMouseButton(0))
        {
            currentPos = mainCamera.WorldToViewportPoint(Input.mousePosition);
            difference = firstPos - currentPos;
            difference.Normalize();
        }
        if (Input.GetMouseButtonUp(0))
        {
            difference = Vector3.zero;
        }
    }
   
}
