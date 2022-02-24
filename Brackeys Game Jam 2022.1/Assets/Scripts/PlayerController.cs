using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour //need to impliment the beat system with game controller
{
    //https://www.youtube.com/watch?v=mbzXIOKZurA

    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float moveDistanceMult = 5f;
    [SerializeField] private Transform movePoint;

    [SerializeField] private LayerMask collision;

    [SerializeField] private float beatIntL;
    [SerializeField] private float beatIntH;
    [SerializeField] private float beatFloat;

    public static PlayerController instance;

    public bool firing;

    public static bool FastApproximately(float a, float b, float threshold)
    {
        if (threshold > 0f)
        {
            return Mathf.Abs(a - b) <= threshold;
        }
        else
        {
            return Mathf.Approximately(a, b);
        }
    }

    public GameObject test;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        movePoint.parent = null; //unparent movepoint at start so it can move seperatly
    }

    // Update is called once per frame
    void Update()
    {
        beatFloat = Conductor.instance.songBeatsPosBase;
        beatIntL = Mathf.Ceil(Conductor.instance.songBeatsPosBase) - 1f + 0.5f; //truncated
        beatIntH = Mathf.Ceil(Conductor.instance.songBeatsPosBase) + 0.5f;

        if (FastApproximately(beatIntL, beatFloat, 0.3f) || FastApproximately(beatIntH, beatFloat, 0.1f)) //second one needs to be 1/3 of the first for reasons
        {
            Move();
            //test.SetActive(true); //debug
        }
        else
        {
            firing = false;
            //test.SetActive(false); //debug
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        //if (Conductor.instance.songPositionInBeats)
        //Move();
    }
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Damage")
        {
            Destroy(gameObject, 0f); //add corpse and blood explosion?
        }
    }

    private void Move()
    {
        //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f) //0.05 can be down to 0,
        {
            if (Input.GetKeyDown("a") || Input.GetKeyDown("d")) //is horizontal input being pressed TODO: Change to get input down so it can not just be held
            {
                if (!Physics.CheckSphere(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * moveDistanceMult, 0f, 0f), 0.2f, collision))
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * moveDistanceMult, 0f, 0f); //(x, y, z), working with x and z
            }
            else if (Input.GetKeyDown("w") || Input.GetKeyDown("s")) //is horizontal input being pressed //change to seperate if to allow diagonal movement
            {
                if (!Physics.CheckSphere(movePoint.position + new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * moveDistanceMult), 0.2f, collision))
                    movePoint.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * moveDistanceMult); //(x, y, z), working with x and z
            }
            else if (Input.GetMouseButtonDown(0))
            {
                firing = true;
            }
        }
    }
}
