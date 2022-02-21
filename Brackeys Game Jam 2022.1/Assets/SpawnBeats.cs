using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeats : MonoBehaviour
{

    [SerializeField] private GameObject beatMark;
    private GameObject test;


    // Start is called before the first frame update
    void Start()
    {
        beatMark = Instantiate(beatMark, new Vector3(0f, -338.33f, 0f), Quaternion.identity);
        beatMark.transform.parent = transform; //sets parent
        beatMark.transform.position = new Vector3(0, -338.33f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 360, Conductor.instance.loopPositionInAnalog));

    }
}
