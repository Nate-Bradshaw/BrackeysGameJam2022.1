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

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null; //unparent movepoint at start so it can move seperatly
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f) //0.05 can be down to 0, TODO: add condition of on the beat when implimented
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) //is horizontal input being pressed
            {
                if (!Physics.CheckSphere(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * moveDistanceMult, 0f, 0f), 0.2f, collision))
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * moveDistanceMult, 0f, 0f); //(x, y, z), working with x and y
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) //is horizontal input being pressed //change to seperate if to allow diagonal movement
            {
                if (!Physics.CheckSphere(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * moveDistanceMult, 0f), 0.2f, collision))
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * moveDistanceMult, 0f); //(x, y, z), working with x and y
            }
        }
    }
}
