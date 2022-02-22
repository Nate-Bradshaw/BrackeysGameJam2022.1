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

    private float playerX;
    private float playerY;

    private float myX;
    private float myY;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null; //unparent movepoint at start so it can move seperatly
    }

    // Update is called once per frame
    void Update()
    {
        playerX = player.transform.position.x; //make so only checks on / after beats so player is always on a tile
        playerY = player.transform.position.y;

        myX = transform.position.x;
        myY = transform.position.y;

        Move();
        //Debug.Log(playerX + " " + playerY + " / " + myX + " " + myY);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f) //0.05 can be down to 0, TODO: add condition of on the beat when implimented
        {
            if (Mathf.Abs(myX - playerX) <= Mathf.Abs(myY - playerY)) //if x dis is higher, move closer in x
            {
                Debug.Log("X dist = or lower");
                if (myX < playerX)
                    movePoint.position += new Vector3(moveDistanceMult, 0f, 0f); //(x, y, z), working with x and y
                else if (myX > playerX)
                    movePoint.position += new Vector3(-moveDistanceMult, 0f, 0f); //(x, y, z), working with x and y
            }
            /*
            else if (Mathf.Abs(myX - playerX) > Mathf.Abs(myY - playerY)) //if y dis is higher, move closer in y 
            {
                Debug.Log("Y dist lower");
                if (myY < playerY)
                    movePoint.position += new Vector3(0f, moveDistanceMult, 0f); //(x, y, z), working with x and y
                else if (myY > playerY)
                    movePoint.position += new Vector3(0f, -moveDistanceMult, 0f); //(x, y, z), working with x and y
            }
            */
        }
    }
}
