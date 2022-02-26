using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIProbe : MonoBehaviour
{
    public bool isCollided;

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "AIProbe" || collisionInfo.collider.tag == "Collision")
        {
            isCollided = true;
        }
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "AIProbe" || collisionInfo.collider.tag == "Collision")
        {
            isCollided = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
