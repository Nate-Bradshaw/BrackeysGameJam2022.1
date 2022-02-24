using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private float yAxis = -2f;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private Vector3 mousePosRaw;
    [SerializeField] private Vector3 gridPos;
    [SerializeField] private bool validGrid;

    [SerializeField] private GameObject lineRender;

    [SerializeField] private GameObject missile;
    public Vector3 clickLocation;
    public static Aim instance;

    private int layerMask;

    private bool waiting;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        aimPoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        bool canFire = PlayerController.instance.firing;

        layerMask = 1 << 8; //only hits 8
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask))
        {
            //aimPoint.position = new Vector3(raycastHit.point.x, raycastHit.point.z, zAxis);
            mousePosRaw = new Vector3(raycastHit.point.x, yAxis, raycastHit.point.z);
            validGrid = true;
        }
        else
        {
            validGrid = false;
        }

        //snap to grid
        gridPos = new Vector3(Mathf.Round(mousePosRaw.x / 5) * 5, yAxis, Mathf.Round(mousePosRaw.z / 5) * 5);
        aimPoint.position = gridPos;

        //shoot

        if (Input.GetMouseButtonDown(0) && validGrid && canFire && !waiting) //move to player controller script to make sure it on beat and not at the same time as moving?
        {
            /*
            GameObject myLine = Instantiate(lineRender, transform);
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, aimPoint.transform.position);
            GameObject.Destroy(myLine, 2f);
            */

            clickLocation = gridPos;
            GameObject Mymissile = Instantiate(missile, this.transform);
            Mymissile.transform.position = transform.position;

            waiting = true;
            StartCoroutine(Cooldown());
            //Vector3 target = myMissile.GetComponent<ArcMovement>(targetPos);

            //DrawLine(transform.position, aimPoint.transform.position, Color.yellow, 1f, 1f);
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.4f);
        waiting = false;
    }
}
