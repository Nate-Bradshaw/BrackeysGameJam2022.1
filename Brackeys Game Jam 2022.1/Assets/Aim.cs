using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private float zAxis = -2f;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private Vector3 mousePosRaw;
    [SerializeField] private Vector3 gridPos;
    [SerializeField] private bool validGrid;

    [SerializeField] private GameObject lineRender;

    private int layerMask;
    // Start is called before the first frame update
    void Start()
    {
        aimPoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        layerMask = 1 << 8; //only hits 8
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask))
        {
            //aimPoint.position = new Vector3(raycastHit.point.x, raycastHit.point.y, zAxis);
            mousePosRaw = new Vector3(raycastHit.point.x, raycastHit.point.y, zAxis);
            validGrid = true;
        }
        else
        {
            validGrid = false;
        }

        //snap to grid
        gridPos = new Vector3(Mathf.Round(mousePosRaw.x / 5) * 5, Mathf.Round(mousePosRaw.y / 5) * 5, zAxis);
        aimPoint.position = gridPos;

        //shoot

        if (Input.GetMouseButtonDown(0) && validGrid)
        {
            GameObject myLine = Instantiate(lineRender, transform);
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, aimPoint.transform.position);
            GameObject.Destroy(myLine, 2f);

            //DrawLine(transform.position, aimPoint.transform.position, Color.yellow, 1f, 1f);
        }
    }

    void DrawLine(Vector3 start, Vector3 end, Color colour, float duration = 0.2f, float width = 1f) //https://answers.unity.com/questions/8338/how-to-draw-a-line-using-script.html Answer by paranoidray
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = colour;
        lr.startWidth = width;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

}
