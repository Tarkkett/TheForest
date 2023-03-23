using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycler : MonoBehaviour
{
    private Camera cam;
    public Gradient gradient;
    public float dayLength = 10;
    public float currentTime;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnValidate()
    {
        Start();
        Update();
    }

    void Update()
    {
        if (Application.isPlaying)
        {
            currentTime += Time.deltaTime;
        } 
        var ratio = currentTime / dayLength;
        if (ratio > 1)
        {
            currentTime = 0;
        }
        cam.backgroundColor = gradient.Evaluate(ratio);
        transform.rotation = Quaternion.Euler(ratio * 360, 0, 0);

        RenderSettings.fogColor = cam.backgroundColor;
    }
}
