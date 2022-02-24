using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArcMovement : MonoBehaviour
{
    //[SerializeField] Transform target;
    public Vector3 targetPos; //access from aim script :)
    private Vector3 startPos;
    private float timing;
    private bool stopMoving;
    [SerializeField] private GameObject explosion;
    [SerializeField] private ParticleSystem particles;

    void Start()
    {
        stopMoving = false;
        startPos = transform.position;

        targetPos = Aim.instance.clickLocation;

        this.transform.parent = null;
    }

    void Update()
    {
        timing += Time.deltaTime;

        timing = timing % 5f;

        if(!stopMoving)
            transform.position = Parabola(startPos, targetPos, 10f, timing / 2f);

        if (Vector3.Distance(transform.position, targetPos) <= 1f && !stopMoving)
        {
            stopMoving = true;
            particles.Stop();

            GameObject Myexplosion = Instantiate(explosion, this.transform);

            Vector3 gridPos = new Vector3(Mathf.Round(transform.position.x / 5) * 5, -2.5f, Mathf.Round(transform.position.z / 5) * 5);
            transform.position = new Vector3(transform.position.x, -2.5f, transform.position.z);
            Myexplosion.transform.position = gridPos;
            //GameObject.Destroy(Myexplosion, 2f);

            Destroy(gameObject, 2f);
        }
    }

    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}
