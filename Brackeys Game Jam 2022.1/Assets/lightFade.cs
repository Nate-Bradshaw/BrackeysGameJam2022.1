using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFade : MonoBehaviour
{

    [SerializeField] private float fadeAmount;
    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        myLight.intensity = myLight.intensity - fadeAmount * Time.deltaTime;
    }
}
