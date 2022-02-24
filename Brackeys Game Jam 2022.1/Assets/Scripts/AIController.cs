using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float moveDistanceMult = 5f;
    [SerializeField] private Transform movePoint;
    [SerializeField] private GameObject player;

    [SerializeField] private LayerMask collision;

    [SerializeField] private float playerX;
    [SerializeField] private float playerZ;

    [SerializeField] private float myX;
    [SerializeField] private float myZ;

    [SerializeField] private float beatIntL;
    [SerializeField] private float beatIntH;
    [SerializeField] private float beatFloat;

    [SerializeField] private GameObject test;

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

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null; //unparent movepoint at start so it can move seperatly
    }

    // Update is called once per frame
    void Update()
    {
        playerX = player.transform.position.x; //make so only checks on / after beats so player is always on a tile nah cba
        playerZ = player.transform.position.z;

        myX = transform.position.x;
        myZ = transform.position.z;

        beatFloat = Conductor.instance.songBeatsPosHalf;
        beatIntL = Mathf.Ceil(Conductor.instance.songBeatsPosHalf) - 1.5f; //truncated
        beatIntH = Mathf.Ceil(Conductor.instance.songBeatsPosHalf) - 0.5f;

        if (FastApproximately(beatIntL, beatFloat, 0.1f) || FastApproximately(beatIntH, beatFloat, 0.03f)) //second one needs to be 1/3 of the first for reasons
        {
            Move();
            //test.SetActive(true); //debug
        }
        else
        {
            //test.SetActive(false); //debug
        }

        //Debug.Log(playerX + " " + playerZ + " / " + myX + " " + myZ);

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Damage")
        {
            Destroy(gameObject, 0f); //add corpse and blood explosion?
        }
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f) //0.05 can be down to 0
        {
            if (Mathf.Abs(myX - playerX) >= Mathf.Abs(myZ - playerZ)) //if x dis is higher, move closer in x
            {
                //Debug.Log("X dist = or lower");
                if (myX < playerX)
                    movePoint.position += new Vector3(moveDistanceMult, 0f, 0f); //(x, y, z), working with x and y
                else if (myX > playerX)
                    movePoint.position += new Vector3(-moveDistanceMult, 0f, 0f); //(x, y, z), working with x and y
            }

            else if (Mathf.Abs(myX - playerX) < Mathf.Abs(myZ - playerZ)) //if y dis is higher, move closer in y 
            {
                //Debug.Log("Z dist lower");
                if (myZ < playerZ)
                    movePoint.position += new Vector3(0f, 0f, moveDistanceMult); //(x, y, z), working with x and y
                else if (myZ > playerZ)
                    movePoint.position += new Vector3(0f, 0f, -moveDistanceMult); //(x, y, z), working with x and y
            }

        }
    }
}
