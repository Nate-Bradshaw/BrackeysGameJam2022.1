using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatAnimationScript : MonoBehaviour
{

    [SerializeField] private Animator animator;

    [SerializeField] private float beatIntL;
    [SerializeField] private float beatIntH;
    [SerializeField] private float beatFloat;

    [SerializeField] private bool animTracker; //true = left, false = right

    [SerializeField] private bool waiting;

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
        animTracker = true;
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        beatFloat = Conductor.instance.songBeatsPosBase;
        beatIntL = Mathf.Ceil(Conductor.instance.songBeatsPosBase) - 1.5f; //truncated
        beatIntH = Mathf.Ceil(Conductor.instance.songBeatsPosBase) - 0.5f;

        if (FastApproximately(beatIntL, beatFloat, 0.1f) || FastApproximately(beatIntH, beatFloat, 0.03f)) //second one needs to be 1/3 of the first for reasons
        {
            if (animTracker && !waiting)
            {
                animator.SetBool("swingL", true);
                animator.SetBool("swingR", false);
                animTracker = false;
                waiting = true;
                StartCoroutine(Cooldown());
            }
            else if (!animTracker && !waiting)
            {
                animator.SetBool("swingL", false);
                animator.SetBool("swingR", true);
                animTracker = true;
                waiting = true;
                StartCoroutine(Cooldown());
            }
        }

        //Metronome();
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.1f);
        waiting = false;
    }
}
