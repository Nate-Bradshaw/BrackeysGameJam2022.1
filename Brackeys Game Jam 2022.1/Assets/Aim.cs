using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private float zAxis = -2f;
    [SerializeField] private Transform aimPoint;
    // Start is called before the first frame update
    void Start()
    {
        aimPoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
            aimPoint.position = new Vector3(raycastHit.point.x, raycastHit.point.y, zAxis);
    }
}
